using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MetricBar : MonoBehaviour
{   
    [Range(-6, 0)] public int minValue = -3;
    [Range(0, 6)] public int maxValue = 3;

    [Space]
    [SerializeField] int minHeight = 15;
    [SerializeField] int maxHeight = 150;

    [Space]
    [SerializeField] Color goodColor = Color.green;
    [SerializeField] Color badColor = Color.red;

    [Space]
    [SerializeField] TMP_Text dayText;
    [SerializeField] Image fill;
    
    private int height;
    private int yPos;

    private RectTransform rectTransform;

    void Start(){
        rectTransform = GetComponent<RectTransform>();

        //gameObject.SetActive(false);
    }


    public void UpdateTable(int min, int max, string day){
        gameObject.SetActive(true);

        minValue = min;
        maxValue = max;

        dayText.text = day;

        int range = Mathf.Abs(minValue) + Mathf.Abs(maxValue);

        float zero = Mathf.Clamp01((float)range / 12f);
        fill.color = Color.Lerp(goodColor, badColor, zero);
        
        if (Mathf.Abs(maxValue) == Mathf.Abs(minValue)) yPos = 0;  
        else {
            if(maxValue > Mathf.Abs(minValue))
                yPos = (maxValue - range/2) * maxHeight - minHeight / 2;
            else 
                yPos = (minValue + range/2) * maxHeight - minHeight / 2;
        }

        height = range * maxHeight + minHeight;

        if(!rectTransform) rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, yPos);
    }
}