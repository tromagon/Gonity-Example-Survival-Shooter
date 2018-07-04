using Gonity;

public class PlayerDamageSystem : ECSSystem, IUpdate
{
	public void Update()
	{
        PlayerDamageComponent playerDamageComponent = entityDatabase.QueryType<PlayerDamageComponent>();
        if (!playerDamageComponent) return;

        Entity playerEntity = playerDamageComponent.entity;

        PlayerComponent playerComponent = playerEntity.GetComponent<PlayerComponent>();
        playerComponent.audioSource.Play();

        playerComponent.healthModel.health.value -= playerDamageComponent.amount;

        if (playerComponent.healthModel.health.value <= 0)
        {
            Death(playerComponent);
        }

        playerEntity.RemoveComponent<PlayerDamageComponent>();

        entityDatabase.QueryType<EventComponent>().eventDispatcher.Dispatch(GameEvent.PlayerDamage);
    }

    private void Death(PlayerComponent playerComponent)
    {
        playerComponent.anim.SetTrigger("Die");

        playerComponent.audioSource.clip = entityDatabase.QueryType<GameDataComponent>().gameData.playerData.deathClip;
        playerComponent.audioSource.Play();

        entityDatabase.QueryType<EventComponent>().eventDispatcher.Dispatch(GameEvent.PlayerDead);

        playerComponent.entity.AddTag(Tag.Dead);
    }
}