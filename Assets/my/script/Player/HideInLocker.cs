using System.Collections;
using UnityEngine;

public class HideInLocker : MonoBehaviour
{
   
    [Header("Main Transorm")]
    [SerializeField] Transform player;
    [SerializeField] Transform newPos;
    [SerializeField] Transform exitPos;
    float headAngle;
    [Header("Door")]
    Transform door;
    [SerializeField] float openDoorAngle;
    [SerializeField] float closeDoorAngle;
    [SerializeField] float doorSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    AudioSource doorSound;

    bool isHiding;
    bool doorIE;
    bool doorOpen;
    Transform thisTransform;
    public bool IsHiding { get => isHiding; set => isHiding = value; }

    void Start()
    {
        door=GetComponent<Transform>();
        doorSound=GetComponent<AudioSource>();
        thisTransform=transform;
        headAngle=thisTransform.rotation.eulerAngles.y;
    }
    IEnumerator IEOpenDoor(float angle,bool posRot)
    {
        
        doorIE=true;
        Player_Static.BlockMovement=true;
        doorSound.Play();
        float angleLocal;
        float speedLocal;
        if(angle<0)
        {
            angleLocal=360+angle;
            speedLocal=doorSpeed*2;
        }            
        else 
        {
            angleLocal=angle;
            speedLocal=doorSpeed;
        }            
        while(door.localRotation.eulerAngles.y!=angleLocal)
        {
            door.localRotation=Quaternion.RotateTowards(door.localRotation,Quaternion.AngleAxis(angle,Vector3.up),speedLocal);
            yield return null;
        }
        if(posRot)
        {
            StartCoroutine(IEPlayerPosition(newPos));
            StartCoroutine(IEPlayerRotation());
        }
        doorOpen=!doorOpen;
        doorIE=false;
        yield break;
    }
    IEnumerator IEPlayerPosition(Transform pos)
    {
        while(player.position!=pos.position)
        {
            player.position=Vector3.MoveTowards(player.position,pos.position,moveSpeed*Time.deltaTime);
            yield return null;          
        }
        if(Player_Static.PlayerInLocker)
        yield return new WaitForSeconds(.5f);        
        StartCoroutine(IEOpenDoor(closeDoorAngle,false));          
        Player_Static.PlayerInLocker=!Player_Static.PlayerInLocker;       
        Player_Static.BlockMovement=false;        
        yield break;
    }
    IEnumerator IEPlayerRotation()
    {
        while(player.rotation.eulerAngles.y!=headAngle)
        {
            player.rotation=Quaternion.RotateTowards(player.rotation,Quaternion.AngleAxis(headAngle,Vector3.up),rotationSpeed);
            yield return null;          
        }
        yield break;
    }
    public void Start_IEOpenDoor()
    {
        if(!doorIE)
        {
            if(doorOpen)
                StartCoroutine(IEOpenDoor(closeDoorAngle,false));
            else
            {
                if(Player_Static.PlayerInLocker)
                {
                    StartCoroutine(IEOpenDoor(openDoorAngle,false));
                    StartCoroutine(IEPlayerPosition(exitPos));
                }
                else
                {
                    StartCoroutine(IEOpenDoor(openDoorAngle,true));
                }
            }   
        }
    }
    public void ZombieOpenDoor()
    {
        Player_Static.PlayerInLocker=false;
        StopAllCoroutines();
        doorIE=false;
    }
    

    

}
