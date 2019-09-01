using System.Collections;
using UnityEngine;

public class PaperPicker : MonoBehaviour
{    
    //layers
    const int inventoryLayer=16;
    const int liftLayer=13;
    const int liftMoveLayer=14;
    const int secureInput=17;
    const int electricHandleLayer=19;
    const int videoCameraLayer=20;
    const int doorLayer=8;
    const int lockerLayer=22;
    [SerializeField] LayerMask rayLayers;

    [Header("Raycast")]
    [SerializeField] float rayLength;
    [Header("Raycast Layers")]
    
    GameObject lift;
    [SerializeField] Camera_Controll cameraMain; 
    [SerializeField] Player_Controller player;
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

    OpenDoor openDoor;
    GameObject currentDoor;
    MaterialChanger materialChanger;
    Transform thisTransform;
    [SerializeField ] PlayerText playerText;
    bool coroutineRunning;
    private Keys key;
    public Sprite[] IMG=>img;
   
    void Start() 
    {
        Cursor.visible=false;
        Cursor.SetCursor(cursor_texture,Vector3.zero,cursorMode);     
        key=GetComponent<Keys>();     
        thisTransform=transform;
    }
    void Update()
    {
        Inventory();   
     
        CheckObject();
    }
    void InventoryItemFinder(RaycastHit rayhit)
    {
        GameObject invObject=rayhit.transform.gameObject;
        InventoryItemInt invInt = invObject.GetComponent<InventoryItemInt>();
        playerText.SetTextVisible(1,true);
        playerText.SetText(1,allText.InventoryItemText(invInt.Name));
        inventory.CheckInv();  
        if(Input.GetKeyDown(key.TakeItem))
        {
            if(invInt.InventoryItemIndex==2)
            {            
                inventory.SecurityCard=true;
            }
            if(!inventory.InventoryFull)
            {
                Destroy(invObject,0.1f);                    
                inventory.AddToList(invInt.ItemImage,invInt.Size);
            }              
            else
            {
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
            Player_Static.InventoryOpen=invIs;
            if(!invIs)
                Player_Static.ShakeValue=.4f;
            cameraMain.SetCursorVisible(invIs);
            player.PlayerStatus=0;
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
                    _lift.OpenCloseDoor(liftMove.Doors,true);                                
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
            liftMove.StartLift(false);
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
        if(!Player_Static.PlayerDie)
        {
            if (Physics.Raycast(thisTransform.position, thisTransform.TransformDirection(Vector3.forward), out hit, rayLength,rayLayers))
            { 
                switch(hit.transform.gameObject.layer)
                {
                    
                    case secureInput:
                        if(Player_Static.Electric)
                        {
                            LiftUnlocked();
                            inputChangeCamera.SecureInput();
                        }
                    break;

                    case liftMoveLayer:
                        LiftMove();
                    break;

                    case liftLayer:
                        if(Player_Static.Electric)
                            Lift(hit);
                    break;

                    case inventoryLayer:
                        InventoryItemFinder(hit);
                    break;

                    case electricHandleLayer:
                        playerText.SetText(1,allText.E_pickCamera);
                        playerText.SetTextVisible(1,true);
                        if(Input.GetKeyDown(key.EventKey))
                        {     
                            electric.Start_HandleMove();                    
                        }
                    break;

                    case videoCameraLayer:
                        GameObject videoCameraObj= hit.transform.gameObject;               
                        if(Input.GetKeyDown(key.EventKey))
                        {                    
                            cameraUi.PickCamera();
                            DestroyObj(videoCameraObj);                            
                        }
                        playerText.SetText(1,allText.E_pickCamera);
                        playerText.SetTextVisible(1,true);
                    break;

                    case doorLayer:
                        if(!Player_Static.DoorText)
                        {
                            playerText.SetText(1,allText.E_door);
                            playerText.SetTextVisible(1,true); 
                        }
                        currentDoor=hit.collider.gameObject;
                        openDoor=currentDoor.GetComponent<OpenDoor>();
                        if(Input.GetKeyDown(key.DoorOpenClose))
                        {
                            Player_Static.DoorText=true;
                            openDoor.OpenDoor_Coroutine();   
                            playerText.SetTextVisible(false);                            
                        }
                    break;   

                    case lockerLayer:
                        if(Player_Static.BlockMovement)
                            playerText.SetTextVisible(1,false);   
                        else 
                            playerText.SetTextVisible(1,true);   
                        if(Input.GetKeyDown(key.DoorOpenClose))
                        {
                            var locker=hit.transform.gameObject.GetComponent<HideInLocker>();
                            locker.Start_IEOpenDoor();
                        } 
                        if(!Player_Static.PlayerInLocker)
                            playerText.SetText(1,allText.E_hide);
                        else 
                        playerText.SetTextVisible(false);
                           
                    break;       
                    default:
                    playerText.SetTextVisible(false);
                    break;          
                }  
               
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

    
