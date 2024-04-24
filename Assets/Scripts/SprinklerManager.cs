using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerManager : MonoBehaviour
{

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    public Sprite initial;

    [SerializeField]
    MapManager mapManager;
    [SerializeField]
    TankManager tankManager;
    private bool isFull = false;
    [SerializeField]
    private int land_id =  -1;
    private int assigned_land = -1;
    private int cycle = 30;
    private bool active = false;

    private bool flag = false;
    public void WaterCrops(){
        if(mapManager.GetCurrentCycle()%cycle==0 && !flag){
            //print(tankManager.GetWaterLevel());
            if(mapManager.GetAverageWaterAtLand(land_id) < 100 && tankManager.GetWaterLevel() > 0 && mapManager.LandPlanted(land_id)){
                animator.enabled = true;
                mapManager.WaterSpecificLand(land_id);
                tankManager.SetWaterLevel(-5);
                print("Yeet: The tank has: "+tankManager.GetWaterLevel());
                flag = true;
            }
        }
        if(mapManager.GetCurrentCycle()%cycle!=0){
            flag = false;
            spriteRenderer.sprite = initial;
        }
        if(tankManager.GetWaterLevel() <= 0){
            animator.enabled = false;
            spriteRenderer.sprite = initial;
        }

    
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Animator>().enabled = false;
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
