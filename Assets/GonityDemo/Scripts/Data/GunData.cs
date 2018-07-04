using UnityEngine;

[CreateAssetMenu(fileName = "gun_data", menuName = "Data/Gun", order = 1)]
public class GunData : ScriptableObject, IGunData
{
    public float timeBetweenBullets;
    public int damagePerShot;
    public float range;
    public float effectsDisplayTime;

    float IGunData.timeBetweenBullets { get { return timeBetweenBullets; } }
    int IGunData.damagePerShot { get { return damagePerShot; } }
    float IGunData.range { get { return range; } }
    float IGunData.effectsDisplayTime { get { return effectsDisplayTime; } }
}