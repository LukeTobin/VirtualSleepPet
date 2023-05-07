using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    public static Currency Instance;

    [SerializeField] TMP_Text currText;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        currText.text = GetCurrency().ToString();
    }

    public void Add(int n){
        PlayerPrefs.SetInt("cur", PlayerPrefs.GetInt("cur", 0) + n);
        currText.text = GetCurrency().ToString();
    }

    public int GetCurrency() { return PlayerPrefs.GetInt("cur", 0); }
}
