using System;
using UnityEngine;

[Serializable]
public class EnemySpawnerSO 
{
    public EnemyDO enemyDO;
    public float spawnTime;
    public Transform[] spawnPoints;
}