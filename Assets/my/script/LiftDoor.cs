using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoor : MonoBehaviour
{
    bool coroutineActive;
    bool doorOpen;
    float startPosX;    
    [SerializeField] float doorVector;   
    [SerializeField] LiftMove liftMove;
    [SerializeField] float doorSpeed;
    [SerializeField] LiftButtonMaterial liftButtonMaterial;
  
    public bool DoorOpen { get => doorOpen; set => doorOpen = value; }
   
    void Start() 
    {
        startPosX=transform.position.x;     
       
    }
    
    IEnumerator OpenCloseDoorLift(bool value,float time)
    {   
        coroutineActive=true;        
        yield return new WaitForSeconds(time);
        doorOpen=value;
        float positionX;
        if(value)positionX=doorVector;else positionX=startPosX;
        Vector3 endV = new Vector3(positionX,transform.position.y,transform.position.z);
        while(transform.position.x!=positionX)
        {
            transform.position= Vector3.MoveTowards(transform.position,endV,doorSpeed);
            yield return null;
        }  
        // liftButtonMaterial.ChangeMaterial(0,0); 
        coroutineActive=false;
        yield break;       
    }
    public void Start_Lift_Door_Coroutine(bool value,float time)
    {
        if(!coroutineActive)
        {
            StartCoroutine(OpenCloseDoorLift(value,time));
        }
    }
}
