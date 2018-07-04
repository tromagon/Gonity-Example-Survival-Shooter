using UnityEngine;

[CreateAssetMenu(fileName = "enemy_data", menuName = "Data Objects/Enemy", order = 1)]
public class EnemyDO : ScriptableObject
{
    public GameObject prefab;
    public AudioClip deathClip;
    public int startingHealth;
    public float sinkSpeed;
    public int scoreValue;
    public float timeBetweenAttacks;
    public int attackDamage;
}