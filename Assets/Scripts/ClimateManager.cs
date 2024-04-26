using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;


public class ClimateManager : MonoBehaviour
{

    //random int number generator

    public static System.Random random = new System.Random();

    private Dictionary<int,Dictionary<string,int>> climates;

    public int currentClimatecycle = 50;

    private int currentClimate = 1;

    public MapManager mapManager;

    public TankManager tankManager;
    
    public TextMeshProUGUI newsText;

    public TextMeshProUGUI bigPeriodicoText1;
    public TextMeshProUGUI bigPeriodicoText2;

    public Image bigPeriodicoImg1;
    public Image bigPeriodicoImg2;

    public Sprite bigPeriodicoSprite1;
    public Sprite bigPeriodicoSprite2;
    public Sprite bigPeriodicoSprite3;
    public Sprite bigPeriodicoSprite4;
    public Sprite bigPeriodicoSprite5;
    public Sprite bigPeriodicoSprite6;
    public Sprite bigPeriodicoSprite7;
    public Sprite bigPeriodicoSprite8;
    public Sprite bigPeriodicoSprite9;
    public Sprite bigPeriodicoSprite10;

    public GameObject rain;
    public GameObject flood;
    public GameObject hurricane;
    private AudioSource audioSourceLluvia;
    private AudioSource audioSourceFlood;
    private AudioSource audioSourceHurricane;

    public bool ClimateAlreadyExecuted = false;
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
                {"water",130},
                {"temperature",15}
            }}
        };
        probability = new Dictionary<int,int>(){
        {0,40},
        {1,90},
        {2,60},
        {3,30},
        {4,10}
        };
        audioSourceLluvia = GameObject.Find("Lluvia").GetComponent<AudioSource>();
        audioSourceFlood = GameObject.Find("Inundacion").GetComponent<AudioSource>();
        audioSourceHurricane = GameObject.Find("Huracan").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int cycle = mapManager.GetCurrentCycle();
        UpdateClimate(cycle);
    }

    private void UpdateClimate(int cycle){
        if(!ClimateAlreadyExecuted){
            ClimateAlreadyExecuted = true;
            int possibleClimate = random.Next(0,5);
            int odds = probability[possibleClimate];
            int climate_probability = random.Next(1,100);
            if(climate_probability < odds){
                currentClimate = possibleClimate;
                PrintClimate(currentClimate);
                mapManager.SetDisaster(currentClimate);
                mapManager.WaterRate(currentClimate);
                mapManager.UpdateVisualWater(currentClimate);
            }
            else{
                if(!(climate_probability<probability[currentClimate])){
                    currentClimate = 1;
                }
                PrintClimate(currentClimate);
                mapManager.SetDisaster(currentClimate);
                mapManager.WaterRate(currentClimate);
                mapManager.UpdateVisualWater(currentClimate);
            }
        }
    }

    public Dictionary<string,int> GetCurrentClimate(){
        return climates[currentClimate];
    }

    public int GetCurrentClimateId(){
        return currentClimate;
    }

    public void PrintClimate(int climate){
        switch(climate){
            case 0:
                Debug.Log("Drought");
                newsText.text = "Sequía daña los cultivos";
                bigPeriodicoText1.text = "Sequía azota el país y mata las cosechas";
                bigPeriodicoImg1.sprite = bigPeriodicoSprite1;
                bigPeriodicoText2.text = "Hombre afeita la cabeza de un gato en su casa";
                bigPeriodicoImg2.sprite = bigPeriodicoSprite2;
                rain.SetActive(false);
                flood.SetActive(false);
                hurricane.SetActive(false);
                break;
            case 1:
                Debug.Log("Normal");
                newsText.text = "Un día normal en el país";
                bigPeriodicoText1.text = "Clima normal en el país";
                bigPeriodicoImg1.sprite = bigPeriodicoSprite3;
                bigPeriodicoText2.text = "Quantum Robotics gana primer lugar en URC";
                bigPeriodicoImg2.sprite = bigPeriodicoSprite4;
                //tankManager.FillTank();
                rain.SetActive(false);
                flood.SetActive(false);
                hurricane.SetActive(false);
                audioSourceLluvia.Stop();
                audioSourceFlood.Stop();
                audioSourceHurricane.Stop();
                break;
            case 2:
                Debug.Log("Rain");
                newsText.text = "Lluvias moderadas en el país";
                bigPeriodicoText1.text = "Se espera lluvia moderada en el país";
                bigPeriodicoImg1.sprite = bigPeriodicoSprite5;
                bigPeriodicoText2.text = "Santiago Chevez gana la lotería";
                bigPeriodicoImg2.sprite = bigPeriodicoSprite6;
                tankManager.FillTank();
                rain.SetActive(true);
                flood.SetActive(false);
                hurricane.SetActive(false);
                audioSourceLluvia.Play();
                audioSourceFlood.Stop();
                audioSourceHurricane.Stop();
                break;
            case 3:
                Debug.Log("Flood");
                newsText.text = "Inundación daña negocios locales";
                bigPeriodicoText1.text = "Inundación en los estados del norte";
                bigPeriodicoImg1.sprite = bigPeriodicoSprite7;
                bigPeriodicoText2.text = "Se celebra competencia de cocina a nivel mundial";
                bigPeriodicoImg2.sprite = bigPeriodicoSprite8;
                tankManager.FillTank();
                rain.SetActive(false);
                flood.SetActive(true);
                hurricane.SetActive(false);
                audioSourceLluvia.Stop();
                audioSourceFlood.Play();
                audioSourceHurricane.Stop();
                break;
            case 4:
                Debug.Log("Hurricane");
                newsText.text = "Alerta de huracán";
                bigPeriodicoText1.text = "Huracán categoría 5 azota los cultivos, se estiman 690000 muertos y desaparecidos";
                bigPeriodicoImg1.sprite = bigPeriodicoSprite9;
                bigPeriodicoText2.text = "Se encuentran fósiles de dinosaurios en México";
                bigPeriodicoImg2.sprite = bigPeriodicoSprite10;
                rain.SetActive(false);
                flood.SetActive(false);
                hurricane.SetActive(true);
                audioSourceLluvia.Stop();
                audioSourceFlood.Stop();
                audioSourceHurricane.Play();
                break;
        }
    }
}
