using System.Collections;
using UnityEngine;

public class LiftMove : MonoBehaviour
{
    const string animName = "liftMovingShake";
    [SerializeField] LiftDoor[] doors;
    [SerializeField] GameObject[] floors;  
    [SerializeField] float liftSpeed;    
    [SerializeField] PlayerAnimation playerAnimation;
    int floor;
    [SerializeField] Lift lift;
    [SerializeField] FloorDisplay[] floorDisplay;   
    [SerializeField] AudioSource liftMoveSound;
    bool liftMoving;
    public int Floor { get => floor; set => floor = value; }
    public LiftDoor[] Doors { get => doors; set => doors = value; }
   
    void Start()
    {
        liftMoveSound=GetComponent<AudioSource>();
    }
    IEnumerator LiftMoving(int value){
        liftMoving=true;
        Door(false);                             
        yield return new WaitForSeconds(2f);
        liftMoveSound.Play();
        // playerAnimation.PlayAnimationPlayer(animName);
        yield return new WaitForSeconds(liftSpeed/1.2f); 
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
        
        foreach (GameObject obj in floors)
        {
            obj.SetActive(false);
        }        
        floors[value].SetActive(true);
        yield return new WaitForSeconds(liftSpeed/2); 
        liftMoving=false;
        Door(true);
        
        yield break;    
        
    }
    public void StartLift(bool value){
        if(!liftMoving)
        {
            Door(value);
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
    }
    public void Door(bool value){
        if(!liftMoving)
        {
            lift.OpenCloseDoor(doors,value);       
        }
    }
}
