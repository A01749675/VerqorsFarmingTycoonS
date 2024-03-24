using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateManager : MonoBehaviour
{

    //random int number generator

    public static System.Random random = new System.Random();

    private Dictionary<int,Dictionary<string,int>> climates;

    private int currentClimatecycle = 60;

    private int currentClimate = 1;

    public MapManager mapManager;
    private Dictionary<int,int> probability;
    // Start is called before the first frame update
    void Awake()
    {
        climates = new Dictionary<int,Dictionary<string,int>>(){
            //type 0 drought
            {0,new Dictionary<string,int>(){
                {"type",0},
                {"cost",-10000},
                {"water",0},
                {"temperature",30}
            }},
            //type 1 normal
            {1,new Dictionary<string,int>(){
                {"type",1},
                {"cost",0},
                {"water",0},
                {"temperature",20}
            }},
            //type 2 rain
            {2,new Dictionary<string,int>(){
                {"type",2},
                {"cost",0},
                {"water",100},
                {"temperature",15}
            }},
            //type 3 flood
            {3,new Dictionary<string,int>(){
                {"type",3},
                {"cost",-10000},
                {"water",130},
                {"temperature",15}
            }},
            //type 4 hurricane
            {4,new Dictionary<string,int>(){
                {"type",4},
                {"cost",-100000},
                {"water",170},
                {"temperature",15}
            }}
        };
        probability = new Dictionary<int,int>(){
        {0,50},
        {1,60},
        {2,60},
        {3,40},
        {4,5}
    };
    }

    // Update is called once per frame
    void Update()
    {
        int cycle = mapManager.GetCurrentCycle();
        if(cycle%currentClimatecycle == 0){
            print("_________Climate update__________");
            print("Current climate: "+currentClimate);
            int possibleClimate = random.Next(0,5);
            int odds = probability[possibleClimate];
            currentClimatecycle = random.Next(0,100);
            if(currentClimatecycle < odds){
                print("Climate changed to "+possibleClimate);
                currentClimate = possibleClimate;
                PrintClimate(currentClimate);
                if(currentClimate != 1){
                    mapManager.ClimateWaterUpdate();
                }
            }
            else{
                print("Climate did not change, checking probability again");
                if(currentClimatecycle<probability[currentClimate]){
                    if(currentClimate != 1){
                        print("Updating water as climate probability repeated");
                        mapManager.ClimateWaterUpdate();
                        print("For current climate "+currentClimate+" water was updated");
                    }
                }
                else{
                    currentClimate = 1;
                    print("Change to normal climate as previous climate probability was not met");
                }
            }
        }
    }

    public Dictionary<string,int> GetCurrentClimate(){
        return climates[currentClimate];
    }
    public void SetCurrentClimate(int climate){
        currentClimate = climate;
    }

    public void PrintClimate(int climate){
        switch(climate){
            case 0:
                Debug.Log("Drought");
                break;
            case 1:
                Debug.Log("Normal");
                break;
            case 2:
                Debug.Log("Rain");
                break;
            case 3:
                Debug.Log("Flood");
                break;
            case 4:
                Debug.Log("Hurricane");
                break;
        }
    }
}
