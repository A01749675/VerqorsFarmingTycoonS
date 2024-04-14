using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public Dictionary<string,int> user_data;
    public Dictionary<int,string> achievements;
    public Dictionary<int,bool> unlocked_achievements;

    private void Awake()
    {
        user_data = new Dictionary<string, int>()
        {
            {"user_id", 0},
            {"financiamiento", 0},
            {"capital",0},
            {"creditos",0},
            {"deudas",3000},
            {"cosechas",0},
            {"ventas",0},
            {"ganancias",0},
            {"perdidas",0}
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
    }

    void SetParameter(string param,int value){
        user_data[param] = value;
    }

    public void UpdateCapital(int money){
        user_data["capital"]+=money;
    }
    public void PayDebt(int money){
        user_data["capital"]-=money;
        user_data["deudas"]-=money;
    }
    public int GetCapital(){
        return user_data["capital"];
    }

    public void UpdateCrops(int crops){
        user_data["cosechas"]+=crops;
    }

    public int GetDebt(){
        return user_data["deudas"];
    }

}
