using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateManager : MonoBehaviour
{

    //random int number generator

    public static System.Random random = new System.Random();

    private Dictionary<int,Dictionary<string,int>> climates;

    private int currentClimatecycle = 600;

    private int currentClimate = 1;

    MapManager mapManager;
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
        {0,10},
        {1,60},
        {2,20},
        {3,10},
        {4,5}
    };
    }

    // Update is called once per frame
    void Update()
    {
        int cycle = mapManager.GetCurrentCycle();
        if(cycle%currentClimatecycle == 0){
            int possibleClimate = random.Next(0,4);
            int odds = probability[possibleClimate];
            currentClimatecycle = random.Next(0,100);

            if(currentClimatecycle < odds){
                currentClimate = possibleClimate;
                mapManager.ClimateWaterUpdate();
            }
        }
    }

    public Dictionary<string,int> GetCurrentClimate(){
        return climates[currentClimate];
    }
    public void SetCurrentClimate(int climate){
        currentClimate = climate;
    }
}
