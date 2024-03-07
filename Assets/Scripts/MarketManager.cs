using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    private Dictionary<int, int> crop_prices;

    private void Awake(){
        crop_prices = new Dictionary<int, int>(){
            {1,60}, //cebada
            {2,62}, //maiz
            {3,70}, //tomate
            {4,140}, //aguacate
            {5,60}, //café
            {6,96} //chile
        };
    
    }

    public int GetPrice(int cropType){
        return crop_prices[cropType];
    }
}
