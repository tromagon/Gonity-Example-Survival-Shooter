public interface IGameData
{
    float timeBeforeRestart { get; }
    ICameraData cameraData { get; }
    IEnemyData enemyData { get; }
    IGunData gunData { get; }
    IPlayerData playerData { get; }
}