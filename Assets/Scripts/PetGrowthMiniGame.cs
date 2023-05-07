using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetGrowthMiniGame : MonoBehaviour
{
    public Image targetImage;
    public Image growImage;
    [Space]
    public float growthAverage;
    public float growthDeviation;
    float growthSpeed;
    [Space]
    public float clickThreshold = 50f;
    public float overgrowPercentage;
    [Space]
    [SerializeField] float downtime = 2f;

    [Header("Score Area")]
    [SerializeField] GameObject scoreContainer;
    [SerializeField] GameObject winImg;
    [SerializeField] GameObject loseImg;
    [SerializeField] SliderModifier slider;

    RectTransform rectTransform;

    bool gameStarted;

    bool firstStart;
    bool _firstStart;

    List<GameObject> scores = new List<GameObject>();
    int count = 0;
    int score = 0;

    void Start()
    {
        rectTransform = growImage.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted) return;

        rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, targetImage.rectTransform.sizeDelta * overgrowPercentage, growthSpeed * Time.deltaTime);

        if(Vector2.Distance(rectTransform.sizeDelta, targetImage.rectTransform.sizeDelta * overgrowPercentage) <= 2f){
            gameStarted = false;
            Invoke("ResetGame", downtime);
        }
    }

    public void StartGame(){
        if(count >= 4) {
            EndGame();
            return;
        }

        if(!firstStart && !_firstStart) ClearFirstStart();

        gameStarted = true;

        growImage.gameObject.SetActive(true);

        growthSpeed = Mathf.Abs(Random.Range(growthAverage - growthDeviation, growthAverage + growthDeviation));
    }

    void ClearFirstStart(){
        _firstStart = true;

        Invoke("Cleared", 2f);
    }

    void Cleared() {
        firstStart = true;
        count = 0;
        StartGame();
    }

    void EndGame(){
        Currency.Instance.Add(score * slider.GetValue());

        score = 0;
        _firstStart = false;
        firstStart = false;
        count = 0;
        gameStarted = false;
    }

    public void ResetGame(){
        rectTransform.sizeDelta = new Vector2(1, 1);
        StartGame();
    }

    public void Click(){
        float v = Vector2.Distance(rectTransform.sizeDelta, targetImage.rectTransform.sizeDelta);

        count++;

        if(v <= clickThreshold){
            GameObject win = Instantiate(winImg);
            win.transform.SetParent(scoreContainer.transform);
            score++;
        }else{
            GameObject win = Instantiate(loseImg);
            win.transform.SetParent(scoreContainer.transform);
        }
    }
}
