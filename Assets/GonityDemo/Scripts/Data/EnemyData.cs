using UnityEngine;

[CreateAssetMenu(fileName = "enemy_data", menuName = "Data/Enemy", order = 1)]
public class EnemyData : ScriptableObject, IEnemyData
{
    public float sinkSpeed;
    public float timeBeforeDestroy;

    float IEnemyData.sinkSpeed { get { return sinkSpeed;  } }
    float IEnemyData.timeBeforeDestroy { get { return timeBeforeDestroy;  } }
}