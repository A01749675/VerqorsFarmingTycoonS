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
        print("UserController Awake");
        user_data = new Dictionary<string, int>(){
            {"capital", 100000000},
            {"financiamiento", 1},
            {"deuda", 1500000},
            {"user_id", -1},
        };

        financeManager.SetPlazo();
        audioSourcemoney = GameObject.Find("CaChing").GetComponent<AudioSource>();
    }

    public void SetParameter(string param,int value){
        user_data[param] = value;
    }

    public int GetParameter(string param){
        return user_data[param];
    }

    public void UpdateCapital(int money){
        user_data["capital"]+=money;
        audioSourcemoney.Play();
    }
    public void PayDebt(int money){
        user_data["capital"]-=money;
        user_data["deuda"]-=money;
    }
    public int GetCapital(){
        return user_data["capital"];
    }

    public int GetDebt(){
        return user_data["deuda"];
    }

}
