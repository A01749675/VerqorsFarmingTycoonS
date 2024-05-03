using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    private int waterLevel;
    private int tankLevel;


    public int GetTankLevel(){
        return tankLevel;
    }

    public int GetWaterLevel(){
        return waterLevel;
    }

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

    public void SetTankLevel(int lvl){
        tankLevel = lvl;
    }

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
