using Gonity;
using UnityEngine;

public class InitGunCommand : ICommand
{
    [Injected]
    public IEntityDatabase entityDatabase;

    public GameObject gunGameObject;
    public Light faceLight;

    public void Execute()
    {
        Entity gunEntity = entityDatabase.CreateEntity();

        GunComponent gunComponent = gunEntity.AddComponent<GunComponent>();
        gunComponent.shootableMask = LayerMask.GetMask("Shootable");

        gunComponent.gunParticles = gunGameObject.GetComponent<ParticleSystem>();
        gunComponent.gunLine = gunGameObject.GetComponent<LineRenderer>();
        gunComponent.gunAudio = gunGameObject.GetComponent<AudioSource>();
        gunComponent.gunLight = gunGameObject.GetComponent<Light>();
        gunComponent.faceLight = faceLight;

        gunEntity.AddComponent<TransformComponent>().transform = gunGameObject.transform;
    }
}