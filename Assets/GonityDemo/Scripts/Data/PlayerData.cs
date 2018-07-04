using UnityEngine;

[CreateAssetMenu(fileName = "player_data", menuName = "Data/Player", order = 1)]
public class PlayerData : ScriptableObject, IPlayerData
{
    public float speed;
    public int startingHealth;
    public AudioClip deathClip;
    public float damageFlashSpeed;
    public Color damageFlashColour;

    float IPlayerData.speed { get { return speed; } }
    int IPlayerData.startingHealth { get { return startingHealth; } }
    AudioClip IPlayerData.deathClip { get { return deathClip; } }
    float IPlayerData.damageFlashSpeed { get { return damageFlashSpeed; } }
    Color IPlayerData.damageFlashColour { get { return damageFlashColour; } }
}