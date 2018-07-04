using Gonity;
using UnityEngine;

public class EnemySinkingSystem : ECSSystem, IUpdate
{
	public void Update()
	{
        ListReadOnly<EnemyDeathComponent> enemyDeathComponents = entityDatabase.QueryTypes<EnemyDeathComponent>();
        if (enemyDeathComponents.Count == 0) return;

        IEnemyData enemyData = entityDatabase.QueryType<GameDataComponent>().gameData.enemyData;

        for (int i = enemyDeathComponents.Count - 1; i >= 0; --i)
        {
            EnemyDeathComponent enemyDeathComponent = enemyDeathComponents[i];

            Entity entity = enemyDeathComponent.entity;
            Transform transform = entity.GetComponent<TransformComponent>().transform;
            transform.Translate(-Vector3.up * enemyData.sinkSpeed * Time.deltaTime);

            enemyDeathComponent.timer -= Time.deltaTime;

            if (enemyDeathComponent.timer < 0)
            {
                GameObject.Destroy(transform.gameObject);
                entityDatabase.DestroyEntity(entity);
            }
        }
	}
}