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

    public UserController userController;

    private Dictionary<int,int> financiamiento_seguro;
    private Dictionary<string, float> VerqorFinanceData;
    private Dictionary<string, float> BancoFinanceData;
    private Dictionary<string, float> CoyoteFinanceData;

    private int loosingCondition = -1000000;
   
     private bool RegenerativeAgriculture = false;

    int current_finance = 1;

    private int seguro=0;

    public float dinero = 1.0f;


    

    private void Awake()
    {
        _prices = new Dictionary<int, int>()
        {
            {1, 30},
            {2, 32},
            {3, 40},
            {4, 45},
            {5, 31},
            {6, 34}
        };
        VerqorFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.5f},
            {"plazo", 12},
            {"montoMaximo", 300000},
            {"seguro", 0.5f}
        };
        BancoFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.3f},
            {"plazo", 24},
            {"montoMaximo", 200000},
            {"seguro", 0.0f}
        };
        CoyoteFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.75f},
            {"plazo", 6},
            {"montoMaximo", 50000},
            {"seguro", 0.0f}
        };



        // ObtenerFinanciamiento();
    }

    private void Update(){
        int cycle = mapManager.GetCurrentCycle();
        if(cycle%3650==0){
            print("Financiamiento actualizado");
            int debt = user_controller.GetDebt();

            if(user_controller.GetCapital()>debt){
                user_controller.PayDebt(debt);
                user_controller.UpdateCapital(-debt);
            }
            else{
                user_controller.PayDebt(user_controller.GetCapital());
                user_controller.UpdateCapital(-debt);
                if(user_controller.GetCapital()<loosingCondition){
                    print("GameOver");
                }
            }
        }
    }

    public int CalculateDebt(int fin){
        int debt = 0;
        switch(fin){
            case 1:
                debt = (int) (VerqorFinanceData["montoMaximo"]*VerqorFinanceData["tasaInteres"]+VerqorFinanceData["montoMaximo"]);
                break;
            case 2:
                debt = (int) (BancoFinanceData["montoMaximo"]*BancoFinanceData["tasaInteres"]+BancoFinanceData["montoMaximo"]);
                break;
            case 3:
                debt = (int) (CoyoteFinanceData["montoMaximo"]*CoyoteFinanceData["tasaInteres"]+CoyoteFinanceData["montoMaximo"]);
                break;
                
        }
        return debt;
    }

    public float TasaInteres(int fin){
        float tasa = 0;
        switch(fin){
            case 1:
                tasa = VerqorFinanceData["tasaInteres"];
                break;
            case 2:
                tasa = BancoFinanceData["tasaInteres"];
                break;
            case 3:
                tasa = CoyoteFinanceData["tasaInteres"];
                break;
        }
        return tasa;
    }

    public void UpdateSeguro(int mejora){
        switch(mejora){
            case 6:
                BancoFinanceData["seguro"] = 0.2f;
                break;
            case 12:
                BancoFinanceData["seguro"] = 0.3f;
                break;
            case 19:
                BancoFinanceData["seguro"] = 0.5f;
                break;
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
            user_controller.UpdateCapital((int)(_prices[cropType] * quantity*dinero));
            crop_manager.UpdateCropQuantity(cropType, -quantity);
        }
    }
    public void SellItem2(int cropType, int quantity)
    {
        if (_prices.ContainsKey(cropType))
        {
            user_controller.UpdateCapital((int)(_prices[cropType] * quantity * 1.3*dinero));
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

    public void SetFinanceParams(){

    }

    public void SetFinanceCurrent(int fin){
        current_finance = fin;
    }
}
