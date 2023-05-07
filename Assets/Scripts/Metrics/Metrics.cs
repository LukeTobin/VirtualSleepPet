using System.Collections.Generic;
using UnityEngine;

public class Metrics : MonoBehaviour
{
    [SerializeField] List<MetricBar> bars = new List<MetricBar>();
    
    List<string> dayStrings = new List<string>(){"S","M","T","W","T","F","S"};

    void Start()
    {
        Display();
    }

    public void Display(){
        SaveData.current.Load();
        for(int i = 0;i < bars.Count;i++){
            if(SaveData.current.SleepData.Days[i] == null) {
                bars[i].gameObject.SetActive(true);
                Vector2 minMax = new Vector2(0,0);
                bars[i].UpdateTable(Mathf.RoundToInt(minMax.x), Mathf.RoundToInt(minMax.y), dayStrings[i]);
            }
            else{
                bars[i].gameObject.SetActive(true);
                Vector2 minMax = SaveData.current.SleepData.Days[i];
                bars[i].UpdateTable(Mathf.RoundToInt(minMax.x), Mathf.RoundToInt(minMax.y), dayStrings[i]);
            }
        }
    }
}
