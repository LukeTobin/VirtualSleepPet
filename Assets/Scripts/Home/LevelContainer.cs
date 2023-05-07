using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    public static LevelContainer Instance;

    public List<LevelBubble> bubbles = new List<LevelBubble>();
    public LevelUp levelUpButton;
    public SleepData _data;

    [SerializeField] List<LevelState> matchStates = new List<LevelState>(4){LevelState.Empty,LevelState.Empty,LevelState.Empty,LevelState.Empty};

    private void Awake() {
        Instance = this;
    }
    
    private void Start() {
        if(levelUpButton.gameObject.activeInHierarchy) 
            levelUpButton.gameObject.SetActive(false);

        SaveData.current.Load();
        _data = SaveData.current.SleepData;

        for(int i = 0;i < bubbles.Count;i++){
            if(_data.levelStates[i] == LevelState.Empty){
                bubbles[i].LoadState(LevelState.Empty, i);
                matchStates[i] = LevelState.Empty;
            }
            else if(_data.levelStates[i] == LevelState.Stored){
                bubbles[i].LoadState(LevelState.Stored, i);
                matchStates[i] = LevelState.Stored;
            }
            else if(_data.levelStates[i] == LevelState.Leveled){
                bubbles[i].LoadState(LevelState.Leveled, i);
                matchStates[i] = LevelState.Leveled;
            }
        }
    }

    public void TaskComplete(){
        for(int i = 0;i < bubbles.Count;i++){
            if(bubbles[i].state == LevelState.Empty){
                bubbles[i].ChangeState(LevelState.Stored);
                matchStates[i] = LevelState.Stored;
                break;
            }
        }

        CheckForComplete();
    }

    public void SleepQuality(int quality){
        int n = quality;
        for(int i = 0;i < bubbles.Count;i++){
            if(bubbles[i].state == LevelState.Stored){
                n--;
                bubbles[i].ChangeState(LevelState.Leveled);
                matchStates[i] = LevelState.Leveled;
            }

            if(n <= 0) {
                break;
            }
        }

        for(int i = 0;i < bubbles.Count;i++){
            if(bubbles[i].state == LevelState.Stored){
                bubbles[i].ChangeState(LevelState.Empty);
                matchStates[i] = LevelState.Empty;
            }
        }

        CheckForComplete();
    }

    public void CheckForComplete(){
        _data = SaveData.current.SleepData;

        // BRUTE FORCE SAVE
        for (int i = 0; i < bubbles.Count; i++)
        {
            _data.levelStates[i] = matchStates[i];
        }

        SaveManager.Instance.Save(_data);

        for(int i = 0;i < bubbles.Count;i++){
            if(bubbles[i].state != LevelState.Leveled){
                return;
            }
        }

        levelUpButton.gameObject.SetActive(true);
    }

    public void Reset(){
        for(int i = 0;i < bubbles.Count;i++){
            bubbles[i].ChangeState(LevelState.Empty);
            matchStates[i] = LevelState.Empty;
            levelUpButton.gameObject.SetActive(false);
        }
    }
}
