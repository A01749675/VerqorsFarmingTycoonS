using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    public AudioSource audioSource; //AudioSource de la música de fondo
    public GameObject volumeControl; //Scrollbar para controlar el volumen de la música
    public GameObject efectosControl; //Scrollbar para controlar el volumen de los efectos de sonido
    private bool isMuted = true; //Variable para saber si la música está silenciada
    private bool isMuted2 = true; //Variable para saber si los efectos de sonido están silenciados
    public Sprite unmuted; //Sprite para el botón de desilenciar
    public Sprite muted;   //Sprite para el botón de silenciar
    public Button muteVolumeButton; //Botón para silenciar/desilenciar la música
    public Button muteEfectsButton; //Botón para silenciar/desilenciar los efectos de sonido
    public AudioSource audioSourcemoney; //AudioSource para los efectos de sonido
    public AudioSource audioSourceLluvia; //AudioSource para efectos de sonido
    public AudioSource audioSourceFlood; //AudioSource para efectos de sonido
    public AudioSource audioSourceHurricane; //AudioSource para efectos de sonido
    public AudioSource audioSourcePlant; //AudioSource para efectos de sonido
    public AudioSource audioSourceTractor; //AudioSource para efectos de sonido
    private float volumenMusica; //Variable para guardar el volumen de la música
    private float volumenEfectos; //Variable para guardar el volumen de los efectos de sonido

    // Start is called before the first frame update
    void Start()
    {
        volumenMusica = PlayerPrefs.GetFloat("volumenMusica", 0.5f);
        isMuted = PlayerPrefs.GetInt("isMuted", 1) == 1;
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
        isMuted2 = PlayerPrefs.GetInt("isMuted2", 1) == 1;
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
        //Controlar el volumen de la música y los efectos de sonido
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
        //Silenciar/desilenciar la música
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
        //Silenciar/desilenciar los efectos de sonido
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
        //Guardar los valores de los volumenes y si están silenciados o no
        PlayerPrefs.SetFloat("volumenMusica", volumeControl.GetComponent<Scrollbar>().value);
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
        PlayerPrefs.SetFloat("volumenEfectos", efectosControl.GetComponent<Scrollbar>().value);
        PlayerPrefs.SetInt("isMuted2", isMuted2 ? 1 : 0);
        PlayerPrefs.Save();
    }
}
