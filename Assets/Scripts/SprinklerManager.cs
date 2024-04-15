using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerManager : MonoBehaviour
{

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    MapManager mapManager;
    private bool isFull = false;
    private int assigned_land = -1;
    private int cycle = 0;
    private bool active = true;

    public void WaterCrops(){
        for (int i = 0; i < 10; i++){
            if(mapManager.GetAverageWaterAtLand(assigned_land) < 100){

            }
        }
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
