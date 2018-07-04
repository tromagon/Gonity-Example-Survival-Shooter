using Gonity;
using UnityEngine;

public class InitPlayerCommand : ICommand
{
    [Injected]
    public IEntityDatabase entityDatabase;

    [Injected]
    public IGameData gameData;

    [Injected]
    public PlayerHealthModel healthModel;

    public GameObject playerGameObject;

    public void Execute()
    {
        Entity playerEntity = entityDatabase.CreateEntity();

        PlayerComponent playerComponent = playerEntity.AddComponent<PlayerComponent>();
        playerComponent.anim = playerGameObject.GetComponent<Animator>();
        playerComponent.rigidBody = playerGameObject.GetComponent<Rigidbody>();
        playerComponent.audioSource = playerGameObject.GetComponent<AudioSource>();
        playerComponent.healthModel = healthModel;

        healthModel.health.value = gameData.playerData.startingHealth;

        playerEntity.AddComponent<TransformComponent>().transform = playerGameObject.transform;
    }
}