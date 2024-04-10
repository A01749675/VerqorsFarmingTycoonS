using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmployeeManager : MonoBehaviour
{

    private int assigned_land = -1;

    private int assigned_crop = -1;

    [SerializeField]
    private MapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void AssignLandToEmployee(int land_id){
        if(mapManager.IsLandUnlocked(land_id)){
            assigned_land = land_id;
            assigned_crop = mapManager.GetCropAtLand(land_id);
        }

    }

    public void CollectLand(){
        if(assigned_land != -1){
            mapManager.FarmerAutomaticCollection(assigned_land);
        }
    }

    public void PlantLand(){
        if(assigned_land != -1){
            mapManager.FarmerAutomaticPlanting(assigned_land,assigned_crop);
        }
    
    }
    // Update is called once per frame
    void Update()
    {
        int cycle = mapManager.GetCurrentCycle();
        if(cycle % 60 == 0){
            CollectLand();
        }
    }
}
