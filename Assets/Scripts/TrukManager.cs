using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class TrukManager : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    MapManager mapManager;
    private bool isMoving = false;
    [SerializeField]
    public int assigned_land = -1;
    private int assigned_crop = -1;

    private int crop_collection_constant = 20;

    private int last_collection = 0;

    private int last_moved = 0;
    private int moving_constant = 5;

    private Vector3 initial_position;
    private int cycle = 0;
    private bool active = true;


    public void CollectCrops(){
        if(assigned_land != -1 && assigned_crop != -1 && mapManager.LandPlanted(assigned_land)){
            mapManager.FarmerAutomaticCollection(assigned_land);
            if(mapManager.GetCropsInLand(assigned_land) == 0){
                last_collection = cycle;
                animator.SetBool("moving", true);
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initial_position = transform.position;
        //AssignLandToTruck(12);
        assigned_crop = mapManager.GetCropAtLand(assigned_land);

    }

    public void AssignLandToTruck(int land_id){
        if(mapManager.IsLandUnlocked(land_id)){
            assigned_land = land_id;
            assigned_crop = mapManager.GetCropAtLand(land_id);
        }
    }
    // Update is called once per frame
    void Update()
    {
        cycle = mapManager.GetCurrentCycle();
        if(active){
            if(cycle % crop_collection_constant == 0){
                CollectCrops();
            }
            if((cycle-last_collection)<moving_constant && last_collection!=0){
                animator.SetBool("moving", true);
                transform.position += Vector3.right*Time.deltaTime;
                last_moved = cycle;
            }
            else if((cycle-last_moved)<moving_constant && last_moved!=0){
                spriteRenderer.flipX = true;
                transform.position -= Vector3.right*Time.deltaTime;
            }
            else{
                if(last_moved!=0){
                    animator.SetBool("moving", false);
                    spriteRenderer.flipX = false;
                    transform.position = initial_position;
                }

                last_moved = 0;
                last_collection = 0;
            }         
        }

    }
}
