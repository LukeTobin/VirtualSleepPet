using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    [SerializeField] ScreenUI screenToOpen;

    Button button;

    void Start(){
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Open());
    }

    void Open(){
        ScreenHandler.Instance.OpenScreen(screenToOpen);
        SideMenu.Instance.CloseMenu();
    }
}
