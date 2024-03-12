using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiControl : MonoBehaviour
{
    public GameObject PanelOpciones;
    public GameObject UsuarioNombre;
    public GameObject Financiamiento;
    public GameObject Produccion;
    public GameObject Rankings;
    private bool flagUsuario=false;
    private bool flagFinanciamiento=false;
    private bool flagProduccion=false;
    private bool flagRankings=false;
    

    //Al dar click al boton de opciones se abre el panel PanelOpciones
    public void OpenOpciones()
    {
        PanelOpciones.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseOpciones()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        PanelOpciones.SetActive(false);
        Time.timeScale = 1;
    }

    public void Usuario()
    {
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        if(flagUsuario){
            flagUsuario=false;
            UsuarioNombre.SetActive(false);
        }else{
            flagUsuario=true;
            UsuarioNombre.SetActive(true);
        }
        
    }
    public void ShowFinanciamiento()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
        if(flagFinanciamiento){
            flagFinanciamiento=false;
            Financiamiento.SetActive(false);
        }else{
            flagFinanciamiento=true;
            Financiamiento.SetActive(true);
        }
        
    }
    public void ShowProduccion()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
        if(flagProduccion){
            flagProduccion=false;
            Produccion.SetActive(false);
        }else{
            flagProduccion=true;
            Produccion.SetActive(true);
        }
        
    }
    public void ShowRankings()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        if(flagRankings){
            flagRankings=false;
            Rankings.SetActive(false);
        }else{
            flagRankings=true;
            Rankings.SetActive(true);
        }
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
