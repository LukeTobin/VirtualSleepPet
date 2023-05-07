using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableStartup : MonoBehaviour
{
    [SerializeField] List<GameObject> disableOnStart = new List<GameObject>();

    void Start()
    {
        for(int i = 0;i < disableOnStart.Count;i++)
            disableOnStart[i].SetActive(false);
    }
}
