using Gonity;
using UnityEngine;

public class CameraFollowSystem : ECSSystem, IUpdate
{
    public void Update()
    {
        Entity cameraEntity = entityDatabase.QueryEntity<CameraComponent>();
        CameraTargetComponent cameraTargetComponent = cameraEntity.GetComponent<CameraTargetComponent>();
        ICameraData cameraData = entityDatabase.QueryType<GameDataComponent>().gameData.cameraData;

        Entity targetEntity = cameraTargetComponent[CameraTargetKey.Target];

        Vector3 targetCamPos = targetEntity.GetComponent<TransformComponent>().transform.position + cameraTargetComponent.offset;

        Transform cameraTranform = cameraEntity.GetComponent<TransformComponent>().transform;
        cameraTranform.position = Vector3.Lerp(cameraTranform.position, targetCamPos, cameraData.smoothing * Time.deltaTime);
    }
}