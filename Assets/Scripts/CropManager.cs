using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CropManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Dictionary<int, int> crop_quantity;
    public Dictionary<int, int> crop_seeds;
    public Dictionary<Vector3Int, Dictionary<string,int>> cropCycleGrowth = new Dictionary<Vector3Int, Dictionary<string,int>>();
    private void Awake(){
        //print("CropManager Awake");
        //Establece valores default
        crop_quantity= new Dictionary<int, int>(){
            {1,0},
            {2,0},
            {3,0},
            {4,0},
            {5,0},
            {6,0}
        };
        crop_seeds = new Dictionary<int, int>(){
            {1,0},
            {2,0},
            {3,0},
            {4,0},
            {5,0},
            {6,0}
        };
    }

    //Actualiza la cantidad de cultivos
    public void UpdateCropQuantity(int cropType, int quantity){
        crop_quantity[cropType] += quantity;
    }
    //Recupera la cantidad de cultivos
    public int GetCropQuantity(int cropType){
        return crop_quantity[cropType];
    }
    //Actualiza la cantidad de semillas
    public void UpdateCropSeeds(int cropType, int quantity){
        crop_seeds[cropType] += quantity;
    }
    //Recupera la cantidad de semillas
    public int GetCropSeeds(int cropType){
        return crop_seeds[cropType];
    }

}
