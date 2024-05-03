using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public Dictionary<string,int> user_data;

    public FinanceManager financeManager;
    private AudioSource audioSourcemoney;

    private void Awake()
    {
        //print("UserController Awake");
        //Valores default
        user_data = new Dictionary<string, int>(){
            {"capital", 10000000},
            {"financiamiento", 1},
            {"deuda", 1500000},
            {"user_id", -1},
        };
        //Establece el plazo
        financeManager.SetPlazo();
        //Establece el sonido
        audioSourcemoney = GameObject.Find("CaChing").GetComponent<AudioSource>();
    }

    //Actualiza algún parametro del diccionario de datos de usuario
    public void SetParameter(string param,int value){
        user_data[param] = value;
    }
    //Recupera un problema del parámetro del diccionario de datos de usuario
    public int GetParameter(string param){
        return user_data[param];
    }
    //Actualiza el capital del usuario
    public void UpdateCapital(int money){
        user_data["capital"]+=money;
        audioSourcemoney.Play();
    }
    //Actualiza la deuda del usuario
    public void PayDebt(int money){
        user_data["capital"]-=money;
        user_data["deuda"]-=money;
    }
    //Recupera el capital del usuario
    public int GetCapital(){
        return user_data["capital"];
    }
    //Recupera la deuda del usuario
    public int GetDebt(){
        return user_data["deuda"];
    }

}
