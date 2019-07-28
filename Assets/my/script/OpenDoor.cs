﻿using System.Collections;
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
    
    // Start is called before the first frame update
  
    void Start()
    {
        CheckDoor();      
        audioSourse=GetComponent<AudioSource>();     
    }

    IEnumerator Door()
   {       
        
        if(isOpened)doorAngle=doorCloseDegrees;
        else doorAngle=doorOpenDegrees;
        isOpened=!isOpened;
        audioSourse.Play();
        while(transform.rotation.y!=doorAngle)
        {             
            transform.rotation=Quaternion.RotateTowards(transform.rotation,Quaternion.AngleAxis(doorAngle,Vector3.up),doorSpeed) ;            
            yield return null;
        }   
        yield break;       
    }
    void CheckDoor()
    { 
        if(transform.rotation.eulerAngles.y<=doorOpenDegrees/2f)   isOpened=false;
        else isOpened=true;
    }
    
    public void OpenDoor_Coroutine()
    {
        StopCoroutine(Door());
        StartCoroutine(Door());
       
    }
}
