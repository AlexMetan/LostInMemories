using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMan : MonoBehaviour
{
    [SerializeField] Transform deadWithHook;
    [SerializeField] float newPosition;
    [SerializeField] float smoothPosition;
    [SerializeField] Animation animationRotation;
    [SerializeField] AudioSource screamSFX;
    [SerializeField] GameObject lightAnim;
    [SerializeField] Camera_Controll camera_palyer;
    bool activateAnimtion;
    bool wasActivated;
   
    
    void Update()
    {
        if(activateAnimtion&&!wasActivated)
        {
            AnimationDead();
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(!wasActivated){
            if(col.gameObject.tag=="Character")
                activateAnimtion=true;
            }
    }
    void AnimationDead()
    {
        screamSFX.Play();
        Vector3 newVector= new Vector3(deadWithHook.position.x,newPosition,deadWithHook.position.z);
        deadWithHook.position=Vector3.MoveTowards(deadWithHook.position,newVector,Time.deltaTime*smoothPosition);
        animationRotation.Play();        
        lightAnim.SetActive(true);
        camera_palyer.AnimationScared();
        if(deadWithHook.position.y==newPosition){
            wasActivated=true;
        }
            
        
        
    }
}
