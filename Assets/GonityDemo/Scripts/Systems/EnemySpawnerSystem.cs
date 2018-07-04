using Gonity;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnerSystem : ECSSystem, IUpdate
{
    public void Update()
    {
        ListReadOnly<EnemySpawnerComponent> enemySpawnerComponents = entityDatabase.QueryTypes<EnemySpawnerComponent>();
        var enumerator = enemySpawnerComponents.GetEnumerator();

        while(enumerator.MoveNext())
        {
            EnemySpawnerComponent enemySpawnerComponent = enumerator.Current;
            enemySpawnerComponent.timer -= Time.deltaTime;

            if (enemySpawnerComponent.timer <= 0)
            {
                Spawn(enemySpawnerComponent.spawnerData);
                enemySpawnerComponent.timer = enemySpawnerComponent.spawnerData.spawnTime;
            }
        }
    }

    private void Spawn(EnemySpawnerSO spawnerData)
    {
        int spawnPointIndex = Random.Range(0, spawnerData.spawnPoints.Length);
        Transform point = spawnerData.spawnPoints[spawnPointIndex];

        Entity enemyEntity = entityDatabase.CreateEntity();
        EnemyComponent enemyComponent = enemyEntity.AddComponent<EnemyComponent>();
        enemyComponent.enemyDO = spawnerData.enemyDO;

        GameObject enemyObject = GameObject.Instantiate(spawnerData.enemyDO.prefab, point.position, point.rotation);
        enemyEntity.AddComponent<TransformComponent>().transform = enemyObject.transform;

        enemyComponent.nav = enemyObject.GetComponent<NavMeshAgent>();
        enemyComponent.audioSource = enemyObject.GetComponent<AudioSource>();
        enemyComponent.hitParticles = enemyObject.GetComponentInChildren<ParticleSystem>();
        enemyComponent.capsuleCollider = enemyObject.GetComponent<CapsuleCollider>();
        enemyComponent.rigidBody = enemyObject.GetComponent<Rigidbody>();
        enemyComponent.anim = enemyObject.GetComponent<Animator>();
        enemyComponent.trigger = enemyObject.GetComponent<TriggerBehaviour>();
        enemyComponent.currentHealth = spawnerData.enemyDO.startingHealth;

        entityDatabase.QueryType<ColliderComponentMap>().Add(enemyObject, enemyEntity);
    }
}