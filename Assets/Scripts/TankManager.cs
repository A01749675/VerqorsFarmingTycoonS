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
        switch(TankLevel){
            case 1:
                WaterLevel = 50;
                break;
            case 2:
                WaterLevel = 75;
                break;
            case 3:
                WaterLevel = 100;
                break;
        }
    }

    public void SetWaterLevel(int change){
        if(WaterLevel>0){
            WaterLevel += change;
        }
    }

    public void SetTankLevel(int lvl){
        TankLevel = lvl;
    
    }
    // Start is called before the first frame update
    void Start()
    {
        TankLevel = 1;
        WaterLevel = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
