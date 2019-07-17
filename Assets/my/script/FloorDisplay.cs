using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDisplay : MonoBehaviour
{
    [SerializeField] GameObject[] floor1;
    [SerializeField] GameObject[] floor2;

    public GameObject[] Floor2 { get => floor2; set => floor2 = value; }
    public GameObject[] Floor1 { get => floor1; set => floor1 = value; }
   
    public void ChangeFloor(GameObject[] obj1,GameObject[] obj2)
    {
        ChangeNumber(obj1,false);
        ChangeNumber(obj2,true);

        
    }
    void ChangeNumber(GameObject[] obj,bool value)
    {
        if(!value) Debug.Log("ddd");
        foreach (GameObject numbers in obj)
        {
            numbers.SetActive(value);
        }
    }
}
