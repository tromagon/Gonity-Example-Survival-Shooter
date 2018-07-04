using Gonity;
using UnityEngine;

public class GameOverMediator
{
	[ViewElement]
	public GameOverView view;

    [Injected]
    public IEventDispatcher eventDispatcher;

    [Injected]
    public IGameData gameData;

    [Injected]
    public IUpdater updater;

    private float _timer;

	[ViewOpened]
	public void OnViewOpened()
	{
        eventDispatcher.Add(GameEvent.PlayerDead, OnPlayerDead);
    }

    [ViewClosed]
	public void OnViewClosed()
	{
        eventDispatcher.Remove(GameEvent.PlayerDead, OnPlayerDead);
    }

    private void OnPlayerDead()
    {
        view.anim.SetTrigger("GameOver");
        _timer = gameData.timeBeforeRestart;
        updater.RunUpdate(Update);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            updater.StopUpdate(Update);
            eventDispatcher.Dispatch(GameEvent.GameOverComplete);
        }
    }
}