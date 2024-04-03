using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObtenerDatos : MonoBehaviour
{
    
    // Variables públicas para guardar los datos del usuario
    public bool success;
    public string message;
    public int user_id;
    public Usuario usuario;
    public List<Progreso> progreso;
    public List<Semilla> semillas;
    public List<Cultivo> cultivos;

    private void Awake()
    {
        try
        {
            StartCoroutine(ObtenerDatosUsuario());
        }
        catch (Exception e)
        {
            Debug.LogError("Error al obtener los datos del usuario: " + e.Message);
            // imprimir 
            Debug.Log("Usando datos dummy" );;
            // datos dummy
            usuario.usuario = "User";
            usuario.tipo_usuario = "Agricultor";
            usuario.id = 1;
            progreso = new List<Progreso> { new() { financiamiento = 1 } };
            semillas = new List<Semilla> { new() { maiz = 10, trigo = 10, chile = 10, tomate = 10 } };

        }
    }

    private IEnumerator ObtenerDatosUsuario()
    {
        // Obtener la URL absoluta de la aplicación
        string url = Application.absoluteURL;

        int index = url.IndexOf("user_id=");
        if (index != -1)
        {
            // Obtener el valor del 'user_id' desde la URL
            string userIdStr = url.Substring(index + 8); // Sumar 8 para ignorar 'user_id='
            if (int.TryParse(userIdStr, out user_id))
            {
                Debug.Log("Usando datos dummy" );
            // datos dummy
            usuario.usuario = "User";
            usuario.tipo_usuario = "Agricultor";
            usuario.id = 1;
            progreso = new List<Progreso> { new() { financiamiento = 1 } };
            semillas = new List<Semilla> { new() { maiz = 10, trigo = 10, chile = 10, tomate = 10 } };
            }
            else
            {
                Debug.Log("Usando datos dummy" );
            // datos dummy
            usuario.usuario = "User";
            usuario.tipo_usuario = "Agricultor";
            usuario.id = 1;
            progreso = new List<Progreso> { new() { financiamiento = 1 } };
            semillas = new List<Semilla> { new() { maiz = 10, trigo = 10, chile = 10, tomate = 10 } };
            }
        }
        else
        {
            Debug.Log("Usando datos dummy" );
            // datos dummy
            usuario.usuario = "User";
            usuario.tipo_usuario = "Agricultor";
            usuario.id = 1;
            progreso = new List<Progreso> { new() { financiamiento = 1 } };
            semillas = new List<Semilla> { new() { maiz = 10, trigo = 10, chile = 10, tomate = 10 } };
        }

        string apiUrl = "http://localhost:8080/Verqor/api/apiUsuario.php?user_id=" + user_id;

        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonString = www.downloadHandler.text;
            DatosUsuario datosUsuario = JsonUtility.FromJson<DatosUsuario>(jsonString);

            success = datosUsuario.success;
            message = datosUsuario.message;
            usuario = datosUsuario.usuario;
            progreso = datosUsuario.progreso;
            semillas = datosUsuario.semillas;
            cultivos = datosUsuario.cultivos;

            if (success)
            {
                Debug.Log("Nombre de usuario: " + usuario.usuario);
                Debug.Log("Tipo de usuario: " + usuario.tipo_usuario);
                Debug.Log("Progreso: " + progreso[0]);
                Debug.Log("Financiamiento: " + progreso[0].financiamiento); 
                
                Debug.Log("Datos del usuario obtenidos correctamente.");
            }
            else
            {
                Debug.Log("Error al obtener los datos del usuario: " + message);
                Debug.Log("Usando datos dummy" );
            // datos dummy
            usuario.usuario = "User";
            usuario.tipo_usuario = "Agricultor";
            usuario.id = 1;
            progreso = new List<Progreso> { new() { financiamiento = 1 } };
            semillas = new List<Semilla> { new() { maiz = 10, trigo = 10, chile = 10, tomate = 10 } };
            }
        }
        else
        {
            
            Debug.Log("Usando datos dummy" );
            // datos dummy
            usuario.usuario = "User";
            usuario.tipo_usuario = "Agricultor";
            usuario.id = 1;
            progreso = new List<Progreso> { new() { financiamiento = 1 } };
            semillas = new List<Semilla> { new() { maiz = 10, trigo = 10, chile = 10, tomate = 10 } };
        }
    }
}

// Clases para deserializar la respuesta JSON
[System.Serializable]
public class DatosUsuario
{
    public bool success;
    public string message;
    public Usuario usuario;
    public List<Progreso> progreso;
    public List<Semilla> semillas;
    public List<Cultivo> cultivos;
}

[System.Serializable]
public class Usuario
{
    public int id;
    public string tipo_usuario;
    public string usuario;
}

[System.Serializable]
public class Progreso
{
    public int id;
    public int seguro;
    public int ciclo;
    public string practica;
    public float dinero;
    public int financiamiento;
}

[System.Serializable]
public class Semilla
{
    public int id;
    public int maiz;
    public int trigo;
    public int chile;
    public int tomate;
}

[System.Serializable]
public class Cultivo
{
    public int id;
    public string hora_plantacion;
    public string estado;
    public string semilla;
    public float posx;
    public float posy;
}
