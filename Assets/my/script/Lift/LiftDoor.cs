using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoor : MonoBehaviour
{
    bool coroutineActive;
    bool doorOpen;
    float startPosX;        
    [SerializeField] float doorVector;       
    [SerializeField] float doorSpeed;
    [SerializeField] LiftButtonMaterial liftButtonMaterial;
    bool doorMoving;
    public bool DoorOpen { get => doorOpen; set => doorOpen = value; }
    public bool DoorMoving { get => doorMoving; set => doorMoving = value; }
    Transform thisTransform;
    void Start() 
    {
        startPosX=transform.position.x;     
        thisTransform=transform;
       
    }
    
    IEnumerator OpenCloseDoorLift(bool value)
    {   
        coroutineActive=true;        
        doorOpen=value;
        doorMoving=true;
        float positionX;         
        if(value)positionX=doorVector;
        else positionX=startPosX;
        Vector3 endV = new Vector3(positionX,thisTransform.position.y,thisTransform.position.z);
       
        while(thisTransform.position.x!=positionX)
        {
            thisTransform.position= Vector3.MoveTowards(thisTransform.position,endV,doorSpeed);
            yield return null;
        }  
        // liftButtonMaterial.ChangeMaterial(0,0); 
        coroutineActive=false;
        doorMoving=false;
        yield break;       
    }
    public void Start_Lift_Door_Coroutine(bool value)
    {
        if(!coroutineActive)
        {
            StartCoroutine(OpenCloseDoorLift(value));
        }
    }
}
