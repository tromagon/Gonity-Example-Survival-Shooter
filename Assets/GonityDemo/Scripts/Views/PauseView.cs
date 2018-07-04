using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    public Canvas canvas;
    public Button quitButton;
    public Button resumeButton;
    public Toggle audioToggle;
    public Slider effectsSlider;
    public Slider musicSlider;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
}