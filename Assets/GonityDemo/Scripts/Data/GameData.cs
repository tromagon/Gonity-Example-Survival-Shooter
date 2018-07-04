using UnityEngine;

[CreateAssetMenu(fileName = "game_data", menuName = "Data/Game", order = 1)]
public class GameData : ScriptableObject, IGameData
{
    public float timeBeforeRestart;
    public CameraData cameraData;
    public EnemyData enemyData;
    public GunData gunData;
    public PlayerData playerData;

    float IGameData.timeBeforeRestart { get { return timeBeforeRestart; } }
    ICameraData IGameData.cameraData { get { return cameraData; } }
    IEnemyData IGameData.enemyData { get { return enemyData; } }
    IGunData IGameData.gunData { get { return gunData; } }
    IPlayerData IGameData.playerData { get { return playerData; } }
}