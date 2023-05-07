using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PetHelp : MonoBehaviour
{
    [SerializeField] Animator animtor;
    [SerializeField] string clipToPlay;

    public void DisplayTip(){
        StopAllCoroutines();
        animtor.SetTrigger(clipToPlay);
    }
}
