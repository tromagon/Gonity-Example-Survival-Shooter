using Gonity;
using UnityEngine.SceneManagement;

public class RestartCommand : ICommand
{
    public void Execute()
    {
        SceneManager.LoadScene(0);
    }
}