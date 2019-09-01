    using UnityEngine;

public class ZombieRay : MonoBehaviour
{
    [SerializeField] float rayLength;
    [SerializeField] LayerMask doors;
    const int doorsLayer=8;
    const int lockerLayer=22;
    [SerializeField] ZombieMoving zombie;
    Transform thisTransform;
    void Start()
    {
        thisTransform=transform;
    }
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(thisTransform.position,thisTransform.TransformDirection(Vector3.forward),out hit,rayLength,doors))
        {      
            switch(hit.transform.gameObject.layer)
            {
                case doorsLayer:
                    OpenDoor(hit);
                break;

                case lockerLayer:
                    OpenDoor(hit);
                    HideInLocker hiden =hit.transform.gameObject.GetComponent<HideInLocker>();
                    hiden.ZombieOpenDoor();
                break;
            }
        }       
       
    }
    void OpenDoor(RaycastHit value)
    {
        OpenDoor door=value.transform.gameObject.GetComponent<OpenDoor>();
        if(!door.IsOpened)
        {
            door.OpenDoor_Coroutine();
        }
    }
}
