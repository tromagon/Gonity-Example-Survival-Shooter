using Gonity;
using UnityEngine;

public class EnemyMovementSystem : ECSSystem, IUpdate
{
    public void Update()
    {
        ListReadOnly<EnemyComponent> enemyComponents = entityDatabase.QueryTypes<EnemyComponent>();
        if (enemyComponents.Count == 0) return;

        Entity playerEntity = entityDatabase.QueryEntity<PlayerComponent>();
        Transform playerTransform = playerEntity.GetComponent<TransformComponent>().transform;

        var enumerator = enemyComponents.GetEnumerator();
        while (enumerator.MoveNext())
        {
            EnemyComponent enemyComponent = enumerator.Current;

            if (enemyComponent.currentHealth > 0 
                && !playerEntity.HasTag(Tag.Dead))
            {
                enemyComponent.nav.SetDestination(playerTransform.position);
            }
            else
            {
                enemyComponent.nav.enabled = false;
            }
        }
    }
}