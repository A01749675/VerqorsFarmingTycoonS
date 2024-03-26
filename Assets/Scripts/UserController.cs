using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    Dictionary<string,int> user_data;
    Dictionary<int,string> achievements;

    private void Awake()
    {
        user_data = new Dictionary<string, int>()
        {
            {"user_id", 0},
            {"financiamiento", 0},
            {"capital",0},
            {"creditos",0},
            {"deudas",0},
            {"cosechas",0},
            {"ventas",0},
            {"ganancias",0},
            {"perdidas",0}
        };
    }

    void updateUserData(string key, int value){
        user_data[key] = value;
    }
}
