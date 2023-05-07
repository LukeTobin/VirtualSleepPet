using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechniqueSetup : MonoBehaviour
{
    [System.Serializable]
    public struct TechniqueInfo{
        public string technique;
        [TextArea] public string description;
        public Sprite correspondingImage;
    }

    [System.Serializable]
    public struct DeepList{
        public Transform connector;
        public List<TechniqueInfo> techniques;
    }

    [SerializeField] DeepList improve;
    [SerializeField] DeepList stay;
    [SerializeField] DeepList habit;

    List<GameObject> storedObjects = new List<GameObject>();

    [Header("References")]
    [SerializeField] GameObject techniquePrefab;

    public void Refresh(){
        if(storedObjects.Count > 0){
            foreach(GameObject go in storedObjects){
                go.SetActive(false);
            }
        }

        SortDeepList(improve);
        SortDeepList(stay);
        SortDeepList(habit);
    }

    public void SortDeepList(DeepList dl){
        for(int i = 0;i < dl.techniques.Count;i++){
            GameObject newTechnique = Instantiate(techniquePrefab);
            newTechnique.transform.SetParent(dl.connector, false);
            storedObjects.Add(newTechnique);

            Technique technique = newTechnique.GetComponent<Technique>();
            technique.Set(dl.techniques[i].correspondingImage, dl.techniques[i].technique, dl.techniques[i].description);
        }
    }
}
