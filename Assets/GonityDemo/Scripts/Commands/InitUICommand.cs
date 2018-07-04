using Gonity;

public class InitUICommand : ICommand
{
    [Injected]
    public IGameData gameData;

    [Injected]
    public IMediatorMap mediatorMap;

    public void Execute()
    {
        mediatorMap.Open(ViewType.Damage);
        mediatorMap.Open(ViewType.GameOver);
        mediatorMap.Open(ViewType.Health);
        mediatorMap.Open(ViewType.Pause);
        mediatorMap.Open(ViewType.Score);
    }
}