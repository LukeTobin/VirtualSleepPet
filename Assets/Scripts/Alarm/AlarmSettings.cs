using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class AlarmSettings : MonoBehaviour
{
    [SerializeField] List<DayOfWeek> days = new List<DayOfWeek>();
    [SerializeField] TMP_InputField inputField;
    [SerializeField] SwitchManager alarmSound;
    [SerializeField] SwitchManager vibrations;

    public List<int> Days(){
        List<int> d = new List<int>();
        for(int i = 0;i < days.Count;i++){
            if(days[i].Toggled) d.Add(days[i].Day);
        }
        return d;
    }

    public bool AlarmSound(){return alarmSound.isOn;}
    public bool Vibrate(){return vibrations.isOn;}
    public string Name(){return inputField.text;}
    public int Hour {get;set;}
    public int Minute {get;set;}
}
