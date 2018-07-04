using Gonity;
using UnityEngine;

public class DamageMediator
{
	[ViewElement]
	public DamageView view;

    [Injected]
    public IEventDispatcher eventDispatcher;

    [Injected]
    public IGameData gameData;

    [Injected]
    public IUpdater updater;

	[ViewOpened]
	public void OnViewOpened()
	{
        eventDispatcher.Add(GameEvent.PlayerDamage, OnPlayerDamage);

        updater.RunUpdate(Update);
    }

    [ViewClosed]
	public void OnViewClosed()
	{
        eventDispatcher.Remove(GameEvent.PlayerDamage, OnPlayerDamage);

        updater.StopUpdate(Update);
    }

    private void Update()
    {
        view.damageImage.color = Color.Lerp(view.damageImage.color, Color.clear,
            gameData.playerData.damageFlashSpeed * Time.deltaTime);
    }

    private void OnPlayerDamage()
    {
        view.damageImage.color = gameData.playerData.damageFlashColour;
    }
}