using UnityEngine;

public class Lift : MonoBehaviour
{   
    
    [SerializeField] float speed;
    
    [SerializeField] LiftDoor liftDoor;
    [SerializeField] LiftMove liftMove;
    [SerializeField] AudioSource liftDoorAudio;

    public void OpenCloseDoor(LiftDoor[] doorFloor,bool value)
    {
        if(!liftDoorAudio.isPlaying)
            liftDoorAudio.Play();
        
        foreach (LiftDoor door in doorFloor)
        {
            door.Start_Lift_Door_Coroutine(value);
        }
        
    }
 

}
