using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DatosUsurio : MonoBehaviour
{
    public GameObject NombreUsuario;
    public GameObject TipoFinanciamiento;
    public ObtenerDatos obtenerDatos;
    private string nombreUsuario;
    private int financiamiento;

    // Start is called before the first frame update
   /*void Start(){
        nombreUsuario = obtenerDatos.usuario;
        financiamiento = obtenerDatos.progreso[0].financiamiento;
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
    } */
}