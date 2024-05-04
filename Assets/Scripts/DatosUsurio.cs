using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*Extrae el nombre del usuario y su tipo de financiamiento.
Autores:  Santiago Chevez Trejo, Carlos Iker Fuentes Reyes, 
          Alma Teresa Carpio Revilla, Mariana Marzyani Hernandez Jurado, 
          y Alan Rodrigo Vega Reza */

public class DatosUsurio : MonoBehaviour
{
    public GameObject NombreUsuario;
    public GameObject TipoFinanciamiento;
    public UserController userController; // Referencia al script UserController
    public ObtenerDatos obtenerDatos; // Referencia al script ObtenerDatos
    public string nombreUsuario;
    public int financiamiento;

    //Start is called before the first frame update
    void Start(){
        nombreUsuario = obtenerDatos.usuario;
        financiamiento = userController.GetParameter("financiamiento");
        switch (financiamiento){
            case 1:
                TipoFinanciamiento.GetComponent<TextMeshProUGUI>().text = "Verqor";
                break;
            case 2:
                TipoFinanciamiento.GetComponent<TextMeshProUGUI>().text = "Banco";
                break;
            case 3:
                TipoFinanciamiento.GetComponent<TextMeshProUGUI>().text = "Coyote";
                break;
            default:
                TipoFinanciamiento.GetComponent<TextMeshProUGUI>().text = "-";
                break;
        }
        NombreUsuario.GetComponent<TextMeshProUGUI>().text = nombreUsuario;
    }
    //Recupera el nombre del usuario
    public string GetNombreUsuario(){
        return nombreUsuario;
    } 
    //Recupera el tipo de financiamiento
    public string GetTipoFinanciamiento(){
        return TipoFinanciamiento.GetComponent<TextMeshProUGUI>().text;
    }
}
