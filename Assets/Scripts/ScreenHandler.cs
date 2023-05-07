using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour
{
    public static ScreenHandler Instance;

    [SerializeField] List<ScreenUI> screens = new List<ScreenUI>();

    ScreenUI lastScreen = null;

    private void Awake() {
        Instance = this;
    }

    public void OpenScreen(ScreenUI screen, bool record = true){
        for(int i = 0;i < screens.Count;i++){
            if(screens[i].isOpen) {
                lastScreen = screens[i];
                screens[i].Close();
            }
        }

        screen.Open();
    }
}
