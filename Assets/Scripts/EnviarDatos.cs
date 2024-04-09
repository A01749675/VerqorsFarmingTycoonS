using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class EnviarDatos : MonoBehaviour
{
    public ObtenerDatos obtenerDatos;

    private void Start()
    {
        string url = Application.absoluteURL;
        int userId = ObtenerUserIdDeURL(url);

        if (userId == -1)
        {
            Debug.LogError("No se encontró el parámetro 'user_id' en la URL.");
            return;
        }

        // Crear el objeto JSON con los datos del usuario
        string jsonData = CrearJSON(userId, obtenerDatos.progreso, obtenerDatos.semillas, obtenerDatos.cultivos);

        // Enviar los datos a la base de datos
        //StartCoroutine(EnviarDatosUsuario(jsonData));
    }

    private int ObtenerUserIdDeURL(string url)
    {
        int userId = -1;
        int startIndex = url.IndexOf("user_id=");
        if (startIndex != -1)
        {
            string userIdStr = url.Substring(startIndex + 8); // Sumar 8 para ignorar 'user_id='
            if (int.TryParse(userIdStr, out userId))
            {
                Debug.Log("user_id obtenido de la URL: " + userId);
            }
        }
        return userId;
    }

    private string CrearJSON(int userId, List<Progreso> progreso, List<Semilla> semillas, List<Cultivo> cultivos)
    {
        // Crear un objeto DatosUsuario con los datos del usuario
        DatosUsuario userData = new()
        {
            user_id = userId,
            progreso = progreso,
            semillas = semillas,
            cultivos = cultivos
        };

        // Convertir el objeto a JSON
        string jsonOutput = JsonUtility.ToJson(userData);
        Debug.Log("JSON generado: " + jsonOutput);
        return jsonOutput;
    }
}
