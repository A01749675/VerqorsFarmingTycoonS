using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DatosUsurio : MonoBehaviour
{
    public GameObject NombreUsuario;
    public GameObject TipoFinanciamiento;

    // Start is called before the first frame update
    void Start(){
        NombreUsuario.GetComponent<TextMeshProUGUI>().text = "Marzy";
        TipoFinanciamiento.GetComponent<TextMeshProUGUI>().text = "Coyote";
    }
}