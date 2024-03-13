using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CropManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static int selected_crop;
    public Dictionary<int, int> crop_quantity;
    public Dictionary<int, int> crop_seeds;
    public Dictionary<Vector3Int, Dictionary<string,int>> cropCycleGrowth = new Dictionary<Vector3Int, Dictionary<string,int>>();
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
            {1,500},
            {2,500},
            {3,500},
            {4,500},
            {5,500},
            {6,500}
        };
        }

    

    public void UpdateCropQuantity(int cropType, int quantity){
        crop_quantity[cropType] += quantity;
    }
    public int GetCropQuantity(int cropType){
        return crop_quantity[cropType];
    }
    public void UpdateCropSeeds(int cropType, int quantity){
        crop_seeds[cropType] += quantity;
    }
    public int GetCropSeeds(int cropType){
        return crop_seeds[cropType];
    }

}
