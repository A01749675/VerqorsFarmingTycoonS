using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    private int WaterLevel;
    private int TankLevel;


    public int GetTankLevel(){
        return TankLevel;
    }

    public int GetWaterLevel(){
        return WaterLevel;
    }

    public void FillTank(){
        print("Filling tank");
        switch(TankLevel){
            case 0:
                WaterLevel = 0;
                break;
            case 1:
                WaterLevel = 125;
                break;
            case 2:
                WaterLevel = 150;
                break;
            case 3:
                WaterLevel = 175;
                break;
        }
    }

    public void SetTankLevel(int lvl){
        TankLevel = lvl;
    }

    public void SetWaterLevel(int change){
        if(WaterLevel>0){
            WaterLevel += change;
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
