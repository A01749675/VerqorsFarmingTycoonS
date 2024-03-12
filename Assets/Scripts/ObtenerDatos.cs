using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ObtenerDatos : MonoBehaviour
{
    public string phpScriptURL = "http://localhost:8080/Verqor/api/apiUsuario.php";
    public void ObtenerFinanciamiento()
    {
        StartCoroutine(ObtenerUsuarioYFinanciamiento());
    }
    private IEnumerator ObtenerUsuarioYFinanciamiento()
    {
        // Realizar la solicitud GET 
        UnityWebRequest usuarioRequest = UnityWebRequest.Get(phpScriptURL);
        yield return usuarioRequest.SendWebRequest();

        // Verificar si hubo algún error en la solicitud
        if (usuarioRequest.result == UnityWebRequest.Result.Success)
        {
            // Obtener el ID de usuario como un string
            string userIdString = usuarioRequest.downloadHandler.text;

            // Convertir el ID de usuario a un entero
            int userId;
            if (int.TryParse(userIdString, out userId))
            {
                StartCoroutine(ObtenerFinanciamientoDeUsuario(userId));
                print("ID de usuario obtenido: " + userIdString);
            }
            else
            {
                Debug.LogError("Error al convertir el ID de usuario: " + userIdString);
            }
        }
        else
        {
            Debug.LogError("Error al obtener el ID de usuario: " + usuarioRequest.error);
        }
    }

    private IEnumerator ObtenerFinanciamientoDeUsuario(int userId)
    {
        // Construir la URL completa con el ID de usuario como parámetro
        string url = phpScriptURL + "?user_id=" + userId;

        // Realizar la solicitud GET al script PHP
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // Verificar si hubo algún error en la solicitud
        if (www.result == UnityWebRequest.Result.Success)
        {
            // Obtener el financiamiento como un string JSON y convertirlo a un objeto JSON
            string jsonString = www.downloadHandler.text;
            
            // Verificar si el JSON contiene un mensaje de error
            if (jsonString.Contains("error"))
            {
                Debug.LogError("Error al obtener el financiamiento: " + jsonString);
            }
            else
            {
                FinanciamientoData financiamiento = JsonUtility.FromJson<FinanciamientoData>(jsonString);

                // Usar el financiamiento en tu juego
                Debug.Log("Financiamiento obtenido: " + financiamiento.financiamiento);

                // Llamar al método UpdateFinanciamiento del FinanceManager
                FindObjectOfType<FinanceManager>().UpdateFinanciamiento(financiamiento.financiamiento);
            }
        }
        else
        {
            Debug.LogError("Error al obtener el financiamiento: " + www.error);
        }
    }
}
