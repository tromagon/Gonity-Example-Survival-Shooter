using Gonity;

public class InitEnemySpawnerCommand : ICommand
{
    [Injected]
    public IEntityDatabase entityDatabase;

    public EnemySpawnerSO[] spawners;

    public void Execute()
	{
        for (int i = 0; i < spawners.Length; ++i)
        {
            EnemySpawnerComponent enemySpawnerComponent = entityDatabase.CreateEntity().AddComponent<EnemySpawnerComponent>(); ;
            enemySpawnerComponent.spawnerData = spawners[i];
            enemySpawnerComponent.timer = enemySpawnerComponent.spawnerData.spawnTime;
        }

        Entity rootEntity = entityDatabase.QueryEntity(Tag.Root);
        rootEntity.AddComponent<ColliderComponentMap>();
    }
}