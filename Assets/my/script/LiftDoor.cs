using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoor : MonoBehaviour
{
    
    bool doorOpen;
    float startPosX;    
    [SerializeField] float doorVector;   
    [SerializeField] LiftMove liftMove;
    [SerializeField] float doorSpeed;
    [SerializeField] LiftButtonMaterial liftButtonMaterial;
  
    public bool DoorOpen { get => doorOpen; set => doorOpen = value; }

    private void Start() 
    {
        startPosX=transform.position.x;     
       
    }
    
    public IEnumerator OpenCloseDoorLift(bool value,float time)
    {   
        
        doorOpen=value;
        Debug.Log(doorOpen);
        yield return new WaitForSeconds(time);
        
        float positionX;
        if(value)positionX=doorVector;else positionX=startPosX;
        Vector3 endV = new Vector3(positionX,transform.position.y,transform.position.z);
        while(transform.position.x!=positionX)
        {
            transform.position= Vector3.MoveTowards(transform.position,endV,doorSpeed);
            yield return null;
        }  
        liftButtonMaterial.ChangeMaterial(0,0); 
        yield break;       
    }
}
