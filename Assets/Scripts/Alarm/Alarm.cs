using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Alarm : MonoBehaviour
{   
    //https://developer.android.com/guide/components/intents-common#Clock
    const string ACTION_SET_ALARM = "android.intent.action.SET_ALARM";
	const string EXTRA_HOUR = "android.intent.extra.alarm.HOUR";
	const string EXTRA_MINUTES = "android.intent.extra.alarm.MINUTES";
	const string EXTRA_MESSAGE = "android.intent.extra.alarm.MESSAGE";
    const string EXTRA_VIBRATE = "android.intent.extra.alarm.VIBRATE";
    const string EXTRA_DAYS = "android.intent.extra.alarm.DAYS";

	public void OnClick(AlarmSettings settings)
	{
		string finalName = settings.Name();
		if(finalName == string.Empty) finalName = "Sleep Pet";
		CreateAlarm(finalName, settings.Hour, settings.Minute, settings.Vibrate(), settings.Days());
	}

	public void CreateAlarm(string message, int hour, int minutes, bool vibrate, List<int> m_Days)
	{

		var intentAJO = new AndroidJavaObject("android.content.Intent", ACTION_SET_ALARM);
		intentAJO
			.Call<AndroidJavaObject>("putExtra", EXTRA_MESSAGE, message)
			.Call<AndroidJavaObject>("putExtra", EXTRA_HOUR, hour)
			.Call<AndroidJavaObject>("putExtra", EXTRA_MINUTES, minutes)
            .Call<AndroidJavaObject>("putExtra", EXTRA_VIBRATE, vibrate);

		// Custom ArrayList int
		if(m_Days != null && m_Days.Count > 0){ // if days should repeat

			// get an instance of a Java ArrayList
			AndroidJavaObject arrayList = new AndroidJavaObject("java.util.ArrayList");

			// get an instance of java Integer class : 
			//
			// ArrayLists use generics, so a type doesnt need to be assigned to them on creation
			// Java's generics try to match C#'s int against Java data types - in this case int does not exist
			// Thus in order to get a suitable match we need to create an instance of an unsigned Integer
			AndroidJavaClass integerClass = new AndroidJavaClass("java.lang.Integer"); 

			// Loop through a list of int's that correspond with key's for the Java Calender classes enum for Days
			for(int i = 0;i < m_Days.Count;i++){
				// using CallStatic over Call lets us access the static method to interpret non-native data types in Java
				// This lets us convert the C#/C/C++ implementation of int too Java's Integer type
				AndroidJavaObject intToInteger = integerClass.CallStatic<AndroidJavaObject>("valueOf", m_Days[i]);

				// Now that the type is native to Java, it's possible to access the ArrayList like intended
				arrayList.Call<bool>("add", intToInteger);
			}

			// Add the list to the EXTRA_DAYS android intents
			intentAJO.Call<AndroidJavaObject>("putExtra", EXTRA_DAYS, arrayList);
		}
		
		GetUnityActivity().Call("startActivity", intentAJO);
	}

	AndroidJavaObject GetUnityActivity()
	{
		using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		}
	}
}
