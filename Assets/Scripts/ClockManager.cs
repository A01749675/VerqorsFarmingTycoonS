using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DPUtils.Systems.DateTime;
using Image = UnityEngine.UI.Image;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;

public class ClockManager : MonoBehaviour
{

    [SerializeField]
    MapManager mapManager;

    public RectTransform ClockFace;
    public TextMeshProUGUI Day, Year, Time, Season, Week;
    //public Image weatherImg1;
    //public Sprite[] weatherSprites;
    private float startingRotation;
    public Light sunlight;
    public float nightIntensity; 
    public float dayIntensity;
    public AnimationCurve dayNightCurve;

    //public int  ClimateID; 

    private void Awake(){
        startingRotation = ClockFace.localEulerAngles.z;
    }

    private void OnEnable(){
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    
    }

    private void OnDisable(){
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }

    private int Dia(){
        int day = 1;
        if (mapManager.GetCurrentCycle()%8 == 0){
            day = mapManager.GetCurrentCycle()/8;
            return day;
        }
        else return day;
        
    }

    private int Semana(){
        int dayNum = Dia();
        int week = 1;
        if (dayNum%7 == 0){
            week = dayNum/7;
            return week;
        }
        else return week;
    }

    private string Mes(){ 
        int dayNum = Dia();
        int month = 1;
        if (dayNum%30 == 0){
            month = dayNum/30;
        }

        switch (month){
            case 1:
                return "Enero";
            case 2:
                return "Febrero";
            case 3:
                return "Marzo";
            case 4:
                return "Abril";
            case 5:
                return "Mayo";
            case 6:
                return "Junio";
            case 7:
                return "Julio";
            case 8:
                return "Agosto";
            case 9:
                return "Septiembre";
            case 10:
                return "Octubre";
            case 11:
                return "Noviembre";
            case 12:
                return "Diciembre";
            default:
                return "Aquí debería de haber un mes";
        }
    }

    private int Año(){
        int dayNum = Dia();
        int year = 1;
        if (dayNum%360 == 0){
            year = dayNum/360;
            return year;
        }
        else return year;
    }

    bool lerUp = true;

    private void UpdateDateTime (DateTime dateTime){
        Day.text = Dia().ToString();
        Time.text = dateTime.TimeToString();
        Season.text = Mes();
        Year.text = Año().ToString();
        Week.text = Semana().ToString();
        //weatherImg1.sprite = weatherSprites[ClimateID];
        float t =  (float)dateTime.Hour/24f;
        float newRotation = Mathf.Lerp(0,360,t);
        ClockFace.localEulerAngles = new Vector3(0,0,newRotation + startingRotation);
        float dayNightT = dayNightCurve.Evaluate(t);
        sunlight.intensity = Mathf.Lerp(dayIntensity,nightIntensity,dayNightT);
    }
    
}
