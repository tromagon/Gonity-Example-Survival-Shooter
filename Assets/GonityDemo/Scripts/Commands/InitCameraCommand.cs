using Gonity;
using UnityEngine;

public class InitCameraCommand : ICommand
{
    [Injected]
    public IEntityDatabase entityDatabase;

    public Camera camera; 

    public void Execute()
    {
        Entity cameraEntity = entityDatabase.CreateEntity();

        CameraComponent cameraComponent = cameraEntity.AddComponent<CameraComponent>();
        cameraComponent.floorMask = LayerMask.GetMask("Floor");

        cameraEntity.AddComponent<TransformComponent>().transform = camera.transform;
    }
}