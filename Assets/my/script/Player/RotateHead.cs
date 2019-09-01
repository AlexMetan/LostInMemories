using UnityEngine;

public class RotateHead : MonoBehaviour
{
    Transform head;
    [SerializeField] Keys keys;
    [SerializeField] float angle;
    [SerializeField] float newPos;
    [SerializeField] float headRotSmooth;
    [SerializeField] float headPosSmooth;
    Vector3 defPosition;
 
    
    void Start()
    {
        head=GetComponent<Transform>();
      
    }

   
    void FixedUpdate()
    {

        if(Input.GetKey(keys.RotateHeadLeft))
            RotateAndPosHead(-newPos,angle);
            
        else if(Input.GetKey(keys.RotateHeadRight))
            RotateAndPosHead(newPos,-angle);
        else  RotateAndPosHead(0,0);
      
         
    }   
    void RotateAndPosHead(float position,float rotation)
    {   
        Vector3 newPosition= new Vector3(position,head.localPosition.y,head.localPosition.z);       
        head.localPosition=Vector3.MoveTowards(head.localPosition,newPosition,Time.deltaTime*headPosSmooth);
        head.localRotation=Quaternion.RotateTowards(head.localRotation,Quaternion.AngleAxis(rotation,Vector3.forward),headRotSmooth);
    }
}
