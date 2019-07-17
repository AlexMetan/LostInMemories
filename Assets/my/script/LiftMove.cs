using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftMove : MonoBehaviour
{   
    [SerializeField] LiftDoor[] doors;
    
   
    
    
    
    public int Floor { get => floor; set => floor = value; }
    public float LiftCloseTime { get => liftCloseTime; set => liftCloseTime = value; }
    public LiftDoor[] Doors { get => doors; set => doors = value; }

    [SerializeField] GameObject[] floors;
  
    [SerializeField] float liftSpeed;
    [SerializeField] float liftCloseTime;
    int floor;
    [SerializeField] Lift liftDoor;
    [SerializeField] FloorDisplay[] floorDisplay;
    
    
    IEnumerator LiftMoving(int value){
        Door(false,0);
        yield return new WaitForSeconds(liftSpeed);     
         
        foreach (GameObject obj in floors)
        {
            obj.SetActive(false);
        }        
        floors[value].SetActive(true);
        Door(true,0);
        if(value==1){
            foreach (FloorDisplay item in floorDisplay)
            {
                item.ChangeFloor(item.Floor1,item.Floor2);
            }            
        }
        else{
            foreach (FloorDisplay item in floorDisplay)
            {
                item.ChangeFloor(item.Floor2,item.Floor1);
            }            
        }
        
        yield break;    
        
    }
    public void StartLift(bool value,float time){
        Door(value,0);
        if(floor==1) 
        {
            floor=0;
            StartCoroutine(LiftMoving(floor));
        }
        else 
        {
            floor=1;
            StartCoroutine(LiftMoving(floor));      
        }

    }
    public void Door(bool value,float time){
        liftDoor.OpenCloseDoor(doors,value,time);       
    }
}
