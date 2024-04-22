using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace DPUtils.Systems.DateTime{
public class TimeManager : MonoBehaviour
{

    [SerializeField]
    public MapManager mapManager;

    [Header("Date & Time Settings")]
    [Range (1, 28)]
    public int  dateInMonth;
    [Range (1,4)]
    public int season;
    [Range (1,99)]
    public int year;
    [Range (0, 24)]
    public int hour;
    [Range (0, 6)]
    public int minutes;

    private DateTime DateTime;

    [Header ("Tick Settings")]
    public int TickMinutesIncrease = 10;
    public float TimeBetweenTicks = 1;
    private float currentTimeBetweenTicks = 0;
    public static UnityAction<DateTime> OnDateTimeChanged; 

    // Start is called before the first frame update
    private void Awake()
    {
        DateTime = new DateTime (dateInMonth, season - 1, year, hour, minutes * 10);
        Debug.Log($"Starting Date: {DateTime.NewYearsDay(2)}");
        Debug.Log($"Starting Date: {DateTime.SummerSolstice(4)}");
        Debug.Log($"Starting Date: {DateTime.PumpkinHarvest(10)}");
        Debug.Log($"Starting Date: {DateTime.StartofSeason(1,3)}");
        Debug.Log($"Starting Date: {DateTime.StartofWinter(2)}");
    }

    private void Start(){
        OnDateTimeChanged?.Invoke(DateTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeBetweenTicks += Time.deltaTime;
        if(currentTimeBetweenTicks >= TimeBetweenTicks){
            currentTimeBetweenTicks = 0;
            Tick();
        } 
    }

    void Tick(){
        AdvanceTime();
    }
    
    public void AdvanceTime(){
        DateTime.AdvanceMinutes(TickMinutesIncrease);
        OnDateTimeChanged ?.Invoke(DateTime);
    }

    public int GetWeek(){
        return DateTime.CurrentWeek;
    }


}

[System.Serializable]
public struct DateTime{
    #region Fields
    private Days day;
    private int date;
    private int year;
    private int hour;
    private int minutes;
    private Season season;
    private int totalNumDays;
    private int totalNumWeeks;

    #endregion

    #region Properties
    public Days Day => day;
    public int Date => date;
    public int Hour => hour;
    public int Minutes => minutes;
    public Season Season1 => season;
    public int Year => year;
    public int TotalNumDays => totalNumDays;
    public int TotalNumWeeks => totalNumWeeks;
    public int CurrentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;

    #endregion

    #region Constructors

    public DateTime(int date, int season, int year, int hour, int minutes){
        this.day = (Days)(date % 7);
        if (day == 0) day = (Days)7;
        this.date = date;
        this.season = (Season)season;
        this.year = year;
        this.hour = hour;
        this.minutes = minutes;

        totalNumDays = date + (28 * (int) this.season) + (112 * (year -1));
        totalNumWeeks = 1 + totalNumDays / 7;  


    }

    #endregion

    #region Time Advancement

    public void AdvanceMinutes(int SecondsToAdvanceBy){
        if (minutes + SecondsToAdvanceBy >= 60){
            minutes = (minutes + SecondsToAdvanceBy) % 60;
            AdvanceHour();
        } else {
            minutes += SecondsToAdvanceBy;
        }
    }

    private void AdvanceHour(){
        if((hour + 1) == 24){
            hour = 0;
            AdvanceDay();
        } else {
            hour++;
        } 
    }

    private void AdvanceDay(){
        
        day++;
        
        if (day + 1 >(Days)7){
            day = (Days)1;
            totalNumWeeks++;
        }
        date++;

        if(date %29 == 0){
            AdvanceSeason();
            date = 1;
        }
        totalNumDays++;
    }

    private void AdvanceSeason(){
        if (season == Season.Invierno){
            season = Season.Primavera;
            AdvanceYear();
        }
        else season++;
    }

    private void AdvanceYear(){
        this.date = 1;
        year ++;
    }


    #endregion

    #region Bool Checks

    public bool IsNight(){
        return hour >18 || hour < 6;
    }

    public bool IsMorning(){
        return hour >= 6 && hour <= 12;
    }

    public bool IsAfternoon(){
        return hour > 12 && hour < 18;
    }

    public bool IsWeekend(){
        return day > Days.Vie? true : false;
    }

    public bool IsParticularDay(Days _day){
        return day == _day;
    }

#endregion

#region Key Dates

public DateTime NewYearsDay(int year){
    if (year == 0) year = 1;
    return new DateTime(1, 0, year, 6, 0);
}

public DateTime SummerSolstice(int year){
    if (year == 0) year = 1;
    return new DateTime(28, 1, year, 6, 0);
}

public DateTime PumpkinHarvest (int year){
    if (year == 0) year = 1;
    return new DateTime (28, 2, year, 6, 0);
}
#endregion

#region Start of Season

public DateTime StartofSeason(int season, int year){
    season = Mathf.Clamp(season, 0, 3);
    if (year == 0) year = 1;
    return new DateTime (1, season, year, 6, 0);
}

public DateTime StartofSpring(int year){
    return StartofSeason(0, year);
}

public DateTime StartodSummer(int year){
    return StartofSeason(1, year);
}

public DateTime StartofAutumn(int year){
    return StartofSeason(2, year);
}

public DateTime StartofWinter(int year){
    return StartofSeason(3, year);
}
    #endregion

    #region To Strings

    public override string ToString()
    {
        return $"Date : {DateToString()} Season: {season} Time: {TimeToString()}" +
        $"\nTotal Days: {totalNumDays} | Total Weeks: {totalNumWeeks}";
    }

    public string DateToString(){
        return $"{Day} {Date} {Year.ToString("D2")}";
    }
    public string TimeToString(){
        int adjustedHour = 0;

        if (hour == 0){
            adjustedHour = 12; 
        }
        
        else if (hour >= 13){
            adjustedHour = hour - 12;
        }

        else {
            adjustedHour = hour;
        }

        string AmPm = hour == 0 || hour < 12? "AM" : "PM";

        return $"{adjustedHour.ToString("D2")}:{minutes.ToString("D2")} {AmPm}";
    }
#endregion

[System.Serializable]

public enum Days{
    Lun = 1,
    Mar = 2,
    Mier = 3,
    Jue = 4,
    Vie = 5,
    Sab = 6,
    Dom = 7 
}

[System.Serializable]

public enum Season{
    Primavera = 1,
    Verano = 2,
    Otoño = 3,
    Invierno = 4
}

}
}