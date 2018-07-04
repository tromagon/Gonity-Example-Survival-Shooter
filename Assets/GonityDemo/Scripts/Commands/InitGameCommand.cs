using Gonity;

public class InitGameCommand : ICommand
{
    [Injected]
    public IEntityDatabase entityDatabase;

    [Injected]
    public ScoreModel scoreModel;

    [Injected]
    public IGameData gameData;

    public void Execute()
    {
        Entity rootEntity = entityDatabase.QueryEntity(Tag.Root);
        rootEntity.AddComponent<ScoreComponent>().scoreModel = scoreModel;
        rootEntity.AddComponent<GameDataComponent>().gameData = gameData;
    }
}