using UnityEngine;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
public class FinanceManager : MonoBehaviour
{
    private int _userId;
    public Dictionary<int, int> _prices;
    private int _financiamiento;

    public UserController user_controller;
    public CropManager crop_manager;
    public ObtenerDatos obtener_datos;

    public MapManager mapManager;

    public EnviarDatos enviarDatos;

    private Dictionary<int,int> financiamiento_seguro;
    private Dictionary<string, float> VerqorFinanceData;
    private Dictionary<string, float> BancoFinanceData;
    private Dictionary<string, float> CoyoteFinanceData;
    private AudioSource audioSourcemoney;

    private int loosingCondition = -1;
   

    public float dinero = 1.0f;
    private bool flag=false;
    public int plazo = 1000;

    public bool IsPaid = false;


    

    private void Awake()
    {
        _prices = new Dictionary<int, int>()
        {
            {1, 30},
            {2, 34},
            {3, 40},
            {4, 44},
            {5, 26},
            {6, 50}
        };
        VerqorFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.5f},
            {"plazo", 2900},
            {"montoMaximo", 100000000},
            {"seguro", 0.5f}
        };
        BancoFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.35f},
            {"plazo", 2900},
            {"montoMaximo", 800000},
            {"seguro", 0.0f}
        };
        CoyoteFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.75f},
            {"plazo", 2900},
            {"montoMaximo", 800000},
            {"seguro", 0.0f}
        };
        audioSourcemoney = GameObject.Find("CaChing").GetComponent<AudioSource>();


        // ObtenerFinanciamiento();
    }

    private void Update(){
        int cycle = mapManager.GetCurrentCycle();
        if(cycle%plazo==0 && !flag &&!IsPaid){
            print("Financiamiento actualizado");
            int debt = user_controller.GetDebt();

            if(user_controller.GetCapital()>debt){
                user_controller.PayDebt(debt);
                IsPaid = true;
            }
            else{
                user_controller.PayDebt(debt);
                if(user_controller.GetCapital()<loosingCondition){
                    print("GameOver");
                    enviarDatos.GuardarySalir();
                    Application.OpenURL("https://verqor.com/");
                    SceneManager.LoadScene("GameOver");
                }
            }
            flag=true;
        } 
        if (cycle%100!=0){
            flag=false;
        }
    }

    public int GetTimetoPay(){
        int cycle = mapManager.GetCurrentCycle();
        int time = (int)((plazo - cycle%plazo)/8);
        return time;
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


    public void SetPlazo(){
        switch(user_controller.user_data["financiamiento"]){
            case 1:
                plazo = (int)VerqorFinanceData["plazo"];
                break;
            case 2:
                plazo = (int)BancoFinanceData["plazo"];
                break;
            case 3:
                plazo = (int)CoyoteFinanceData["plazo"];
                break;
        }
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

    public float GetSeguro(int fin){
        switch(fin){
            case 1:
                return VerqorFinanceData["seguro"];
            case 2:
                return BancoFinanceData["seguro"];
            case 3:
                return CoyoteFinanceData["seguro"];
            default:
                return 0f;
        }
        
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
            audioSourcemoney.Play();
        }
    }
    public void SellItem2(int cropType, int quantity)
    {
        if (_prices.ContainsKey(cropType))
        {
            user_controller.UpdateCapital((int)(_prices[cropType] * quantity * 1.3*dinero));
            crop_manager.UpdateCropQuantity(cropType, -quantity);
            audioSourcemoney.Play();
        }
    }

    public int GetCropPrice(int cropType)
    {
        return _prices.ContainsKey(cropType) ? _prices[cropType] : 0;
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


    public void PathWays(int debt,int cycle){
        int financiamiento = user_controller.GetParameter("financiamiento");
        switch(financiamiento){
            case 1:
                VerqorPathway(debt,cycle);
                break;
            case 2:
                BankPathway(debt,cycle);
                break;
            case 3: 
                CoyotePathway(debt,cycle);
                break;
        }
    }
    private void VerqorPathway(int debt,int cycle){
        
        if(user_controller.GetParameter("capital")>user_controller.GetParameter("deuda")){
            user_controller.PayDebt(user_controller.GetParameter("deuda"));
        }
        else if(user_controller.GetParameter("capital")>(user_controller.GetParameter("deuda"))/2){
            user_controller.UpdateCapital(-user_controller.GetParameter("deuda"));
            int new_debt = (int)(((user_controller.GetParameter("deuda"))/2)*(1+VerqorFinanceData["tasaInteres"])+user_controller.GetParameter("deuda"));
            user_controller.SetParameter("deuda",new_debt);
        }
        else{
            print("GameOver");
        }
            
    }
    private void BankPathway(int debt,int cycle){
        if(user_controller.GetParameter("capital")>user_controller.GetParameter("deuda")){
            user_controller.PayDebt(user_controller.GetParameter("deuda"));
        }
        else if(user_controller.GetParameter("capital")>(user_controller.GetParameter("deuda"))/2){
            user_controller.UpdateCapital(-user_controller.GetParameter("deuda"));
            int new_debt = (int)(((user_controller.GetParameter("deuda"))/2)*(1+BancoFinanceData["tasaInteres"])+user_controller.GetParameter("deuda"));
            user_controller.SetParameter("deuda",new_debt);
        }
        else{
            print("GameOver");
        }
    }
    private void CoyotePathway(int debt,int cycle){
        if(user_controller.GetParameter("capital")>user_controller.GetParameter("deuda")){
            user_controller.PayDebt(user_controller.GetParameter("deuda"));
        }
        else if(user_controller.GetParameter("capital")>(user_controller.GetParameter("deuda"))/2){
            user_controller.UpdateCapital(-user_controller.GetParameter("deuda"));
            int new_debt = (int)(((user_controller.GetParameter("deuda"))/2)*(1+CoyoteFinanceData["tasaInteres"])+user_controller.GetParameter("deuda"));
            user_controller.SetParameter("deuda",new_debt);
        }
        else{
            print("GameOver");
        }
    }

}
