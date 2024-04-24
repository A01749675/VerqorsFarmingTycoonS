using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DatosUsurio : MonoBehaviour
{
    public GameObject NombreUsuario;
    public GameObject TipoFinanciamiento;
    public UserController userController;
    public ObtenerDatos obtenerDatos;
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
    public string GetNombreUsuario(){
        return nombreUsuario;
    } 
    public string GetTipoFinanciamiento(){
        return TipoFinanciamiento.GetComponent<TextMeshProUGUI>().text;
    }
}
