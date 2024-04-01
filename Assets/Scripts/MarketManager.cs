using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static System.Random random = new System.Random();
    private Dictionary<int, int> crop_prices;
    private Dictionary<int, int> crop_quantity;
    public FinanceManager financeManager;

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
    }

    public int GetCantidad(int cropType){
        return crop_quantity[cropType];
    }

    public double GetTotal(int cropType){
        return financeManager._prices[cropType] * GetCantidad(cropType) * 0.8;
    }

    public void UpdateCropQuantity(int cropType, int quantity){
        crop_quantity[cropType] = quantity;
    }
}
