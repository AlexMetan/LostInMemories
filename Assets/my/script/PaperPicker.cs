using System.Collections;
using UnityEngine;

public class PaperPicker : MonoBehaviour
{
    [SerializeField] float rayLength;
    [SerializeField] LayerMask inventoryLayer;
    [SerializeField] LayerMask liftLayer;
    [SerializeField] LayerMask liftMoveLayer;
    [SerializeField] LayerMask secureInput;
    [SerializeField] LayerMask electricHandleLayer;
    [SerializeField] LayerMask videoCameraLayer;
    GameObject lift;
    [SerializeField] Camera_Controll cameraMain; 
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject inv;
    bool invIs=true;
    [SerializeField] Sprite[] img;
    [SerializeField] Texture2D cursor_texture;
    [SerializeField] CursorMode cursorMode;
    [SerializeField] GameObject text;
    [SerializeField] LiftMove liftMove;
    [SerializeField] LiftButtonMaterial liftButtonMaterial;
    [SerializeField] LiftDoor LiftDoor;
    [SerializeField] LiftCard liftCard;
    [SerializeField] PlayAudio playAudio;
    [SerializeField] AudioSource accessDeniedSound;
    [SerializeField] Electric electric;
    [SerializeField] AllText allText;
    [SerializeField] float textUiTime;
    [SerializeField] CameraUi cameraUi;
    [SerializeField] AudioSource liftButton;
    [SerializeField] InputChangeCamera inputChangeCamera;
    [SerializeField] SecureStatus secureStatus;
    MaterialChanger materialChanger;
    bool coroutineRunning;
    private Keys key;
    public Sprite[] IMG=>img;
    void Start() 
    {
        Cursor.visible=false;
        Cursor.SetCursor(cursor_texture,Vector3.zero,cursorMode);     
        key=GetComponent<Keys>();     
    }
    void Update()
    {
        Inventory();   
       
        CheckObject();
    }
    void InventoryItemFinder(RaycastHit rayhit){
         GameObject invObject=rayhit.transform.gameObject;
           
            materialChanger= invObject.GetComponent<MaterialChanger>();
            
            // materialChanger.ChangeMat(1);
        if(Input.GetKeyDown(key.TakeItem)){
            
            InventoryItemInt invInt = invObject.GetComponent<InventoryItemInt>();
            
            inventory.CheckInv();
            if(invInt.InventoryItemIndex==2)
            {            
                inventory.SecurityCard=true;
            }
            if(!inventory.InventoryFull){
                Destroy(invObject,0.1f);                    
                inventory.AddToList(invInt.ItemImage,invInt.Size);
            }              
            else{
                StopCoroutine("SetActiveTimer");
                StartCoroutine(SetActiveTimer(2,text,true));
            }
            
        }        
    }
    void Inventory()
    {     
        if(Input.GetKeyDown(key.Inventory))
        {                
            inv.SetActive(invIs);
            cameraMain.SetCursorVisible(invIs);
            invIs=!invIs;
        }
               
    }
        
    public bool GetInventoryEnable => invIs;

    IEnumerator SetActiveTimer(float time,GameObject obj,bool active)
    {
        coroutineRunning=true;
        obj.SetActive(active);
        yield return new WaitForSeconds(time);
        obj.SetActive(!active);
        coroutineRunning=false;
        yield break;
    }
    void Lift(RaycastHit rayhit)
    {
        if(Input.GetKeyDown(key.EventKey))
        {
            if(secureStatus.UnBlocked)
            {
                liftButton.Play();
                if(!LiftDoor.DoorOpen)
                {
                    lift=rayhit.transform.gameObject;                
                    Lift _lift= lift.GetComponent<Lift>();
                    _lift.OpenCloseDoor(liftMove.Doors,true,0);                                
                }
            }
            else
            {                    
                playAudio.PlaySound(accessDeniedSound);
            } 
                    
        }
    }
    void LiftMove(){
        
        if(Input.GetKeyDown(key.EventKey))
        {   
             liftButton.Play();        
            liftMove.StartLift(true,0);
            liftButtonMaterial.ChangeMaterial(1,1);
        }
        
    }
    void LiftUnlocked()
    {
        if(Input.GetKeyDown(key.EventKey))
        {     
            if(inventory.SecurityCard)
            {
                secureStatus.UnBlocked=true; 
                liftCard.UnlockDoors();
            }
            else
            {
                playAudio.PlaySound(accessDeniedSound);
            }     
        }                        
    }
    void CheckObject(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,secureInput))
        {
            LiftUnlocked();
            inputChangeCamera.SecureInput();
            
        }            
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,liftMoveLayer))
        {
            LiftMove();
           
        }      
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,liftLayer))
        {
            Lift(hit);
            
        }                
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,inventoryLayer))
        {
            InventoryItemFinder(hit);           
        }   
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,electricHandleLayer))
        {
            if(Input.GetKeyDown(key.EventKey))
            {     
                electric.Start_HandleMove();
               
            }
        } 
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,videoCameraLayer))
        {
            GameObject videoCameraObj= hit.transform.gameObject;
            allText.SetTextEvent(allText.DialogEvents[0]);
            SetActiveObject(allText.TextObjEvent,true);
            allText.Start_Show_Dialog(allText.UiTextDialog, 0,allText.TextObjDialog,allText.DialogTime);
            if(Input.GetKeyDown(key.EventKey))
            {                    
                cameraUi.PickCamera();
                DestroyObj(videoCameraObj);
                
            
            }
            
        } 

        
        
    }
    void DestroyObj(GameObject objToDestroy)
    {
        Destroy(objToDestroy);
    }
    public void SetActiveObject(GameObject obj,bool value)
    {
        obj.SetActive(value);
    }
    
}

    
