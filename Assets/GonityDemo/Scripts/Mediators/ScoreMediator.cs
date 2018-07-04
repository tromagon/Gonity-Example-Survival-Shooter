using Gonity;

public class ScoreMediator
{
	[ViewElement]
	public ScoreView view;

    [Injected]
    public ScoreModel scoreModel;

    [ViewOpened]
	public void OnViewOpened()
	{
        scoreModel.score.onChanged.Add(OnScoreChanged);
    }

    [ViewClosed]
	public void OnViewClosed()
	{
        scoreModel.score.onChanged.Remove(OnScoreChanged);
    }

    private void OnScoreChanged()
    {
        view.scoreText.text = "Score: " + scoreModel.score.value;
    }
}