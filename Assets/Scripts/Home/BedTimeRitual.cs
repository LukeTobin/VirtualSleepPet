using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedTimeRitual : MonoBehaviour
{
    [SerializeField] GameObject swipeObjectPrefab;
    [SerializeField] Transform listContainer;
    [SerializeField] int amount;

    List<GameObject> stored = new List<GameObject>();

    private void Start() {
        UpdateList();
    }

    public void UpdateList(){
        SaveData.current.Load();

        if(stored.Count > 0){
            foreach(GameObject go in stored){
                go.SetActive(false);
            }
            stored.Clear();
        }

        List<Block> nonRepeat = new List<Block>();

        for(int i = 0;i < SaveData.current.SleepData.Routines.Count;i++){
            if(SaveData.current.SleepData.Routines[i].Repeating){
                InstObject(SaveData.current.SleepData.Routines[i]);
            }
            else{
                nonRepeat.Add(SaveData.current.SleepData.Routines[i]);
            }
        }

        if(nonRepeat.Count > 0)
            Shuffle(nonRepeat);

        for(int i = 0;i < Mathf.Clamp(amount, 0, nonRepeat.Count);i++){
            InstObject(nonRepeat[i]);
        }

    }

    void InstObject(Block data){
        GameObject newObject = Instantiate(swipeObjectPrefab);
        newObject.transform.SetParent(listContainer, false);
        stored.Add(newObject);

        Swipe swipe = newObject.GetComponent<Swipe>();
        swipe.CreateBlock(data, false);
    }

    // Fisher-Yates
    void Shuffle<T>(List<T> list){
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
}
