using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CropManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static int selected_crop;
    public Dictionary<int, int> crop_quantity;
    public Dictionary<int, int> crop_seeds;

    public Dictionary<int, int> crop_soil;
    private void Awake(){
        crop_quantity = new Dictionary<int, int>(){
            {1,10},
            {2,0},
            {3,0},
            {4,0},
            {5,0},
            {6,0}
        };
        crop_seeds = new Dictionary<int, int>(){
            {1,10},
            {2,0},
            {3,0},
            {4,0},
            {5,0},
            {6,0}
        };
        crop_soil = new Dictionary<int, int>(){
            {1,-1},
            {2,-2},
            {3,-3},
            {4,-4},
            {5,-5},
            {6,-6}
        };
    }

    public void UpdateCropQuantity(int cropType, int quantity){
        crop_quantity[cropType] += quantity;
        print(crop_quantity[cropType]);
    }
    public int GetCropQuantity(int cropType){
        return crop_quantity[cropType];
    }
    public void UpdateCropSeeds(int cropType, int quantity){
        if(crop_seeds[cropType]+quantity<=0){
            crop_seeds[cropType] += quantity;
        }
        print(crop_seeds[cropType]);
    }
    public int GetCropSeeds(int cropType){
        return crop_seeds[cropType];
    }

    public int GetCropSoil(int cropType){
        return crop_soil[cropType];
    }



}
