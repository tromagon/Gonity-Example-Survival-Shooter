using Gonity;
using UnityEngine;
using UnityEngine.Audio;

public class InitAudioCommand : ICommand
{
    [Injected]
    public AudioModel audioModel;

    public Camera camera;
    public AudioMixer masterMixer;

    public void Execute()
    {
        audioModel.audioListener = camera.GetComponent<AudioListener>();
        audioModel.masterMixer = masterMixer;
    }
}