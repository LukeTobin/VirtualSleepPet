using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimePicker : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] AlarmSettings settings;
    [SerializeField] TMP_Text targetSleepText;

    private AndroidJavaObject timePickerDialog;

    private const int DEFAULT_HOUR = 8;
    private const int DEFAULT_MINUTE = 0;

    private void Start() {
        settings.Hour = DEFAULT_HOUR;
        settings.Minute = DEFAULT_MINUTE;
    }

    public void DefaultText(){
        TimeSpan defaultTime = new TimeSpan(DEFAULT_HOUR, DEFAULT_MINUTE, 0);
        TimeSpan target = SleepMonitor.Instance.TargetSleep;

        if(defaultTime < target){
            defaultTime = defaultTime.Add(new TimeSpan(24,0,0));
        }

        TimeSpan diff = defaultTime.Subtract(target);

        targetSleepText.text = $"~{diff.Hours} hours sleep";
    }

    public void ShowTimePicker()
    {
        AndroidJavaObject activity = GetUnityActivity();

        int hour = DEFAULT_HOUR;
        int minute = DEFAULT_MINUTE;
        
        timePickerDialog = new AndroidJavaObject("android.app.TimePickerDialog", activity, new TimePickerDialogListener(_text, settings, targetSleepText), hour, minute, false);

        // Show the TimePickerDialog
        timePickerDialog.Call("show");
    }

    private class TimePickerDialogListener : AndroidJavaProxy
    {
        TMP_Text txt;
        AlarmSettings settings;
        TMP_Text targetSleepText;


        public TimePickerDialogListener(TMP_Text match, AlarmSettings _settings, TMP_Text target) : base("android.app.TimePickerDialog$OnTimeSetListener")
        {
            txt = match;
            settings = _settings;
            targetSleepText = target;
        }

        public void onTimeSet(AndroidJavaObject view, int hourOfDay, int minute)
        {
            // Handle the time selected by the user
            txt.text = string.Format("{0:D2}:{1:D2}", hourOfDay, minute);
            settings.Hour = hourOfDay;
            settings.Minute = minute;

            TimeSpan defaultTime = new TimeSpan(hourOfDay, minute, 0);
            TimeSpan target = SleepMonitor.Instance.TargetSleep;

            if(defaultTime < target){
                defaultTime = defaultTime.Add(new TimeSpan(24,0,0));
            }

            TimeSpan diff = defaultTime.Subtract(target);

            targetSleepText.text = $"~{diff.Hours} hours sleep";

            //TimeSpan diff = new TimeSpan(hourOfDay, minute, 0) - SleepMonitor.Instance.TargetSleep;
            //targetSleepText.text = $"~{diff.Hours} hours sleep";
        }
    }

    private AndroidJavaObject GetUnityActivity()
    {
        using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
}
