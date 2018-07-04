using UnityEngine;

public interface IPlayerData
{
    float speed { get; }
    int startingHealth { get; }
    AudioClip deathClip { get; }
    float damageFlashSpeed { get; }
    Color damageFlashColour { get; }
}