﻿using System.Collections;
using UnityEngine;

public class PaperPicker : MonoBehaviour
{
    [SerializeField] float rayLength;
    [SerializeField] LayerMask inventoryLayer;
    [SerializeField] LayerMask liftLayer;
    [SerializeField] LayerMask liftMoveLayer;
    [SerializeField] LayerMask liftCardLayer;
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
    [SerializeField] UIscript uiScript;
    private Keys key;
    public Sprite[] IMG=>img;
    void Start() {
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
         
        if(Input.GetKeyDown(key.TakeItem)){
            GameObject invObject=rayhit.transform.gameObject;
            InventoryItemInt invInt = invObject.GetComponent<InventoryItemInt>();
            inventory.CheckInv();
            if(invInt.InventoryItemIndex==2)
                inventory.SecurityCard=true;
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
        obj.SetActive(active);
        yield return new WaitForSeconds(time);
        obj.SetActive(!active);
        yield return new WaitForSeconds(0f);
    }
    void Lift(RaycastHit rayhit)
    {
        if(Input.GetKeyDown(key.EventKey))
        {
            if(liftCard.Unlocked)
            {
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
                liftCard.Unlocked=true; 
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,liftCardLayer))
        {
            LiftUnlocked();
            uiScript.HandActive(true);
        }            
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,liftMoveLayer))
        {
            LiftMove();
            uiScript.HandActive(true);
        }      
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,liftLayer))
        {
            Lift(hit);
            uiScript.HandActive(true);
        }                
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength,inventoryLayer))
        {
            InventoryItemFinder(hit);
            uiScript.HandActive(true);
        }   
        else uiScript.HandActive(false);
        
    }
    
}

    