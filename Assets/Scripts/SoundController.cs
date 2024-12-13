
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    AudioListener _listener;
    [SerializeField] Button soundbutton;
    [SerializeField] Sprite soundOn, soundOff;
    bool isMuted = false;
    private void Start()
    {
        if (Camera.main != null) _listener = Camera.main.GetComponent<AudioListener>();
    }
    public void SetAudio()
    {
        if (!isMuted)
        {
            AudioListener.volume= 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
        isMuted = !isMuted;
        soundbutton.GetComponent<Image>().sprite = !isMuted ? soundOn : soundOff;
    }
}
