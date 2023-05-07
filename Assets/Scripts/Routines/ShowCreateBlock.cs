using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCreateBlock : MonoBehaviour
{
    [SerializeField] GameObject raycastBlock;
    [SerializeField] RoutineBlockBuilder creator;
    [SerializeField] Animator animator;

    Button m_Button;

    bool toggle = false;

    void Start(){
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(() => Toggle());
    }

    void Toggle(){
        toggle = !toggle;

        raycastBlock.SetActive(toggle);
        creator.gameObject.SetActive(toggle);
        creator.Refresh(toggle);

        animator.SetBool("open", toggle);
    }
}
