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
    public TextMeshProUGUI dayT, yearT, timeT, seasonT, weekT;
    //public Image weatherImg1;
    //public Sprite[] weatherSprites;
    private float startingRotation;
    public Light sunlight;
    public float nightIntensity; 
    public float dayIntensity;
    public AnimationCurve dayNightCurve;

    private int day;
    private int day_year;
    private int week;
    private int month;
    private int year;
    private bool banderaday=false;
    private bool banderamonth =  false;

    //public int  ClimateID; 

    private void Awake(){
        startingRotation = ClockFace.localEulerAngles.z;
        day = 1;
        week = 1;
        month = 1;
        year = 2023;
        day_year = 1;
    }
    public void Start(){
        SetDate(mapManager.GetCurrentCycle());
    }

    private void OnEnable(){
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    
    }

    private void OnDisable(){
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }

    //función que establece la fecha dependiendo del ciclo de MapManager
    public void SetDate(int cycle){
        cycle = (int) (cycle/8);
        year = (int)(cycle/360+2024);
        month = ((cycle%360)/30) ;
        week = (int)((cycle%360)/7)+1;
        day = (cycle%360)%30;
        if((cycle%360)%30 != 0){
            month += 1;
        }


    }

    //Función que actualiza el día conforme van pasando los ciclos
    private int Dia(){
        if (mapManager.GetCurrentCycle()%8 == 0 && !banderaday){
            day ++;   
            day_year = (mapManager.GetCurrentCycle()/8)%360;
            banderaday = true;
            return day;
        }
        else if (mapManager.GetCurrentCycle()%8 != 0){
            banderaday = false;
            return day;
        }
        else return day;
        
    }

    //Función que actualiza la semana conforme van pasando los días
    private int Semana(){
        int dayNum = day_year;
        if (dayNum%7 == 0){
            week = day_year/7 + 1;
            return week;
        }
        else return week;
    }

    //Función que actualiza el mes conforme van pasando los días
    private string Mes(){ 
        int dayNum = Dia();
        if (dayNum%31 == 0 && !banderamonth){
            banderamonth = true;
            month += 1;
            switch (month){
            case 1:
                day = 1;
                return "Enero";
            case 2:
                day = 1; 
                return "Febrero";
            case 3:
                day = 1; 
                return "Marzo";
            case 4:
                day = 1;
                return "Abril";
            case 5:
                day = 1;
                return "Mayo";
            case 6:
                day = 1;
                return "Junio";
            case 7:
                day = 1;
                return "Julio";
            case 8:
                day = 1; 
                return "Agosto";
            case 9:
                day = 1;
                return "Septiembre";
            case 10:
                day = 1;
                return "Octubre";
            case 11:
                day = 1;
                return "Noviembre";
            case 12:
                day = 1;
                return "Diciembre";
            default:
                day = 1;
                return "Aquí debería de haber un mes";
        }
        
        }
        else {
            banderamonth = false;
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

    //Función que actualiza el año conforme van pasando los días (cada 360 días)
    private int Año(){
        int dayNum = day_year;
        if (dayNum%360 == 0){
            year += 1;
            day = 1;
            day_year = 1;
            month = 1;
            week = 1;
            return year;
        }
        else return year;
    }

    //Función que actualiza la fecha y la hora en el reloj y hace que gire la rueda del calendario.
    private void UpdateDateTime(DateTime dateTime){
        dayT.text = Dia().ToString();
        timeT.text = dateTime.TimeToString();
        seasonT.text = Mes();
        yearT.text = Año().ToString();
        weekT.text = Semana().ToString();
        //weatherImg1.sprite = weatherSprites[ClimateID];
        float t =  (float)dateTime.Hour/24f;
        float newRotation = Mathf.Lerp(0,360,t);
        ClockFace.localEulerAngles = new Vector3(0,0,newRotation + startingRotation);
        float dayNightT = dayNightCurve.Evaluate(t);
        sunlight.intensity = Mathf.Lerp(dayIntensity,nightIntensity,dayNightT);
    }
    
}
