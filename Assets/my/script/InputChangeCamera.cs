using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChangeCamera : MonoBehaviour
{
    [SerializeField] Transform exitPos;
    Vector3 defPos;
    [SerializeField] float smoothPosition;
    [SerializeField] Transform inputPosition;
    [SerializeField] Keys keys;
    [SerializeField] Player_Controller player_Controller;
    [SerializeField] Camera_Controll camera_Controll;
    [SerializeField] Camera_Controll camera_Controll1;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform head;
    [SerializeField] float rotationPlayer;
    [SerializeField] SecureStatus secureStatus;
    Vector3 newPosition;
    bool isChanging,isChanging2,isChanging3;
    bool isInput;
    

    public bool IsChanging { get => isChanging; set => isChanging = value; }
    public Vector3 DefPos { get => defPos; set => defPos = value; }

    void Start() 
    {
        newPosition=inputPosition.position;
        defPos=exitPos.position;
    }

   
    
    IEnumerator ChangeCamera(Vector3 vec)
    {   
       isChanging=true;
        isInput=!isInput;
        if(isInput)
        SetBoolMovement(isInput);        
            
        while(playerTransform.position!=vec)
        {
           
            playerTransform.position=Vector3.MoveTowards(playerTransform.position,vec,smoothPosition*Time.deltaTime);           
                
           yield return null;
           
        }        
        if(!isInput)
        {
            SetBoolMovement(isInput); 
            Cursor.visible=isInput;
        }
        isChanging=false;
        yield break;
        
        
    }
    IEnumerator RotateCamera()
    {
        isChanging2=true;
        while(playerTransform.rotation.x!=rotationPlayer)
        {
            playerTransform.rotation=Quaternion.RotateTowards(playerTransform.rotation,Quaternion.AngleAxis(0,Vector3.right),smoothPosition);
            yield return null;
        }

        yield break;
        
    }
    IEnumerator RotateHead()
    {
         while(head.rotation.y!=rotationPlayer)
        {
            head.rotation=Quaternion.RotateTowards(head.rotation,Quaternion.AngleAxis(0,Vector3.up),smoothPosition);
            yield return null;
        }

        yield break;
    }
    public void Start_ChangeCamera(Vector3 vec)
    {
        if(!isChanging){
            if(!isInput)
            {
                StartCoroutine(RotateCamera());
                StartCoroutine(RotateHead());
               
            }
            
             StartCoroutine(ChangeCamera(vec));
            
        }
            
    }
    public void SecureInput()
    {
        if(Input.GetKeyDown(keys.PasswordInput)&&!secureStatus.UnBlocked)
        {
            if(!isInput) Start_ChangeCamera(newPosition);
            else Start_ChangeCamera(defPos);
        }
            
    }
    void SetBoolMovement(bool value)
    {
        camera_Controll.BlockMovement=value;
        camera_Controll1.BlockMovement=value;
        player_Controller.BlockMovement=value;
    }
    void Update() 
    {
        if(isInput)
             Cursor.visible=isInput;
    }

}
