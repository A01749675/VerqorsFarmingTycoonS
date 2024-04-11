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
    public string usuario;
    public string tipo_usuario;
    public List<Progreso> progreso;
    public List<Semilla> semillas;
    public List<Cosecha> cosechas;
    public List<Parcela> parcelas;

    private void Awake()
    {
        StartCoroutine(ObtenerIdUsuario());
    }

    private IEnumerator ObtenerIdUsuario()
    {
        // Obtener la URL absoluta de la aplicación
        string url = Application.absoluteURL;

        // Obtener el 'user_id' desde la URL
        int startIndex = url.IndexOf("user_id=");
        if (startIndex != -1)
        {
            string userIdStr = url.Substring(startIndex + 8); // Sumar 8 para ignorar 'user_id='
            if (int.TryParse(userIdStr, out user_id))
            {
                Debug.Log("user_id obtenido de la URL: " + user_id);
            }
            else
            {
                Debug.LogError("No se pudo convertir el 'user_id' de la URL a entero.");
                yield break;
            }
        }
        else
        {
            //Debug.LogError("El parámetro 'user_id' no se encontró en la URL.");
            yield break;
        }

        StartCoroutine(ObtenerDatosUsuario(user_id));
    }

    private IEnumerator ObtenerDatosUsuario(int userId)
    {
        string apiUrl = "http://localhost:3000/game-data?user_id=" + userId;

        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonString = www.downloadHandler.text;
            DatosUsuario datosUsuario = JsonUtility.FromJson<DatosUsuario>(jsonString);

            success = datosUsuario.success;
            message = datosUsuario.message;
            usuario = datosUsuario.usuario;
            tipo_usuario = datosUsuario.tipo_usuario;
            progreso = datosUsuario.progreso;
            semillas = datosUsuario.semillas;
            cosechas = datosUsuario.cosechas;
            parcelas = datosUsuario.parcelas;
            

            if (success)
            {
                Debug.Log("Nombre de usuario: " + usuario);
                Debug.Log("Tipo de usuario: " + tipo_usuario);
                Debug.Log("Financiamiento: " + progreso[0].financiamiento);

                Debug.Log("Datos del usuario obtenidos correctamente.");
            }
            else
            {
                Debug.Log("Error al obtener los datos del usuario: " + message);
            }
        }
        else
        {
            Debug.LogError("Error al obtener los datos del usuario: " + www.error);
        }
    }
}

// Clases para deserializar la respuesta JSON
[System.Serializable]
public class DatosUsuario
{
    public bool success;
    public string message;
    public int user_id;
    public string usuario;
    public string tipo_usuario;
    public List<Progreso> progreso;
    public List<Semilla> semillas;
    public List<Cosecha> cosechas;
    public List<Parcela> parcelas;
}

[System.Serializable]
public class Progreso
{
    public int id;
    public int id_usuario;
    public float dinero;
    public int ciclo;
    public int seguro;
    public string practica;
    public int financiamiento;
}

[System.Serializable]
public class Semilla
{
    public int id;
    public int id_progreso;
    public int maiz;
    public int trigo;
    public int tomate;
    public int chile;
}

public class Cosecha
{
    public int id;
    public int id_progreso;
    public int maiz;
    public int trigo;
    public int tomate;
    public int chile;
}

[System.Serializable]
public class Parcela
{
    public int id_progreso;
    public int id_parcela;
    public int estado;
    public int cantidad;
    public int agua;
}