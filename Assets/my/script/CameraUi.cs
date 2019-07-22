using UnityEngine;

public class CameraUi : MonoBehaviour
{
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
    bool cameraTutorial;
    bool playerHaveCamera;
    int cameraTutorialStep;
    public bool CameraOn { get => cameraOn; set => cameraOn = value; }
    public bool NightVision { get => nightVision; set => nightVision = value; }
    public bool PlayerHaveCamera { get => playerHaveCamera; set => playerHaveCamera = value; }
    public GameObject CameraObj { get => cameraObj; set => cameraObj = value; }

    void Update()
    {  
         if(playerHaveCamera){    
            TurnOnOffCamera();
            ZoomCamera();
            if(cameraOn)
                playerCamera.fieldOfView=Mathf.Clamp(playerCamera.fieldOfView,maxZoom,mainZoom);
        }
        if(cameraTutorial)
            CameraTutorial();
    }
    void TurnOnOffCamera()
    {
        if(Input.GetKeyDown(keys.CameraOnOff))
        {
            CameraOn=!CameraOn;
            CameraObj.SetActive(CameraOn);            
        }
        if(cameraOn)
        {
            if(Input.GetKeyDown(keys.NightVision))
            {   
                nightVision=!nightVision;
                NightVisionStatus(nightVision);
            }
        }
        else
        {
            CameraObj.SetActive(false);
            nightVision=false;
            NightVisionStatus(nightVision);
        }
    }
    void ZoomCamera()
    {      
        if(cameraOn) playerCamera.fieldOfView-= Input.mouseScrollDelta.y * zoomStep;
        else playerCamera.fieldOfView=mainZoom;   
    }
    void NightVisionStatus(bool value)
    {
        
        foreach (GameObject obj in nightVisionObjects)
        {
            obj.SetActive(NightVision);
        }
    }
    public void PickCamera()
    {
        playerHaveCamera=true;       
        cameraTutorial=true;
    }
    void CameraTutorial()
    {
        allText.SetActiveText(allText.TextObjEvent,true);       
        if(cameraTutorialStep==0){
            allText.SetTextEvent(allText.DialogEvents[1]);
            if(Input.GetKeyDown(keys.CameraOnOff))
            {   
                cameraTutorialStep=1;
                
            }
        }
        if(cameraTutorialStep==1){
            allText.SetTextEvent(allText.DialogEvents[2]);
            if(Input.GetKeyDown(keys.NightVision))
                {
                    allText.SetActiveText(allText.TextObjEvent,false);
                    cameraTutorial=false;
                }
            }
        

    }
}
