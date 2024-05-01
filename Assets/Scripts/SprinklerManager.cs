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
    [SerializeField]
    ClimateManager climateManager;
    [SerializeField]
    private int land_id =  -1;
    private int cycle = 30;

    private bool flag = false;
    public void WaterCrops(){
        
        if(mapManager.GetCurrentCycle()%cycle==0 && !flag && mapManager.GetCropsInLand(land_id)>0 && climateManager.GetCurrentClimateId() != 2){
            //print(tankManager.GetWaterLevel());
            if(mapManager.GetAverageWaterAtLand(land_id) < 75 && tankManager.GetWaterLevel() > 0 && mapManager.LandPlanted(land_id)){
                animator.enabled = true;
                mapManager.WaterSpecificLand(land_id);
                tankManager.SetWaterLevel(-5);
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
        if(mapManager.GetCropsInLand(land_id)==0){
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
  
    }

    // Update is called once per frame
    void Update()
    {
        WaterCrops();
    }
}
