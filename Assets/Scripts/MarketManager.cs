using System;
using System.Collections;
using System.Collections.Generic;
using DPUtils.Systems.DateTime;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static System.Random random = new System.Random();

    private Dictionary<int, int> crop_quantity;
    private Dictionary<int, int> crop_quantity2;
    public FinanceManager financeManager;
    public TimeManager timeManager;

    private bool flag1;

    private void Awake(){
        crop_quantity = new Dictionary<int, int>(){
            {1, random.Next(200, 1000)},
            {2, random.Next(200, 1000)},
            {3, random.Next(200, 1000)},
            {4, random.Next(200, 1000)},
            {5, random.Next(200, 1000)},
            {6, random.Next(200, 1000)}

        };
        crop_quantity2= new Dictionary<int, int>(){
            {1, random.Next(200, 1000)},
            {2, random.Next(200, 1000)},
            {3, random.Next(200, 1000)},
            {4, random.Next(200, 1000)},
            {5, random.Next(200, 1000)},
            {6, random.Next(200, 1000)}
        };
        flag1 = false;
    }

    public int GetCantidad(int cropType){
        return crop_quantity[cropType];
    }

    public int GetCantidad2(int cropType){
        return crop_quantity2[cropType];
    }

    private int GetDate(){
        int week = timeManager.GetWeek();
        return week;
    }

    public double GetTotal(int cropType){
        return financeManager._prices[cropType] * GetCantidad(cropType) * 0.5;
    }
    public double GetTotal2(int cropType){
        return financeManager._prices[cropType] * crop_quantity2[cropType] * 0.3;
    }

    public void UpdateCropQuantity(int cropType, int quantity){
        crop_quantity[cropType] = quantity;
    }
    public void UpdateCropQuantity2(int cropType, int quantity){
        crop_quantity2[cropType] = quantity;
    }

    private void Update(){
        int week1 = GetDate();
        if(week1%2 == 0 && !flag1){
            flag1 = true;
            print("Cambiando cantidades");
            crop_quantity = new Dictionary<int, int>(){
                {1, random.Next(200, 1000)},
                {2, random.Next(200, 1000)},
                {3, random.Next(200, 1000)},
                {4, random.Next(200, 1000)},
                {5, random.Next(200, 1000)},
                {6, random.Next(200, 1000)}
            };
            crop_quantity2= new Dictionary<int, int>(){
                {1, random.Next(200, 1000)},
                {2, random.Next(200, 1000)},
                {3, random.Next(200, 1000)},
                {4, random.Next(200, 1000)},
                {5, random.Next(200, 1000)},
                {6, random.Next(200, 1000)}
            };
        }
        else if (week1%2 != 0){
            flag1 = false;
        }
    }
}
