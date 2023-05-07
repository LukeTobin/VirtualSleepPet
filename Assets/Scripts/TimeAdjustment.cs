using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeAdjustment : MonoBehaviour
{
    [SerializeField] TimeItem hours;
    [SerializeField] TimeItem minutes;

    public void SetTimeSpan(){
        TimeSpan adjustedTime = new TimeSpan(hours.GetTime(), minutes.GetTime(), 0);
        SleepMonitor.Instance.QueryTime(adjustedTime);
    }
}
