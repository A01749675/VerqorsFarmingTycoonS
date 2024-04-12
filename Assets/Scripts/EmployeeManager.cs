using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmployeeManager : MonoBehaviour
{

    [SerializeField]
    private int assigned_land = -1;

    private int assigned_crop = -1;

    private int last_plant = 0;

    [SerializeField]
    private MapManager mapManager;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //AssignLandToEmployee(12);
        
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
            animator.SetBool("ActivatedMovement",true);
        }
    }

    public void PlantLand(){
        if(assigned_land != -1){
            mapManager.FarmerAutomaticPlanting(assigned_land,assigned_crop);
            animator.SetBool("ActivatedMovement",true);
        }
    
    }
    // Update is called once per frame
    void Update()
    {
        int cycle = mapManager.GetCurrentCycle();
        
        if(cycle % 10 == 0 && mapManager.LandPlanted(assigned_land) == false && assigned_land != -1){
            assigned_crop = mapManager.GetCropAtLand(assigned_land);
            print("Employee has been called to work at " +assigned_land+ " with crop "+ assigned_crop);
            print("Employee Tried to plant");
            PlantLand();
            last_plant = cycle;
        }
        if(cycle - last_plant > 10 && last_plant != 0){
            animator.SetBool("ActivatedMovement",false);
            last_plant = 0;
        }
        
        
    }
}
