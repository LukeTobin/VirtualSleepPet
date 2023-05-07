using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    Button nextButton;
    
    public GameObject nextTutorial;
    [Space]
    public bool onStart;
    public bool endOnMe;

    private void Start() {
        int done = PlayerPrefs.GetInt("tutorial", 0);
        if(done == 1){
            gameObject.SetActive(false);
            return;
        }

        if(nextButton == null) nextButton = GetComponentInChildren<Button>();

        nextButton.onClick.AddListener(() => NextTutorial());

        if(onStart) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    void NextTutorial(){
        gameObject.SetActive(false);

        if(!nextTutorial) {
            if(endOnMe) {
                PlayerPrefs.SetInt("tutorial", 1);
                PlayerPrefs.Save();
            }
            return;
        }

        nextTutorial.gameObject.SetActive(true);
    }
}