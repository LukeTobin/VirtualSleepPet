using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Swipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private RectTransform rectTransform;
    private Vector2 startPosition;
    private LayoutElement layoutElement;
    private Image image;

    [SerializeField] private float snapDistance = 100f;

    [Header("References")]
    [SerializeField] private TMP_Text titleText;

    [Header("Color")]
    [SerializeField] Color removeColor = Color.red;
    [SerializeField] Color completeColor = Color.green;
    [Space]
    [SerializeField] Color defaultRepeating = Color.blue;
    [SerializeField] Color defaultNormal = Color.yellow;

    [Header("Function")]
    [SerializeField] bool deleteAndEdit = false;

    private Color defaultColor;

    private float ticks = 0;

    private Block block;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        layoutElement = GetComponent<LayoutElement>();
        startPosition = rectTransform.anchoredPosition;
        image = GetComponent<Image>();
    }

    public void CreateBlock(Block _block, bool mode){
        block = _block;
        titleText.text = block.Title;

        if(block.Repeating) defaultColor = defaultRepeating;
        else defaultColor = defaultNormal;

        if(!image) image = GetComponent<Image>();
        image.color = defaultColor;

        deleteAndEdit = mode;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        //layoutElement.ignoreLayout = true;
        startPosition = rectTransform.anchoredPosition;
        ticks = 0;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += new Vector2(eventData.delta.x, 0);

        ticks += Time.deltaTime;

        if(ticks >= 0.1f){
            float distanceFromStart = rectTransform.anchoredPosition.x - startPosition.x;

            if (Mathf.Abs(distanceFromStart) > snapDistance) {
                if (distanceFromStart < 0) {
                    image.color = removeColor;
                } else {
                    image.color = completeColor;
                }
            }
            else{
                image.color = defaultColor;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        float distanceFromStart = rectTransform.anchoredPosition.x - startPosition.x;

        if (Mathf.Abs(distanceFromStart) > snapDistance) {
            if (distanceFromStart < 0) {
                // left : delete
                if(deleteAndEdit){
                    SaveData.current.Load();
                    int catchId = -1;
                    for(int i = 0;i < SaveData.current.SleepData.Routines.Count;i++){
                        bool state = false;
                        if(block.Title == SaveData.current.SleepData.Routines[i].Title)
                            if(block.Repeating == SaveData.current.SleepData.Routines[i].Repeating)
                                state = true;
                        if(state){
                            catchId = i;
                            break;
                        }
                    }

                    if(catchId != -1) {
                        SaveData.current.SleepData.Routines.RemoveAt(catchId);
                        SaveManager.Instance.Save(SaveData.current.SleepData);
                    } 

                    gameObject.SetActive(false);
                }
                else{
                    gameObject.SetActive(false);
                }
            } else {
                // right : edit?
                if(!deleteAndEdit){
                    LevelContainer.Instance.TaskComplete();
                    gameObject.SetActive(false);
                }

                gameObject.SetActive(false);
            }
        } else {
            layoutElement.ignoreLayout = false;
            rectTransform.anchoredPosition = startPosition;
        }
    }
}
