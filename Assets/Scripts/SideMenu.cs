using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenu : MonoBehaviour
{
    public static SideMenu Instance;

    [Header("Side Menu Button")]
    [SerializeField] Animator buttonAnimator;
    [SerializeField] Button sideMenuButton;
    [SerializeField] Button blur;
    [SerializeField] Color openColor = Color.black;
    Color defaultColor;
    Image buttonImage;

    Animator animator;

    bool menuOpen = false;

    const string openAnimation = "open";
    const string closeAnimation = "close";

    void Awake(){
        Instance = this;
        animator = GetComponent<Animator>();
        sideMenuButton.onClick.AddListener(() => ToggleMenu());
        blur.onClick.AddListener(() => ToggleMenu());
        buttonImage = sideMenuButton.gameObject.GetComponent<Image>();
        defaultColor = buttonImage.color;
        blur.gameObject.SetActive(false);
    }

    void ToggleMenu(){
        menuOpen = !menuOpen;

        if(menuOpen) {
            animator.SetTrigger(openAnimation);
            buttonAnimator.SetTrigger(openAnimation);
            buttonImage.color = openColor;
            blur.gameObject.SetActive(true);
        }
        else{
            animator.SetTrigger(closeAnimation);
            buttonAnimator.SetTrigger(closeAnimation);
            buttonImage.color = defaultColor;
            blur.gameObject.SetActive(false);
        }
    }

    public void CloseMenu(){
        menuOpen = false;
        animator.SetTrigger(closeAnimation);
        buttonAnimator.SetTrigger(closeAnimation);
        buttonImage.color = defaultColor;
        blur.gameObject.SetActive(false);
    }
}
