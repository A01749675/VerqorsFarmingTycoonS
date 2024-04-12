using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DPUtils.Systems.DateTime;
using Image = UnityEngine.UI.Image;

public class ClockManager : MonoBehaviour
{

    public RectTransform ClockFace;
    public TextMeshProUGUI Date, Time, Season, Week;
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

    bool lerUp = true;

    private void UpdateDateTime (DateTime dateTime){
        Date.text = dateTime.DateToString();
        Time.text = dateTime.TimeToString();
        Season.text = dateTime.Season1.ToString();
        Week.text = $"SEM: {dateTime.CurrentWeek+1.ToString()}";
        //weatherImg1.sprite = weatherSprites[ClimateID];
        float t =  (float)dateTime.Hour/24f;
        float newRotation = Mathf.Lerp(0,360,t);
        ClockFace.localEulerAngles = new Vector3(0,0,newRotation + startingRotation);
        float dayNightT = dayNightCurve.Evaluate(t);
        sunlight.intensity = Mathf.Lerp(dayIntensity,nightIntensity,dayNightT);
    }
    
}
