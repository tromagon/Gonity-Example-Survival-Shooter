using Gonity;
using UnityEngine;

public class EnemyAttackSystem : ECSSystem, IUpdate
{
    public void Update()
    {
        ListReadOnly<EnemyComponent> enemyComponents = entityDatabase.QueryTypes<EnemyComponent>();
        if (enemyComponents.Count == 0) return;

        Entity playerEntity = entityDatabase.QueryEntity<PlayerComponent>();
        GameObject playerObject = playerEntity.GetComponent<TransformComponent>().transform.gameObject;

        var enumerator = enemyComponents.GetEnumerator();
        while (enumerator.MoveNext())
        {
            EnemyComponent enemyComponent = enumerator.Current;
            enemyComponent.attackTimer += Time.deltaTime;

            if (enemyComponent.attackTimer >= enemyComponent.enemyDO.timeBetweenAttacks
                && enemyComponent.trigger.isColliding(playerObject)
                && enemyComponent.currentHealth > 0)
            {
                Attack(enemyComponent, playerEntity);
            }

            if (playerEntity.HasTag(Tag.Dead))
            {
                enemyComponent.anim.SetTrigger("PlayerDead");
            }
        }
    }

    private void Attack(EnemyComponent enemyComponent, Entity playerEntity)
    {
        enemyComponent.attackTimer = 0f;

        if (!playerEntity.HasTag(Tag.Dead) &&
            !playerEntity.GetComponent<PlayerDamageComponent>())
        {
            playerEntity.AddComponent<PlayerDamageComponent>().amount = enemyComponent.enemyDO.attackDamage;
        }
    }
}