using Gonity;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerMovementSystem : ECSSystem, IUpdate
{
	public void Update()
	{
        PlayerComponent playerComponent = entityDatabase.QueryType<PlayerComponent>();
        if (playerComponent.entity.HasTag(Tag.Dead)) return;

        IGameData gameData = entityDatabase.QueryType<GameDataComponent>().gameData;

        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

        Move(playerComponent, gameData.playerData, h, v);
        Turning(playerComponent, gameData.cameraData);
        Animating(playerComponent, h, v);
    }

    private void Move(PlayerComponent playerComponent, IPlayerData playerData, float h, float v)
    {
        playerComponent.movement.Set(h, 0f, v);
        playerComponent.movement = playerComponent.movement.normalized * playerData.speed * Time.deltaTime;

        Transform transform = playerComponent.entity.GetComponent<TransformComponent>().transform;
        playerComponent.rigidBody.MovePosition(transform.position + playerComponent.movement);
    }

    private void Turning(PlayerComponent playerComponent, ICameraData cameraData)
    {
        CameraComponent cameraComponent = entityDatabase.QueryType<CameraComponent>();

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, cameraData.camRayLength, cameraComponent.floorMask))
        {
            Transform transform = playerComponent.entity.GetComponent<TransformComponent>().transform;
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerComponent.rigidBody.MoveRotation(newRotation);
        }
    }

    private void Animating(PlayerComponent playerComponent, float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        playerComponent.anim.SetBool("IsWalking", walking);
    }
}