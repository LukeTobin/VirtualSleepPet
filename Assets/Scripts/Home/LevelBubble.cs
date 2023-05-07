using UnityEngine;
using UnityEngine.UI;

public class LevelBubble : MonoBehaviour
{
    public LevelState state = LevelState.Empty;

    [Header("Colouring")]
    public Color emptyColor = Color.white;
    public Color leveledColor = Color.green;
    public Color storedColor = Color.blue;

    Image image;
    public int BubbleIndex {get;set;}

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void ChangeState(LevelState _state){
        state = _state;
        
        SaveData.current.Load();

        switch(state){
            case LevelState.Empty:
                image.color = emptyColor;
                break;
            case LevelState.Leveled:
                image.color = leveledColor;
                break;
            case LevelState.Stored:
                image.color = storedColor;
                break;
        }

        SaveData.current.SleepData.levelStates[BubbleIndex] = _state;
        SaveManager.Instance.Save(SaveData.current.SleepData);
    }

    public void LoadState(LevelState _state, int index){
        state = _state;
        BubbleIndex = index;
        
        switch(state){
            case LevelState.Empty:
                image.color = emptyColor;
                break;
            case LevelState.Leveled:
                image.color = leveledColor;
                break;
            case LevelState.Stored:
                image.color = storedColor;
                break;
        }
    }
}