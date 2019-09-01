using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] float doorSpeed;
    [SerializeField] float doorOpenDegrees;
    [SerializeField] float doorCloseDegrees=0f;
    float doorAngle;
    bool isOpened;
    AudioSource audioSourse;
    Transform thisTransform;
    public bool IsOpened { get => isOpened; set => isOpened = value; }

    // Start is called before the first frame update

    void Start()
    {    
        audioSourse=GetComponent<AudioSource>();     
        thisTransform=transform;
    }

    IEnumerator Door()
    {       
        
        if(isOpened)
            doorAngle=doorCloseDegrees;
        else 
            doorAngle=doorOpenDegrees;
        isOpened=!isOpened;
        audioSourse.Play();
        float angleLocal;
        if(doorAngle<0)
            angleLocal=360+doorAngle;       
        else 
            angleLocal=doorAngle;        
        while(thisTransform.localRotation.eulerAngles.y!=angleLocal)
        {             
            thisTransform.localRotation=Quaternion.RotateTowards(thisTransform.localRotation,Quaternion.AngleAxis(doorAngle,Vector3.up),doorSpeed) ;
            yield return null;            
        }   
        yield break;       
    }
       
    public void OpenDoor_Coroutine()
    {
        StopCoroutine(Door());
        StartCoroutine(Door());
       
    }
}
