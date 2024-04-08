using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DPUtils.Systems.DateTime;
using Microsoft.Unity.VisualStudio.Editor;

public class ClockManager : MonoBehaviour
{

    public RectTransform ClockFace;
    public TextMeshProUGUI Date, Time, Season, Week;
    public Image weatherSprite;
    public Sprite[] weatherSprites;
    private float startingRotation;
    public Light sunlight;
    public float nightIntensity; 
    public float dayIntensity;
    public AnimationCurve dayNightCurve;

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
        Season.text = dateTime.Season.ToString();
        Week.text = $"WK: {dateTime.CurrentWeek.ToString()}";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
