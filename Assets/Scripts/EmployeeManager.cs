using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmployeeManager : MonoBehaviour
{
    public static System.Random random = new System.Random();

    [SerializeField]
    private int assigned_land = -1;

    private int assigned_crop = -1;

    private int last_plant = 0;
    private int last_movement = 0;

    private int current_movement = 0;

    private int movement_constant = 5;
    private int planting_constant = 10;

    private int show_emotion_time = 10;

    private int show_emotion_cycle = 20;

    private int showed_emotion = 0;

    private bool unlocked_farmer = true;

    [SerializeField]
    private List<Sprite> farmer_emotion;

    [SerializeField]
    private MapManager mapManager;
    [SerializeField]
    private CropManager cropManager;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector3 initial_position;

    private bool flip = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initial_position = transform.position;
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
        if(assigned_land != -1 && !mapManager.LandPlanted(assigned_land) && cropManager.GetCropSeeds(assigned_crop) > 0){
            mapManager.FarmerAutomaticPlanting(assigned_land,assigned_crop);
            animator.SetBool("ActivatedMovement",true);
        }
    
    }

    public void ShowEmotion(){
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.SetActive(true);
        child.GetComponent<SpriteRenderer>().sprite = farmer_emotion[random.Next(0,farmer_emotion.Count-1)];

    }

    public void HideEmotion(){
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        int cycle = mapManager.GetCurrentCycle();
        if(unlocked_farmer){
            if(cycle % 20 == 0 && mapManager.LandPlanted(assigned_land) == false && assigned_land != -1){
                assigned_crop = mapManager.GetCropAtLand(assigned_land);
                PlantLand();
                last_plant = cycle;
                last_movement = cycle;

            }
            if(cycle - last_plant > planting_constant && last_plant != 0){
                animator.SetBool("ActivatedMovement",false);
                last_plant = 0;
            }
            if((cycle-last_movement)<movement_constant && last_movement != 0){
                transform.position += Vector3.right*Time.deltaTime;
                current_movement = cycle;
                
            }
            else if((cycle-current_movement)<movement_constant && current_movement != 0){
                spriteRenderer.flipX = !flip;
                transform.position -= Vector3.right*Time.deltaTime;
            }
            else{
                if(current_movement != 0){
                    flip = !flip;
                    spriteRenderer.flipX = flip;
                }
                last_movement=0;
                current_movement=0;
                transform.position = initial_position;
                //spriteRenderer.flipX = !flip;
            }
            
            if(cycle%show_emotion_cycle == 0){
                ShowEmotion();
                showed_emotion = cycle;
            }
            else if ((cycle-showed_emotion)%show_emotion_time == 0 && showed_emotion != 0){
                HideEmotion();
            }
        }
        
    }
}
