using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerManager : MonoBehaviour
{

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    MapManager mapManager;
    TankManager tankManager;
    private bool isFull = false;
    private int assigned_land = -1;
    private int cycle = 0;
    private bool active = false;

    public void WaterCrops(){
        for (int i = 0; i < 10; i++){
            if(mapManager.GetAverageWaterAtLand(assigned_land) < 100 && tankManager.GetWaterLevel() > 0){
                mapManager.WaterSpecificLand(assigned_land);
            }
        }
    
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AssignLandToSprinkler(int land_id){
        if(mapManager.IsLandUnlocked(land_id)){
            assigned_land = land_id;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
