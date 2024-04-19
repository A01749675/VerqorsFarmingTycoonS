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
    public List<Cosecha> cosecha;
    public List<Parcela> parcela;
    public List<Mejoras> mejoras;

    public List<List<int>> parcela_data = new List<List<int>>();

    public MapManager mapManager;

    [SerializeField]
    private UserController userController;
    [SerializeField]
    private FinanceManager financeManager;
    [SerializeField]
    private CropManager cropManager;
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
        string apiUrl = "http://localhost:3000/game-data?user_id="+userId;

        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonString = www.downloadHandler.text;
            print(jsonString);
            DatosUsuario datosUsuario = JsonUtility.FromJson<DatosUsuario>(jsonString);

            success = datosUsuario.success;
            message = datosUsuario.message;
            usuario = datosUsuario.usuario;
            tipo_usuario = datosUsuario.tipo_usuario;
            progreso = datosUsuario.progreso;
            semillas = datosUsuario.semillas;
            cosecha = datosUsuario.cosecha;
            parcela = datosUsuario.parcela;  
            mejoras = datosUsuario.mejoras;
                      

            if (success)
            {
                Debug.Log("Nombre de usuario: " + usuario);
                Debug.Log("Tipo de usuario: " + tipo_usuario);
                Debug.Log("Financiamiento: " + progreso[0].financiamiento);
                Debug.Log("Maíz semillas: " + semillas[0].maiz);
                Debug.Log("Parcela" + parcela[0]);
                print("Hola buenas soy MarzyParcela2 "+parcela.ToString());
                
                GetParcelaData(parcela);
                print("Dato usuario");
                SetUserData(progreso[0]);
                print("Progreso");
                SetSemillas(semillas[0]);
                print("Semillas");
                SetCosecha(cosecha[0]);
                print("Cosecha");

                Debug.Log("Datos del usuario obtenidos correctamente.");
            }
            else
            {
                SetToDefault();
                Debug.Log("Error al obtener los datos del usuario: " + message);
            }
        }
        else
        {
            Debug.LogError("Error al obtener los datos del usuario: " + www.error);
        }
    }
    public void GetParcelaData(List<Parcela> parcelas)
    {
        List<List<int>> parcela = new List<List<int>>();
        foreach (Parcela p in parcelas)
        {
            List<int> data = new List<int>();
            data.Add(p.id_parcela);
            data.Add(p.estado);
            data.Add(p.cantidad);
            data.Add(p.agua);
            parcela.Add(data);
        }
        print("Parcelas obtenidas: " +parcela.Count);
        mapManager.LoadDataFromMap(parcela);

    }
    private void SetUserData(Progreso progreso){
        userController.user_data["user_id"]=progreso.id_usuario;
        userController.user_data["capital"]=(int) progreso.dinero;
        userController.user_data["financiamiento"]=progreso.financiamiento;
        userController.user_data["deuda"]=financeManager.CalculateDebt(progreso.financiamiento);
        mapManager.SetCycle(progreso.ciclo);
    }

    private void SetSemillas(Semilla semilla){
        cropManager.crop_seeds[1]=semilla.trigo;
        cropManager.crop_seeds[2]=semilla.maiz;
        cropManager.crop_seeds[3]=semilla.tomate;
        cropManager.crop_seeds[6]=semilla.chile;
        cropManager.crop_seeds[4]=0;
        cropManager.crop_seeds[5]=0;
        
    }

    private void SetToDefault(){
        userController.user_data["user_id"]=-1;;
        userController.user_data["capital"]=0;
        userController.user_data["financiamiento"]=1;
        userController.user_data["deuda"]=0;
        cropManager.crop_quantity = new Dictionary<int, int>(){
            {1,10},
            {2,0},
            {3,0},
            {4,0},
            {5,0},
            {6,0}
        };
        cropManager.crop_seeds = new Dictionary<int, int>(){
            {1,500},
            {2,500},
            {3,500},
            {4,500},
            {5,500},
            {6,500}
        };

    }

    private void SetCosecha(Cosecha cosecha){
        cropManager.crop_quantity[1]=cosecha.trigo;
        cropManager.crop_quantity[2]=cosecha.maiz;
        cropManager.crop_quantity[3]=cosecha.tomate;
        cropManager.crop_quantity[6]=cosecha.chile;
        cropManager.crop_quantity[4]=0;
        cropManager.crop_quantity[5]=0;
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
    public List<Cosecha> cosecha;
    public List<Parcela> parcela;
    public List<Mejoras> mejoras;
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
    public int aguacate;
    public int frijol;
}
[System.Serializable]
public class Cosecha
{
    public int id;
    public int id_progreso;
    public int trigo;
    public int tomate;
    public int chile;
    public int maiz;
    public int aguacate;
    public int frijol;
}

[System.Serializable]
public class Parcela
{
    public int id;
    public int id_progreso;
    public int id_parcela;
    public int estado;
    public int cantidad;
    public int agua;
}

[System.Serializable]
public class Mejoras{
    public int id;
    public int id_progreso;
    public int id_mejora;
    public bool estado;
}