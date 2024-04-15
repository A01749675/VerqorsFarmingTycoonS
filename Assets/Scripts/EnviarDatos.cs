using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class EnviarDatos : MonoBehaviour
{
    public ObtenerDatos obtenerDatos;

    public UserController userController;
    public MapManager mapManager;
    public CropManager cropManager;

    private Cosecha cosecha;
    private Semilla semilla;
    private Progreso progreso;
    private List<Parcela> parcelas_data;

    public void GetDataFromCodes(){
        int trigo = cropManager.GetCropQuantity(1);
        int maiz = cropManager.GetCropQuantity(2);
        int tomate = cropManager.GetCropQuantity(3);
        int chile = cropManager.GetCropQuantity(6);
        int trigo_seed = cropManager.GetCropSeeds(1);
        int maiz_seed = cropManager.GetCropSeeds(2);
        int tomate_seed = cropManager.GetCropSeeds(3);
        int chile_seed = cropManager.GetCropSeeds(6);
        int ciclo = mapManager.GetCurrentCycle();
        int capital = userController.GetParameter("capital");

        List<List<int>> parcelas_raw = mapManager.SaveDataFromMap();
        parcelas_data = new List<Parcela>();

        foreach(List<int> parcela in parcelas_raw){
            Parcela parcela_data = new Parcela();
            parcela_data.id = parcela[0];
            parcela_data.estado = parcela[1];
            parcela_data.cantidad = parcela[2];
            parcela_data.agua = parcela[3];
            parcelas_data.Add(parcela_data);
        }

        cosecha = new Cosecha();
        cosecha.trigo = trigo;
        cosecha.maiz = maiz;
        cosecha.tomate = tomate;
        cosecha.chile = chile;

        semilla = new Semilla();
        semilla.trigo = trigo_seed;
        semilla.maiz = maiz_seed;
        semilla.tomate = tomate_seed;
        semilla.chile = chile_seed;

        progreso = new Progreso();
        progreso.id_usuario = userController.GetParameter("user_id");
        progreso.dinero = capital;
        progreso.ciclo = ciclo;
        progreso.financiamiento = userController.GetParameter("financiamiento");

    }

    public void Guardar()
    {
        string url = Application.absoluteURL;
        int userId = ObtenerUserIdDeURL(url);

        if (userId == -1)
        {
            //Debug.LogError("No se encontró el parámetro 'user_id' en la URL.");
            return;
        }

        // Crear el objeto JSON con los datos del usuario
        string jsonData = CrearJSON(userId, obtenerDatos.progreso, obtenerDatos.semillas, obtenerDatos.cosecha, obtenerDatos.parcela);

        // Enviar los datos a la base de datos
        StartCoroutine(EnviarDatosUsuario(jsonData));
    }

    // corrutina de enviar datos
    private IEnumerator EnviarDatosUsuario(string jsonData)
    {
        string apiUrl = "http://localhost:3000/game-data";

        // Crear un objeto UnityWebRequest para enviar los datos
        UnityWebRequest www = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Enviar la petición
        yield return www.SendWebRequest();

        // Verificar si la petición fue exitosa
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al enviar los datos: " + www.error);
        }
        else
        {
            Debug.Log("Datos enviados correctamente.");
        }
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

    private string CrearJSON(int userId, List<Progreso> progreso, List<Semilla> semillas, List<Cosecha> cosecha, List<Parcela> parcela)
    {
        // Crear un objeto DatosUsuario con los datos del usuario
        DatosUsuario userData = new()
        {
            user_id = userId,
            progreso = progreso,
            semillas = semillas,
            cosecha = cosecha,
            parcela = parcela
        };

        // Convertir el objeto a JSON
        string jsonOutput = JsonUtility.ToJson(userData);
        Debug.Log("JSON generado: " + jsonOutput);
        return jsonOutput;
    }
}
