using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderModifier : MonoBehaviour
{
    [SerializeField] Image sliderColorBar;
    [SerializeField] Color goodSleep;
    [SerializeField] Color okSleep;
    [SerializeField] Color badSleep;

    Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
    }

    public void SliderUpdate(int value){
        slider.value = value;
        
        if(value >= 70) sliderColorBar.color = goodSleep;
        else if(value < 70 && value >= 33) sliderColorBar.color = okSleep;
        else sliderColorBar.color = badSleep;
    }

    public int GetValue() {return (int)slider.value;}
}
