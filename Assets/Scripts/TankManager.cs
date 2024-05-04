using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Controla el nivel del agua del tanque de agua y dependiendo de los diferentes climas llena el tanque, 
cuando los aspersores lo usan baja el nivel del tanque de agua.
Autores:  Santiago Chevez Trejo, Carlos Iker Fuentes Reyes, 
          Alma Teresa Carpio Revilla, Mariana Marzyani Hernandez Jurado, 
          y Alan Rodrigo Vega Reza */
public class TankManager : MonoBehaviour
{
    private int waterLevel; //nivel de agua del tanque
    private int tankLevel; //nivel del tanque

    //obtener el nivel del tanque 
    public int GetTankLevel(){
        return tankLevel;
    }
    //Obtener cuanta agua hay en el tanque
    public int GetWaterLevel(){
        return waterLevel;
    }
    //Llenar el tanque dependiendo de su nivel 
    public void FillTank(){
        
        switch(tankLevel){
            case 0:
                waterLevel = 0;
                break;
            case 1:
                waterLevel = 125;
                break;
            case 2:
                waterLevel = 150;
                break;
            case 3:
                waterLevel = 175;
                break;
        }
    }
    //Cambiar el nivel del tanque
    public void SetTankLevel(int lvl){
        tankLevel = lvl;
    }
    //Cambiar el nivel de agua del tanque
    public void SetWaterLevel(int change){
        if(waterLevel>0){
            waterLevel += change;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetTankLevel(1);
        FillTank();
        
    }

    // Update is called once per frame
}
