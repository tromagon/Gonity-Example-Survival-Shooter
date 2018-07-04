using Gonity;
using UnityEditor;
using UnityEngine;

public class PauseMediator
{
	[ViewElement]
	public PauseView view;

    [Injected]
    public IUpdater updater;

    [Injected]
    public AudioModel audioModel;

    [ViewOpened]
	public void OnViewOpened()
	{
        view.canvas.enabled = false;

        view.quitButton.onClick.AddListener(OnQuit);
        view.resumeButton.onClick.AddListener(OnResume);
        view.audioToggle.onValueChanged.AddListener(OnAudioToggle);
        view.effectsSlider.onValueChanged.AddListener(OnEffectsSlider);
        view.musicSlider.onValueChanged.AddListener(OnMusicSlider);

        updater.RunUpdate(Update);
    }
	
	[ViewClosed]
	public void OnViewClosed()
	{
        view.quitButton.onClick.RemoveListener(OnQuit);
        view.resumeButton.onClick.RemoveListener(OnResume);
        view.audioToggle.onValueChanged.RemoveListener(OnAudioToggle);
        view.effectsSlider.onValueChanged.RemoveListener(OnEffectsSlider);
        view.musicSlider.onValueChanged.RemoveListener(OnMusicSlider);

        updater.StopUpdate(Update);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void OnQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

    private void OnResume()
    {
        Pause();
    }

    private void Pause()
    {
        view.canvas.enabled = !view.canvas.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Lowpass();
    }

    private void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            view.paused.TransitionTo(.01f);
        }
        else
        {
            view.unpaused.TransitionTo(.01f);
        }
    }

    private void OnAudioToggle(bool value)
    {
        audioModel.masterMixer.SetFloat("masterVol", value ? 0 : -80);
    }

    private void OnEffectsSlider(float value)
    {
        audioModel.masterMixer.SetFloat("sfxVol", value);
    }

    private void OnMusicSlider(float value)
    {
        audioModel.masterMixer.SetFloat("musicVol", value);
    }
}