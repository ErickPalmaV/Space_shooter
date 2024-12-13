using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private Button soundButton;
    [SerializeField] Sprite soundOn, soundOff;
    bool _isMuted;

    public void SetAudio()
    {
        AudioListener.volume = !_isMuted ? 0 : 1;
        _isMuted = !_isMuted;
        soundButton.GetComponent<Image>().sprite = !_isMuted ? soundOn : soundOff;
    }
}
