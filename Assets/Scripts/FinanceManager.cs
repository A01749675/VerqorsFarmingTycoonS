using UnityEngine;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
public class FinanceManager : MonoBehaviour
{
    private int _userId;
    public Dictionary<int, int> _prices;
    private int _financiamiento;

    public UserController user_controller; //Referencia al UserController
    public CropManager crop_manager; //Referencia al CropManager
    public ObtenerDatos obtener_datos; //Referencia al ObtenerDatos

    public MapManager mapManager; //Referencia al MapManager

    public EnviarDatos enviarDatos; //Referencia al EnviarDatos

    
    private Dictionary<string, float> verqorFinanceData; //Datos de financiamiento de Verqor
    private Dictionary<string, float> bancoFinanceData; //Datos de financiamiento de Banco
    private Dictionary<string, float> coyoteFinanceData; //Datos de financiamiento de Coyote
    private AudioSource audioSourcemoney; //Sonido de dinero

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
        verqorFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.5f},
            {"plazo", 2900},
            {"montoMaximo", 100000000},
            {"seguro", 0.5f}
        };
        bancoFinanceData = new Dictionary<string, float>()
        {
            {"tasaInteres", 0.35f},
            {"plazo", 2900},
            {"montoMaximo", 800000},
            {"seguro", 0.0f}
        };
        coyoteFinanceData = new Dictionary<string, float>()
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
        //Si ya ha pasado el plazo y no se ha pagado con anterioridad 
        if(cycle%plazo==0 && !flag &&!IsPaid){
            //print("Financiamiento actualizado");

            int debt = user_controller.GetDebt(); //Obtiene la deuda del user controller

            //Checa si puede pagar la deuda
            if(user_controller.GetCapital()>debt){
                user_controller.PayDebt(debt); 
                IsPaid = true;
            }
            else{
                //Termina el juego
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

    //Método que regresa cuando se tiene que pagar
    public int GetTimetoPay(){
        int cycle = mapManager.GetCurrentCycle();
        int time = (int)((plazo - cycle%plazo)/8);
        return time;
    }
    //Método que calcula la deuda
    public int CalculateDebt(int fin){
        int debt = 0;
        switch(fin){
            case 1:
                debt = (int) (verqorFinanceData["montoMaximo"]*verqorFinanceData["tasaInteres"]+verqorFinanceData["montoMaximo"]);
                break;
            case 2:
                debt = (int) (bancoFinanceData["montoMaximo"]*bancoFinanceData["tasaInteres"]+bancoFinanceData["montoMaximo"]);
                break;
            case 3:
                debt = (int) (coyoteFinanceData["montoMaximo"]*coyoteFinanceData["tasaInteres"]+coyoteFinanceData["montoMaximo"]);
                break;
                
        }
        return debt;
    }

    //Método que establece el plazo de pago
    public void SetPlazo(){
        switch(user_controller.user_data["financiamiento"]){
            case 1:
                plazo = (int)verqorFinanceData["plazo"];
                break;
            case 2:
                plazo = (int)bancoFinanceData["plazo"];
                break;
            case 3:
                plazo = (int)coyoteFinanceData["plazo"];
                break;
        }
    }
    

    //Método regresa el seguro del financiamiento
    public float GetSeguro(int fin){
        switch(fin){
            case 1:
                return verqorFinanceData["seguro"];
            case 2:
                return bancoFinanceData["seguro"];
            case 3:
                return coyoteFinanceData["seguro"];
            default:
                return 0f;
        }
        
    }
    //Método que actualiza el seguro del banco
    public void UpdateSeguro(int mejora){
        switch(mejora){
            case 6:
                bancoFinanceData["seguro"] = 0.2f;
                break;
            case 12:
                bancoFinanceData["seguro"] = 0.3f;
                break;
            case 19:
                bancoFinanceData["seguro"] = 0.5f;
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
    //Método que vende un cultivo
    public void SellItem(int cropType, int quantity)
    {
        if (_prices.ContainsKey(cropType))
        {
            user_controller.UpdateCapital((int)(_prices[cropType] * quantity*dinero));
            crop_manager.UpdateCropQuantity(cropType, -quantity);
            audioSourcemoney.Play();
        }
    }
    //Método que vende un cultivo
    public void SellItem2(int cropType, int quantity)
    {
        if (_prices.ContainsKey(cropType))
        {
            user_controller.UpdateCapital((int)(_prices[cropType] * quantity * 1.3*dinero));
            crop_manager.UpdateCropQuantity(cropType, -quantity);
            audioSourcemoney.Play();
        }
    }
    //Método que obtiene el precio de un cultivo
    public int GetCropPrice(int cropType)
    {
        return _prices.ContainsKey(cropType) ? _prices[cropType] : 0;
    }

    //Método para mostrar el financiamiento
    public void UpdateFinanciamiento(int financiamiento)
    {
        //print("Financiamiento: " + financiamiento);
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
            int new_debt = (int)(((user_controller.GetParameter("deuda"))/2)*(1+verqorFinanceData["tasaInteres"])+user_controller.GetParameter("deuda"));
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
            int new_debt = (int)(((user_controller.GetParameter("deuda"))/2)*(1+bancoFinanceData["tasaInteres"])+user_controller.GetParameter("deuda"));
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
            int new_debt = (int)(((user_controller.GetParameter("deuda"))/2)*(1+coyoteFinanceData["tasaInteres"])+user_controller.GetParameter("deuda"));
            user_controller.SetParameter("deuda",new_debt);
        }
        else{
            print("GameOver");
        }
    }

}
