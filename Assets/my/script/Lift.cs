using UnityEngine;

public class Lift : MonoBehaviour
{   
    
    [SerializeField] float speed;
    [SerializeField] PlayerInLift playerInLift;
    [SerializeField] LiftDoor liftDoor;
    [SerializeField] LiftMove liftMove;
    [SerializeField] AudioSource liftDoorAudio;

    public void OpenCloseDoor(LiftDoor[] doorFloor,bool value,float timer)
    {
        if(!liftDoorAudio.isPlaying)
            liftDoorAudio.Play();
            Debug.Log(value);
        foreach (LiftDoor door in doorFloor)
        {
            door.Start_Lift_Door_Coroutine(value,timer);
        }
        
    }
    void Update() {
        if(!playerInLift.IsInLift&&liftDoor.DoorOpen)
        {
            OpenCloseDoor(liftMove.Doors,false,liftMove.LiftCloseTime);             
            return;       
        }
    }

}
