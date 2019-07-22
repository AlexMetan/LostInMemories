using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{   
    
    [SerializeField] float speed;
    [SerializeField] PlayerInLift playerInLift;
    [SerializeField] LiftDoor liftDoor;
    [SerializeField] LiftMove liftMove;
    [SerializeField] AudioSource audioSource;

    public void OpenCloseDoor(LiftDoor[] doorFloor,bool value,float timer){
        foreach (LiftDoor door in doorFloor)
        {
            StartCoroutine(door.OpenCloseDoorLift(value,timer));
        }
    }
    void Update() {
        if(!playerInLift.IsInLift&&liftDoor.DoorOpen){
            OpenCloseDoor(liftMove.Doors,false,liftMove.LiftCloseTime);   
            audioSource.Play();
            return;       
        }
    }

}
