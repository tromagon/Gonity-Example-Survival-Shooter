using Gonity;
using UnityEngine;
using UnityEngine.Audio;

public class GameContext : MonoBehaviour
{
    public GameData gameData;
    public Camera cameraObject;
    public GameObject player;
    public GameObject gun;
    public Light faceLight;
    public EnemySpawnerSO[] spawners;

    public DamageView damageView;
    public GameOverView gameOverView;
    public HealthView healthView;
    public PauseView pauseView;
    public ScoreView scoreView;

    public AudioMixer masterMixer;

    private IDirectCommand _directCommand;
    private IEventCommandMap _eventCommandMap;
    private IEventDispatcher _eventDispatcher;
    private IInjector _injector;
    private IEntityDatabase _entityDatabase;
    private IMediatorMap _mediatorMap;
    private SystemMap _systemMap;
    private ViewFactory _viewFactory;
    private Updater _updater;

    private void Awake()
    {
        _injector = new Injector();
        _directCommand = new DirectCommand(_injector);
        _eventDispatcher = new EventDispatcher();
        _eventCommandMap = new EventCommandMap(_eventDispatcher, _injector);
        _viewFactory = new ViewFactory();
        _mediatorMap = new MediatorMap(_viewFactory, _injector);
        _entityDatabase = new EntityDatabase();
        _systemMap = new SystemMap(_entityDatabase);
        _updater = new Updater();

        _injector.Inject(_directCommand);
        _injector.Inject(_eventDispatcher);
        _injector.Inject(_entityDatabase);
        _injector.Inject(_mediatorMap);
        _injector.Inject<IUpdater>(_updater);

        // systems
        _systemMap.Add(new PlayerMovementSystem());
        _systemMap.Add(new PlayerShootingSystem());
        _systemMap.Add(new CameraFollowSystem());
        _systemMap.Add(new EnemySpawnerSystem());
        _systemMap.Add(new EnemyMovementSystem());
        _systemMap.Add(new EnemyDamageSystem());
        _systemMap.Add(new EnemyAttackSystem());
        _systemMap.Add(new PlayerDamageSystem());
        _systemMap.Add(new EnemySinkingSystem());

        // injection
        _injector.Inject<IGameData>(gameData);
        _injector.Inject(new AudioModel());
        _injector.Inject(new PlayerHealthModel());
        _injector.Inject(new ScoreModel());

        // view factory
        _viewFactory.Register(damageView, false);
        _viewFactory.Register(gameOverView, false);
        _viewFactory.Register(healthView, false);
        _viewFactory.Register(pauseView, false);
        _viewFactory.Register(scoreView, false);

        // mediator
        _mediatorMap.Map<DamageMediator>(ViewType.Damage);
        _mediatorMap.Map<GameOverMediator>(ViewType.GameOver);
        _mediatorMap.Map<HealthMediator>(ViewType.Health);
        _mediatorMap.Map<PauseMediator>(ViewType.Pause);
        _mediatorMap.Map<ScoreMediator>(ViewType.Score);

        // event command
        _eventCommandMap.Map(GameEvent.GameOverComplete, new RestartCommand());
    }

    private void Start()
    {
        Entity rootEntity = _entityDatabase.CreateEntity();
        rootEntity.AddTag(Tag.Root);
        rootEntity.AddComponent<EventComponent>().eventDispatcher = _eventDispatcher;

        _directCommand.Execute(new InitGameCommand());
        _directCommand.Execute(new InitAudioCommand { camera = cameraObject, masterMixer = masterMixer });
        _directCommand.Execute(new InitCameraCommand { camera = cameraObject });
        _directCommand.Execute(new InitPlayerCommand { playerGameObject = player });
        _directCommand.Execute(new InitCameraTargetCommand());
        _directCommand.Execute(new InitGunCommand { gunGameObject = gun, faceLight = faceLight });
        _directCommand.Execute(new InitEnemySpawnerCommand { spawners = spawners });
        _directCommand.Execute(new InitUICommand());
    }

    private void Update()
    {
        _systemMap.Update();
        _updater.Update();
    }

    private void LateUpdate()
    {
        _systemMap.LateUpdate();
        _updater.LateUpdate();
    }

    private void OnDestroy()
    {
        _entityDatabase.DestroyAllEntities();
        _injector.ClearAll();
        _eventDispatcher.RemoveAll();
        _eventCommandMap.ClearAll();
        _viewFactory.ClearAll();
        _mediatorMap.ClearAll();
    }
}
