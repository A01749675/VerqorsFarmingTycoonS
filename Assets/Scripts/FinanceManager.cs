using UnityEngine;
using System.Collections.Generic;
public class FinanceManager : MonoBehaviour
{
    private int _userId;
    public Dictionary<int, int> _prices;
    private int _financiamiento;

    public UserController user_controller;
    public CropManager crop_manager;
    public ObtenerDatos obtener_datos;

    public MapManager mapManager;

    private Dictionary<string, float> VerqorFinanceData;
    private Dictionary<string, float> BancoFinanceData;
    private Dictionary<string, float> CoyoteFinanceData;

    

    private void Awake()
    {
        _prices = new Dictionary<int, int>()
        {
            {1, 60},
            {2, 62},
            {3, 70},
            {4, 140},
            {5, 61},
            {6, 96}
        };
        VerqorFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.5f},
            {"plazo", 12},
            {"montoMaximo", 300000}
        };
        BancoFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.3f},
            {"plazo", 24},
            {"montoMaximo", 200000}
        };
        CoyoteFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.75f},
            {"plazo", 6},
            {"montoMaximo", 50000}
        };

        // ObtenerFinanciamiento();
    }

    private void Update(){
        int cycle = mapManager.GetCurrentCycle();
        if(cycle%3650==0){
            print("Financiamiento actualizado");
            int fin = 0;
            int debt = 0;
            switch(fin){
                case 1:
                    debt = (int) (VerqorFinanceData["montoMaximo"]*VerqorFinanceData["tasaInteres"]);
                    break;
                case 2:
                    debt = (int) (BancoFinanceData["montoMaximo"]*BancoFinanceData["tasaInteres"]);
                    break;
                case 3:
                    debt = (int) (CoyoteFinanceData["montoMaximo"]*CoyoteFinanceData["tasaInteres"]);
                    break;
                    
            }
            if(user_controller.GetCapital()>debt){
                user_controller.PayDebt(debt);
                user_controller.UpdateCapital(-debt);
            }
            else{
                user_controller.PayDebt(user_controller.GetCapital());
                user_controller.UpdateCapital(-debt);
                if(user_controller.GetCapital()<-1000000){
                    print("GameOver");
                }
            }
        }
    }
    private void ObtenerFinanciamiento()
    {
        print("Datos progreso: " + obtener_datos.progreso);
        if (obtener_datos.progreso != null && obtener_datos.progreso.Count > 0)
        {
            _userId = obtener_datos.user_id;
            _financiamiento = obtener_datos.progreso[0].financiamiento;
            // Llamar al método UpdateFinanciamiento
            UpdateFinanciamiento(_financiamiento);
        }
    }
    public void SellItem(int cropType, int quantity)
    {
        if (_prices.ContainsKey(cropType))
        {
            user_controller.UpdateCapital(_prices[cropType] * quantity);
            crop_manager.UpdateCropQuantity(cropType, -quantity);
        }
    }

    public int GetCropPrice(int cropType)
    {
        return _prices.ContainsKey(cropType) ? _prices[cropType] : 0;
    }

    public int GetFinanciamiento()
    {
        return _financiamiento;
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

    public Dictionary<int, int> GetPrices()
    {
        return _prices;
    }
}
