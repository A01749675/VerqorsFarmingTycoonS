using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject volumeControl;
    private bool isMuted = true;
    public Sprite unmuted;
    public Sprite muted;
    public Button muteVolumeButton;
    
    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volumeControl.GetComponent<Scrollbar>().value;
    }
    public void Mute()
    {
        audioSource.mute = isMuted;
        if(!isMuted)
        {
            muteVolumeButton.GetComponent<Image>().sprite = unmuted;
        }
        else
        {
            muteVolumeButton.GetComponent<Image>().sprite = muted;
        }
        isMuted=!isMuted;
    }
}
