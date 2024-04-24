using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject volumeControl;
    public GameObject efectosControl;
    private bool isMuted = true;
    private bool isMuted2 = true;
    public Sprite unmuted;
    public Sprite muted;
    public Button muteVolumeButton;
    public Button muteEfectsButton;
    public AudioSource audioSourcemoney;
    public AudioSource audioSourceLluvia;
    public AudioSource audioSourceFlood;
    public AudioSource audioSourceHurricane;
    public AudioSource audioSourcePlant;
    
    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volumeControl.GetComponent<Scrollbar>().value;
        audioSourcemoney.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourceLluvia.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourceFlood.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourceHurricane.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourcePlant.volume = efectosControl.GetComponent<Scrollbar>().value;
        
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
    public void MuteE(){
        audioSourcemoney.mute = isMuted2;
        audioSourceLluvia.mute = isMuted2;
        audioSourceFlood.mute = isMuted2;
        audioSourceHurricane.mute = isMuted2;
        audioSourcePlant.mute = isMuted2;
        if(!isMuted2)
        {
            muteEfectsButton.GetComponent<Image>().sprite = unmuted;
        }
        else
        {
            muteEfectsButton.GetComponent<Image>().sprite = muted;
        }
        isMuted2=!isMuted2;
    }
}
