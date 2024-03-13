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
    public GameObject Celular;
    public GameObject Mercado;
    public GameObject Inventario;
    public GameObject Deuda;
    public GameObject Herramienta;
    private bool flagUsuario=false;
    private bool flagFinanciamiento=false;
    private bool flagProduccion=false;
    private bool flagRankings=false;
    private bool flagCelular=false;
    private bool flagMercado=false;
    private bool flagInventario=false;
    private bool flagDeuda=false;
    public bool flagHerramienta=false;
    

    //Al dar click al boton de opciones se abre el panel PanelOpciones
    public void OpenOpciones()
    {
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        PanelOpciones.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseOpciones()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
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

    public void ShowCelular()
    {
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        if(flagCelular){
            flagCelular=false;
            Celular.SetActive(false);
        }else{
            flagCelular=true;
            Celular.SetActive(true);
        }
    }
    public void ShowMercado()
    {
        flagCelular=false;
        Celular.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        if(flagMercado){
            flagMercado=false;
            Mercado.SetActive(false);
        }else{
            flagMercado=true;
            Mercado.SetActive(true);
        }
    }
    public void ShowInventario()
    {
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        if(flagInventario){
            flagInventario=false;
            Inventario.SetActive(false);
        }else{
            flagInventario=true;
            Inventario.SetActive(true);
        }
    }
    public void ShowDeuda()
    {
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        if(flagDeuda){
            flagDeuda=false;
            Deuda.SetActive(false);
        }else{
            flagDeuda=true;
            Deuda.SetActive(true);
        }
    }

    public void hoz(){
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        if(flagHerramienta){
            flagHerramienta=false;    
        }else{
            flagHerramienta=true;
        }
    }
}
