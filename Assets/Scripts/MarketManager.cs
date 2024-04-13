using System;
using System.Collections;
using System.Collections.Generic;
using DPUtils.Systems.DateTime;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static System.Random random = new System.Random();
    private Dictionary<int, int> crop_prices;
    private Dictionary<int, int> crop_quantity;
    public FinanceManager financeManager;
    public TimeManager timeManager;

    private bool flag1;

    private void Awake(){
        crop_prices = financeManager.GetPrices();
        crop_quantity = new Dictionary<int, int>(){
            {1, random.Next(1, 500)},
            {2, random.Next(1, 500)},
            {3, random.Next(1, 500)},
            {4, random.Next(1, 500)},
            {5, random.Next(1, 500)},
            {6, random.Next(1, 500)}

        };
        flag1 = false;
    }

    public int GetCantidad(int cropType){
        return crop_quantity[cropType];
    }

    private int GetDate(){
        int week = timeManager.GetWeek();
        return week;
    }

    public double GetTotal(int cropType){
        return financeManager._prices[cropType] * GetCantidad(cropType) * 0.5;
    }

    public void UpdateCropQuantity(int cropType, int quantity){
        crop_quantity[cropType] = quantity;
    }

    private void Update(){
        int week1 = GetDate();
        if(week1%2 == 0 && !flag1){
            flag1 = true;
            print("Cambiando cantidades");
            crop_quantity = new Dictionary<int, int>(){
                {1, random.Next(1, 500)},
                {2, random.Next(1, 500)},
                {3, random.Next(1, 500)},
                {4, random.Next(1, 500)},
                {5, random.Next(1, 500)},
                {6, random.Next(1, 500)}
            };
        }
        else if (week1%2 != 0){
            flag1 = false;
        }
    }
}
