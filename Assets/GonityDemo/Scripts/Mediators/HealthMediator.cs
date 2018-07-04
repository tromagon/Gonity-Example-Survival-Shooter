using Gonity;

public class HealthMediator
{
	[ViewElement]
	public HealthView view;

    [Injected]
    public PlayerHealthModel healthModel;

    [ViewOpened]
    public void OnViewOpened()
    {
        healthModel.health.onChanged.Add(OnHealthChanged);
    }

    [ViewClosed]
    public void OnViewClosed()
    {
        healthModel.health.onChanged.Remove(OnHealthChanged);
    }

    private void OnHealthChanged()
    {
        view.healthSlider.value = healthModel.health.value;
    }
}