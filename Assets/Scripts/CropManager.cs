using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Dictionary<int, int> crop_quantity = new Dictionary<int, int>(){
        {1,0},
        {2,0},
        {3,0},
        {4,0},
        {5,0}
    };
    private Dictionary<int, int> crop_seeds = new Dictionary<int, int>(){
        {1,0},
        {2,0},
        {3,0},
        {4,0},
        {5,0}
    };
    public void updateCropQuantity(int cropType, int quantity){
        crop_quantity[cropType] += quantity;
    }
    public int getCropQuantity(int cropType){
        return crop_quantity[cropType];
    }
    public void updateCropSeeds(int cropType, int quantity){
        if(crop_seeds[cropType]+quantity<=0){
            crop_seeds[cropType] += quantity;
        }
    }
    public int getCropSeeds(int cropType){
        return crop_seeds[cropType];
    }

}
