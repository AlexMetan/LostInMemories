using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInLift : MonoBehaviour
{
   
    [SerializeField] Lift lift;
    [SerializeField] LiftMove liftMove;
    [SerializeField] float liftCloseTime;
    bool isInLift;
    private void Start() {
        isInLift=false;
    }
    public bool IsInLift { get => isInLift; set => isInLift = value; }
    void OnTriggerEnter(Collider col) {
         if(col.gameObject.tag=="Character")
            isInLift=true;
    }
    void OnTriggerExit(Collider col) 
    {
        if(col.gameObject.tag=="Character")
        {
            lift.OpenCloseDoor(liftMove.Doors,false,liftCloseTime);
            isInLift=false;

        }
    }
}
