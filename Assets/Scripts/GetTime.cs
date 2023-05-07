using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTime : MonoBehaviour
{
    public static GetTime Instance;

    private AndroidJavaObject timePickerDialog;

    private const int DEFAULT_HOUR = 22;
    private const int DEFAULT_MINUTE = 0;

    private void Awake() {
        Instance = this;
    }

    public void ShowTimePicker()
    {
        AndroidJavaObject activity = GetUnityActivity();

        int hour = DEFAULT_HOUR;
        int minute = DEFAULT_MINUTE;
        
        timePickerDialog = new AndroidJavaObject("android.app.TimePickerDialog", activity, new TimePickerDialogListener(), hour, minute, false);

        // Show the TimePickerDialog
        timePickerDialog.Call("show");
    }

    private class TimePickerDialogListener : AndroidJavaProxy
    {
        public TimePickerDialogListener() : base("android.app.TimePickerDialog$OnTimeSetListener")
        {

        }

        public void onTimeSet(AndroidJavaObject view, int hourOfDay, int minute)
        {
            SleepMonitor.Instance.SetTargetSleepTime(hourOfDay, minute);
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
