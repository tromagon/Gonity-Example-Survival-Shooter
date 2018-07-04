using Gonity;

public class EnemyDamageSystem : ECSSystem, IUpdate
{
	public void Update()
	{
        ListReadOnly<EnemyDamageComponent> enemyDamageComponent = entityDatabase.QueryTypes<EnemyDamageComponent>();
        if (enemyDamageComponent.Count == 0) return;

        for (int i = enemyDamageComponent.Count - 1; i >= 0; --i)
        {
            EnemyDamageComponent damageComponent = enemyDamageComponent[i];
            Entity entity = damageComponent.entity;

            EnemyComponent enemyComponent = entity.GetComponent<EnemyComponent>();
            enemyComponent.audioSource.Play();

            enemyComponent.hitParticles.transform.position = damageComponent.hitPoint;
            enemyComponent.hitParticles.Play();

            enemyComponent.currentHealth -= damageComponent.amount;

            if (enemyComponent.currentHealth <= 0)
            {
                Death(enemyComponent);
            }

            entity.RemoveComponent<EnemyDamageComponent>();
        }
	}

    private void Death(EnemyComponent enemyComponent)
    {
        enemyComponent.capsuleCollider.isTrigger = true;
        enemyComponent.anim.SetTrigger("Dead");

        enemyComponent.rigidBody.isKinematic = true;
        enemyComponent.nav.enabled = false;

        enemyComponent.audioSource.clip = enemyComponent.enemyDO.deathClip;
        enemyComponent.audioSource.Play();

        enemyComponent.entity.AddComponent<EnemyDeathComponent>().timer = 
            entityDatabase.QueryType<GameDataComponent>().gameData.enemyData.timeBeforeDestroy;

        entityDatabase.QueryType<ScoreComponent>().scoreModel.score.value += enemyComponent.enemyDO.scoreValue;

        enemyComponent.entity.RemoveComponent<EnemyComponent>();
    }
}