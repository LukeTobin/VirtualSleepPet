using UnityEngine;
using UnityEngine.Events;

public class ScreenUI : MonoBehaviour
{   
    [HideInInspector]
    public bool isOpen = false;
    [SerializeField] bool startClosed = true;

    [Space] [SerializeField] private UnityEvent OpenEvent = new UnityEvent();

    private void Start() {
        if(startClosed) {
            gameObject.SetActive(false);
            isOpen = false;
        }else{
            if(!gameObject.activeInHierarchy) gameObject.SetActive(true);
            isOpen = true;
        }
    }

    public void Open(){
        gameObject.SetActive(true);
        isOpen = true;

        OpenEvent.Invoke();
    }

    public void Close(){
        gameObject.SetActive(false);
        isOpen = false;
    }
}
