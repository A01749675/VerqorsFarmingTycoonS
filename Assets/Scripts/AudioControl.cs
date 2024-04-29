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
    public AudioSource audioSourceTractor;
    private float volumenMusica;
    private float volumenEfectos;

    // Start is called before the first frame update
    void Start()
    {
        volumenMusica = PlayerPrefs.GetFloat("volumenMusica", 0.5f);
        isMuted = PlayerPrefs.GetInt("isMuted", 0) == 1;
        volumeControl.GetComponent<Scrollbar>().value = volumenMusica;
        audioSource.mute = !isMuted;
        if(isMuted)
        {
            muteVolumeButton.GetComponent<Image>().sprite = unmuted;
        }
        else
        {
            muteVolumeButton.GetComponent<Image>().sprite = muted;
        }
        volumenEfectos = PlayerPrefs.GetFloat("volumenEfectos", 0.5f);
        isMuted2 = PlayerPrefs.GetInt("isMuted2", 0) == 1;
        efectosControl.GetComponent<Scrollbar>().value = volumenEfectos;
        audioSourcemoney.mute = !isMuted2;
        audioSourceLluvia.mute = !isMuted2;
        audioSourceFlood.mute = !isMuted2;
        audioSourceHurricane.mute = !isMuted2;
        audioSourcePlant.mute = !isMuted2;
        audioSourceTractor.mute = !isMuted2;
        if(isMuted2)
        {
            muteEfectsButton.GetComponent<Image>().sprite = unmuted;
        }
        else
        {
            muteEfectsButton.GetComponent<Image>().sprite = muted;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volumeControl.GetComponent<Scrollbar>().value;
        audioSourcemoney.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourceLluvia.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourceFlood.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourceHurricane.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourcePlant.volume = efectosControl.GetComponent<Scrollbar>().value;
        audioSourceTractor.volume = efectosControl.GetComponent<Scrollbar>().value;
        
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
        audioSourceTractor.mute = isMuted2;
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

    public void Save()
    {
        PlayerPrefs.SetFloat("volumenMusica", volumeControl.GetComponent<Scrollbar>().value);
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
        PlayerPrefs.SetFloat("volumenEfectos", efectosControl.GetComponent<Scrollbar>().value);
        PlayerPrefs.SetInt("isMuted2", isMuted2 ? 1 : 0);
    }
}
