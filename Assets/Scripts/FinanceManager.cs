using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinanceManager : MonoBehaviour
{

    private Dictionary<int,int> _prices;

    private void  Awake()
    {
        _prices = new Dictionary<int, int>(){
            {1, 200},
            {2, 300},
            {3, 400},
            {4, 500},
            {5, 600},
            {6, 700}
        };
    }
    
    public int SellItem(int crop_type, int quantity){

        if(_prices.ContainsKey(crop_type)){
            return _prices[crop_type] * quantity;
        }
        return 0;
    }

    }



