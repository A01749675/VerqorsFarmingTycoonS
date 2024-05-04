using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Llama funciones de MapManager para regar los cultivos y las funciones de TankManager
 para bajar el nivel del tanque de agua conforme se usa.
Autores:  Santiago Chevez Trejo, Carlos Iker Fuentes Reyes, 
          Alma Teresa Carpio Revilla, Mariana Marzyani Hernandez Jurado, 
          y Alan Rodrigo Vega Reza */
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
    private int cycle = 30; //Ciclo de regado

    private bool flag = false;
    public void WaterCrops(){
        //Riega si el ciclo es múltiplo de 30, si hay cultivos y si no está lloviendo
        if(mapManager.GetCurrentCycle()%cycle==0 && !flag && mapManager.GetCropsInLand(land_id)>0 && climateManager.GetCurrentClimateId() != 2){
            //print(tankManager.GetWaterLevel());
            //riega si el nivel del agua en la tierra está en ciertos rangos
            if(mapManager.GetAverageWaterAtLand(land_id) < 75 && tankManager.GetWaterLevel() > 0 && mapManager.LandPlanted(land_id)){
                animator.enabled = true;
                mapManager.WaterSpecificLand(land_id);
                tankManager.SetWaterLevel(-5);
                flag = true;
            }
        }
        //Desactiva la animación si no se cumple la condición
        if(mapManager.GetCurrentCycle()%cycle!=0){
            flag = false;
            spriteRenderer.sprite = initial;
        }
        //desactiva la animación si no hay agua en el tanque
        if(tankManager.GetWaterLevel() <= 0){
            animator.enabled = false;
            spriteRenderer.sprite = initial;
        }
        //Desactiva la animación si no hay cultivos en la tierra
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
