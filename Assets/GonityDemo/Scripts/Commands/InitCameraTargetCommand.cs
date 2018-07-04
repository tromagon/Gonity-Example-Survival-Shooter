using Gonity;

public class InitCameraTargetCommand : ICommand
{
    [Injected]
    public IEntityDatabase entityDatabase;

    public void Execute()
    {
        Entity cameraEntity = entityDatabase.QueryEntity<CameraComponent>();
        Entity playerEntity = entityDatabase.QueryEntity<PlayerComponent>();

        CameraTargetComponent cameraTargetComponent = cameraEntity.AddComponent<CameraTargetComponent>();

        cameraTargetComponent.Add(CameraTargetKey.Target, playerEntity);
        cameraTargetComponent.offset = cameraTargetComponent.entity.GetComponent<TransformComponent>().transform.position -
            playerEntity.GetComponent<TransformComponent>().transform.position;
    }
}