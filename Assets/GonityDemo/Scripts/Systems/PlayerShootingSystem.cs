using Gonity;
using UnityEngine;

public class PlayerShootingSystem : ECSSystem, IUpdate
{
    public void Update()
    {
        if (entityDatabase.QueryEntity<PlayerComponent>().HasTag(Tag.Dead)) return;

        GunComponent gunComponent = entityDatabase.QueryType<GunComponent>();

        IGunData gunData = entityDatabase.QueryType<GameDataComponent>().gameData.gunData;

        gunComponent.timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && gunComponent.timer >= gunData.timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot(gunComponent, gunData);
        }

        if (gunComponent.timer >= gunData.timeBetweenBullets * gunData.effectsDisplayTime)
        {
            DisableEffects(gunComponent);
        }
    }

    private void Shoot(GunComponent gunComponent, IGunData gunData)
    {
        Transform transform = gunComponent.entity.GetComponent<TransformComponent>().transform;

        gunComponent.timer = 0f;

        gunComponent.gunAudio.Play();

        gunComponent.gunLight.enabled = true;
        gunComponent.faceLight.enabled = true;

        gunComponent.gunParticles.Stop();
        gunComponent.gunParticles.Play();

        gunComponent.gunLine.enabled = true;
        gunComponent.gunLine.SetPosition(0, transform.position);

        gunComponent.shootRay.origin = transform.position;
        gunComponent.shootRay.direction = transform.forward;

        if (Physics.Raycast(gunComponent.shootRay, out gunComponent.shootHit, gunData.range, gunComponent.shootableMask))
        {
            ColliderComponentMap colliderComponentMap = entityDatabase.QueryType<ColliderComponentMap>();
            if (colliderComponentMap.ContainsKey(gunComponent.shootHit.collider.gameObject))
            {
                Entity targetEntity = colliderComponentMap[gunComponent.shootHit.collider.gameObject];
                EnemyDamageComponent damageComponent = targetEntity.AddComponent<EnemyDamageComponent>();
                damageComponent.amount = gunData.damagePerShot;
                damageComponent.hitPoint = gunComponent.shootHit.point;
            }

            gunComponent.gunLine.SetPosition(1, gunComponent.shootHit.point);
        }
        else
        {
            gunComponent.gunLine.SetPosition(1, gunComponent.shootRay.origin + gunComponent.shootRay.direction * gunData.range);
        }
    }

    private void DisableEffects(GunComponent gunComponent)
    {
        gunComponent.gunLine.enabled = false;
        gunComponent.faceLight.enabled = false;
        gunComponent.gunLight.enabled = false;
    }
}