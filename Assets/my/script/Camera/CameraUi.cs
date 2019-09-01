using System.Collections;
using UnityEngine;

public class CameraUi : MonoBehaviour
{
    const string animOn="cameraTaking";
    const string animOff="cameraTaking 1";
    const float mainZoom=60;
    [SerializeField] Keys keys;
    bool cameraOn;
    [SerializeField] GameObject cameraObj;
    [SerializeField] Camera playerCamera;
    float zoom = 60f;
    [SerializeField] GameObject[] nightVisionObjects;
    bool nightVision;
    [SerializeField] float maxZoom;
    [SerializeField] float zoomStep;
    [SerializeField] AllText allText;
    [SerializeField] GrayScale grayScale;
    [SerializeField] NightVisionEffect nvGrain;
    [SerializeField] RectTransform zoomImagePos;    
    [SerializeField] float zoomValueStep;
    [SerializeField] Animation cameraAnim;
    [SerializeField] GameObject cameraMesh;
    [SerializeField] float cameraAnimTime;
    float zoomPos;
    float cameraZoomValue;
    bool cameraTutorial;
    bool playerHaveCamera;
    int cameraTutorialStep;
    string animName;
    bool coroutine;
    public bool CameraOn { get => cameraOn; set => cameraOn = value; }
    public bool NightVision { get => nightVision; set => nightVision = value; }
    public bool PlayerHaveCamera { get => playerHaveCamera; set => playerHaveCamera = value; }
    public GameObject CameraObj { get => cameraObj; set => cameraObj = value; }


    void FixedUpdate()
    {  
        if(playerHaveCamera&&!Player_Static.PlayerDie){    
           
            if(Input.GetKeyDown(keys.CameraOnOff))
            {         
                Start_CameraTaking();
            }
           NightVisionStatus();
            if(cameraOn)
            {
                animName=animOff;
                ZoomCamera();
                
                playerCamera.fieldOfView=Mathf.Clamp(playerCamera.fieldOfView,maxZoom,mainZoom);      
            }
            else 
                animName=animOn;
                         
        }
       
    }
    void Start_CameraTaking()
    {
        if(!coroutine)
        {
            if(cameraOn)
                StartCoroutine(IE_CameraOff());  
            else
                StartCoroutine(IE_CameraOn()); 
        } 
    }
    IEnumerator IE_CameraOn()
    {
        coroutine=true;
        cameraMesh.SetActive(true);
        if(!cameraOn)
            cameraAnim.Play(animName);
        yield return new WaitForSeconds(cameraAnimTime);             
        
        cameraMesh.SetActive(false);   
        CameraObj.SetActive(!cameraOn);
        cameraOn=!cameraOn;
        coroutine=false;
        yield break;
    }
    public IEnumerator IE_CameraOff()
    {
        coroutine=true;
        cameraMesh.SetActive(true);
        CameraObj.SetActive(!cameraOn);
        cameraAnim.Play(animName);    
        cameraOn=!cameraOn;     
        playerCamera.fieldOfView=mainZoom;
        yield return new WaitForSeconds(cameraAnimTime);        
        cameraMesh.SetActive(false);               
        coroutine=false;
        yield break;
    }
    void NightVisionStatus()
    {      
        if(cameraOn)
        {
            if(Input.GetKeyDown(keys.NightVision))
            {   
                nightVision=!nightVision;
                NightVisionEffects(nightVision);
            }
        }
        else
        {
            nightVision=false;
            NightVisionEffects(nightVision);
        }
    }
    void ZoomCamera()
    {      
        
        if(cameraOn) playerCamera.fieldOfView-= Input.mouseScrollDelta.y * zoomStep;
        else playerCamera.fieldOfView=mainZoom;   
        cameraZoomValue=playerCamera.fieldOfView;
        cameraZoomValue=Mathf.Clamp(cameraZoomValue,maxZoom,mainZoom);
        zoomPos=cameraZoomValue*zoomValueStep;
        zoomImagePos.localPosition=new Vector3(zoomPos,zoomImagePos.localPosition.y,zoomImagePos.localPosition.z);
    }
    public void NightVisionEffects(bool value)
    {
        foreach (GameObject obj in nightVisionObjects)
        {
            obj.SetActive(NightVision);
        }
        grayScale.enabled=value;
        nvGrain.SetEffect(value);
    }
    public void PickCamera()
    {
        playerHaveCamera=true;       
        cameraTutorial=true;
    }
}
    
