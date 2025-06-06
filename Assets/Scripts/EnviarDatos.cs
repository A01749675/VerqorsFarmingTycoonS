using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

/* Envía los datos del progreso del usuario a la base de datos a partir de métodos get.
Autores:  Santiago Chevez Trejo, Carlos Iker Fuentes Reyes, 
          Alma Teresa Carpio Revilla, Mariana Marzyani Hernandez Jurado, 
          y Alan Rodrigo Vega Reza */

public class EnviarDatos : MonoBehaviour
{
    public ObtenerDatos obtenerDatos; // Referencia al script ObtenerDatos
    public TreeManager treeManager; // Referencia al script TreeManager

    public UserController userController; // Referencia al script UserController
    private string url = "http://52.5.57.146:8080/";
    public MapManager mapManager; // Referencia al script MapManager
    public CropManager cropManager; // Referencia al script CropManager
    private Cosecha cosecha; // Estructura cosecha
    private Semilla semilla;  //Estructura semilla
    private Progreso progreso; //Estructura progreso
    private List<Parcela> parcelas_data; // Lista de la estructura parcela

    private List<Progreso> progreso_lista = new List<Progreso>(); // Lista de la estructura progreso
    private List<Semilla> semilla_lista = new List<Semilla>(); // Lista de la estructura semilla
    private List<Cosecha> cosecha_lista = new List<Cosecha>(); // Lista de la estructura cosecha
    private List<Mejoras> mejoras_lista = new List<Mejoras>(); // Lista de la estructura mejoras

    public void GetDataFromCodes()
    {
        //Obtiene mejoras
        int trigo = cropManager.GetCropQuantity(1);
        int maiz = cropManager.GetCropQuantity(2);
        int tomate = cropManager.GetCropQuantity(3);
        int chile = cropManager.GetCropQuantity(6);
        int aguacate = cropManager.GetCropQuantity(4);
        int frijol = cropManager.GetCropQuantity(5);
        int trigo_seed = cropManager.GetCropSeeds(1);
        int maiz_seed = cropManager.GetCropSeeds(2);
        int tomate_seed = cropManager.GetCropSeeds(3);
        int chile_seed = cropManager.GetCropSeeds(6);
        int aguacate_seed = cropManager.GetCropSeeds(4);
        int frijol_seed = cropManager.GetCropSeeds(5);
        int ciclo = mapManager.GetCurrentCycle();
        int capital = userController.GetParameter("capital");


        //Obtiene datos de las parcelas desde map manager
        List<List<int>> parcelas_raw = mapManager.SaveDataFromMap();
        //una lista de parcelas
        parcelas_data = new List<Parcela>();
        //Guarda los datos del mapmanager en la lista de parcelas
        foreach (List<int> parcela in parcelas_raw)
        {
            Parcela parcela_data = new Parcela();
            parcela_data.id_parcela = parcela[0];
            parcela_data.estado = parcela[1];
            parcela_data.cantidad = parcela[2];
            parcela_data.agua = parcela[3];
            parcelas_data.Add(parcela_data);
        }
        //guarda las mejoras
        Mejoras mejoras = new Mejoras();
        for (int i = 1; i <= 21; i++)
        {
            mejoras = new Mejoras();
            mejoras.id_mejora = i;
            mejoras.estado = treeManager.getMejoras(i);
            //print("Mejora " + i + " " + treeManager.getMejoras(i));
            mejoras_lista.Add(mejoras);
        }

        //Guarda los datos de la cosecha y semilla
        cosecha = new Cosecha();
        cosecha.trigo = trigo;
        cosecha.maiz = maiz;
        cosecha.tomate = tomate;
        cosecha.chile = chile;
        cosecha.aguacate = aguacate;
        cosecha.frijol = frijol;
        cosecha_lista.Add(cosecha);
        
        semilla = new Semilla();
        semilla.trigo = trigo_seed;
        semilla.maiz = maiz_seed;
        semilla.tomate = tomate_seed;
        semilla.chile = chile_seed;
        semilla.aguacate = aguacate_seed;
        semilla.frijol = frijol_seed;
        semilla_lista.Add(semilla);
        //Guarda los datos del progreso
        progreso = new Progreso();
        progreso.id_usuario = userController.GetParameter("user_id");
        progreso.dinero = capital;
        progreso.deuda = userController.GetParameter("deuda");
        progreso.ciclo = ciclo;
        progreso.financiamiento = userController.GetParameter("financiamiento");
        progreso.practica = obtenerDatos.progreso[0].practica;
        progreso.seguro = obtenerDatos.progreso[0].seguro;
        progreso_lista.Add(progreso);

    }

    public void Guardar()
    {
        //Establece la URL de la página
        string url = Application.absoluteURL;
        //Obtiene el id del usuario
        int userId = ObtenerUserIdDeURL(url);

        if (userId == -1)
        {
            //Debug.LogError("No se encontró el parámetro 'user_id' en la URL.");
            return;
        }
        //llama la función para obtener los datos
        GetDataFromCodes();

        // Crear el objeto JSON con los datos del usuario
        string jsonData = CrearJSON(userId, progreso_lista, semilla_lista, cosecha_lista, parcelas_data, mejoras_lista);

        // Enviar los datos a la base de datos
        StartCoroutine(EnviarDatosUsuario(jsonData));
    }
    public void GuardarySalir()
    {
        string url = Application.absoluteURL;
        int userId = ObtenerUserIdDeURL(url);

        if (userId == -1)
        {
            //Debug.LogError("No se encontró el parámetro 'user_id' en la URL.");
            return;
        }

        GetDataFromCodes();

        // Crear el objeto JSON con los datos del usuario
        string jsonData = CrearJSON(userId, progreso_lista, semilla_lista, cosecha_lista, parcelas_data, mejoras_lista);

        // Enviar los datos a la base de datos
        StartCoroutine(EnviarDatosUsuario2(jsonData));
    }

    // corrutina de enviar datos
    private IEnumerator EnviarDatosUsuario(string jsonData)
    {
        string apiUrl = url + "game-data";

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
    private IEnumerator EnviarDatosUsuario2(string jsonData)
    {
        string apiUrl = url+"game-data";

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
            /* Debug.Log("Datos enviados correctamente.");
            string redirectUrl = "http://52.5.57.146:8080/";
            UnityWebRequest www3 = UnityWebRequest.Get(redirectUrl);
            yield return www3.SendWebRequest(); */

            Application.OpenURL(url);
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

    private string CrearJSON(int userId, List<Progreso> progreso, List<Semilla> semillas, List<Cosecha> cosecha, List<Parcela> parcela, List<Mejoras> mejoras)
    {
        // Crear un objeto DatosUsuario con los datos del usuario
        DatosUsuario userData = new()
        {
            user_id = userId,
            progreso = progreso,
            semillas = semillas,
            cosecha = cosecha,
            parcela = parcela,
            mejoras = mejoras
        };

        // Convertir el objeto a JSON
        string jsonOutput = JsonUtility.ToJson(userData);
        Debug.Log("JSON generado: " + jsonOutput);
        return jsonOutput;
    }
}
