using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class FinanceManager : MonoBehaviour
{
    private int _userId;
    private Dictionary<int, int> _prices;
    private int _financiamiento;

    private void Awake()
    {
        _prices = new Dictionary<int, int>()
        {
            {1, 200},
            {2, 300},
            {3, 400},
            {4, 500},
            {5, 600},
            {6, 700}
        };

        // Iniciar la coroutine para obtener el financiamiento del usuario
        StartCoroutine(ObtenerFinanciamiento());
    }

    public int SellItem(int cropType, int quantity)
    {
        if (_prices.ContainsKey(cropType))
        {
            return _prices[cropType] * quantity;
        }
        return 0;
    }

    public void SetUserId(int userId)
    {
        _userId = userId;
    }

    public int GetFinanciamiento()
    {
        return _financiamiento;
    }

    private IEnumerator ObtenerFinanciamiento()
    {
        // Obtener el financiamiento del usuario 
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/Verqor/api/apiFinanciamiento.php?user_id=" + _userId);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Parsear el resultado JSON y obtener el financiamiento
            string jsonString = www.downloadHandler.text;
            FinanciamientoData financiamientoData = JsonUtility.FromJson<FinanciamientoData>(jsonString);
            _financiamiento = financiamientoData.financiamiento;

            // Llamar al método UpdateFinanciamiento para actualizar el financiamiento
            UpdateFinanciamiento(_financiamiento);
        }
        else
        {
            Debug.LogError("Error al obtener el financiamiento: " + www.error);
        }
    }

    public void UpdateFinanciamiento(int financiamiento)
    {
        print("Financiamiento: " + financiamiento);
        switch (financiamiento)
        {
            case 1:
                // Acciones para el financiamiento de Verqor
                Debug.Log("Financiamiento de Verqor");
                break;
            case 2:
                // Acciones para el financiamiento de Banco
                Debug.Log("Financiamiento de Banco");
                break;
            case 3:
                // Acciones para el financiamiento de Coyote
                Debug.Log("Financiamiento de Coyote");
                break;
            default:
                // Acciones por defecto o para otros tipos de financiamiento
                Debug.Log("Financiamiento desconocido");
                break;
        }
    }
}

[System.Serializable]
public class FinanciamientoData
{
    public int financiamiento;
}
