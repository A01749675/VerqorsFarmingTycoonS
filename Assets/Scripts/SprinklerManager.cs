using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerManager : MonoBehaviour
{

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    MapManager mapManager;
    [SerializeField]
    TankManager tankManager;
    private bool isFull = false;
    [SerializeField]
    private int land_id =  -1;
    private int assigned_land = -1;
    private int cycle = 20;
    private bool active = false;

    public void WaterCrops(){
        if(mapManager.GetCurrentCycle()%cycle==0){
            if(mapManager.GetAverageWaterAtLand(assigned_land) < 100 && tankManager.GetWaterLevel() > 0){
                mapManager.WaterSpecificLand(assigned_land);
                tankManager.SetWaterLevel(-5);
            }
        }

    
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //AssignLandToSprinkler(land_id);
    }

    public void AssignLandToSprinkler(int land_id){
        if(mapManager.IsLandUnlocked(land_id)){
            assigned_land = land_id;
        }
    }

    // Update is called once per frame
    void Update()
    {
        WaterCrops();
    }
}
