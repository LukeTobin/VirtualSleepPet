using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeItem : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [Space]
    [SerializeField] List<int> times;

    public int CurrentIndex {get;set;}

    private void Awake() {
        CurrentIndex = 0;
        timeText.text = CurrentIndex.ToString();
    }

    public void TimeChange(int indexChange){
        CurrentIndex += indexChange;
        if(CurrentIndex < 0) CurrentIndex = times.Count - 1;
        if(CurrentIndex >= times.Count) CurrentIndex = 0;
        timeText.text = times[CurrentIndex].ToString();
    }

    public int GetTime() {return times[CurrentIndex];}
}
