using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public Dictionary<string,int> user_data;
    public Dictionary<int,string> achievements;
    public Dictionary<int,bool> unlocked_achievements;

    public FinanceManager financeManager;
    private AudioSource audioSourcemoney;

    private void Awake()
    {
        print("UserController Awake");
        user_data = new Dictionary<string, int>(){
            {"capital", 1000000},
            {"financiamiento", 1},
            {"deuda", 1500000},
            {"user_id", -1},
        };

        achievements = new Dictionary<int, string>()
        {
            {1, "Primera cosecha"},
            {2, "Primer credito"},
            {3, "Primer venta"},
            {4, "Primer ganancia"},
            {5, "Primer perdida"},
            {6, "Primer deuda"},
            {7, "Primer financiamiento"},
            {8, "Primera compra"}
        };
        unlocked_achievements = new Dictionary<int, bool>()
        {
            {1, false},
            {2, false},
            {3, false},
            {4, false},
            {5, false},
            {6, false},
            {7, false},
            {8, false}
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
