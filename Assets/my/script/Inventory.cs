using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
      

    [SerializeField] Image[] listInv;
    [SerializeField] bool[] invFull;
    [SerializeField] Sprite nullInvSprite;
    [SerializeField] GameObject[] ceilObjects;
    [SerializeField] float maxDistanceInvDrop;
    [SerializeField] CameraBattery torch;
    [SerializeField] PaperPicker picker;
    private bool isInvClicked;
    int inventoryCount;
    int index;
    bool inventoryFull;
    
    void Start() { 
        inventoryCount = listInv.Length ;
        isInvClicked=false;
    }
    public int InvCount=>inventoryCount;

    public bool[] InvFull { get => invFull;  }
    public bool IsInvClicked { get => isInvClicked;set =>isInvClicked=value;}
    public bool InventoryFull { get => inventoryFull;  }
    public bool SecurityCard {get;set;}

    public void AddToList(Sprite img,Vector3 size){
        CheckInv();
        if(!InventoryFull){
            for(int i=0;i<listInv.Length;i++){           
                if(!InvFull[i])
                {
                    listInv[i].transform.localScale=size;
                    listInv[i].sprite=img;
                    InvFull[i]=!InvFull[i];
                    return;
                }    
            }
        }
    }
    public void InventoryClick(int index){
        if(listInv[index].transform.localScale.y!=0.6f){
            if(invFull[index]) 
            {
                isInvClicked=true;
                this.index=index;
            }
        }    
    }
    void Update(){
        if(isInvClicked){
        ceilObjects[index].transform.position= Input.mousePosition;
        float posX= ceilObjects[index].transform.localPosition.x;
        float posY= ceilObjects[index].transform.localPosition.y;
            if(Mathf.Abs(posX)>maxDistanceInvDrop||Mathf.Abs(posY)>maxDistanceInvDrop)
            {
                DropItem();
            }
        } 
        else ceilObjects[index].transform.localPosition=Vector2.zero;
        if(Input.GetKeyDown(KeyCode.R))
        {
           TryToChargeTorch();
        }
    }
    void DropItem()
    {
        listInv[index].sprite=nullInvSprite;
        ceilObjects[index].transform.localPosition=Vector2.zero;
        ceilObjects[index].transform.localScale=Vector3.one;
        invFull[index]=false;
        isInvClicked=false;
    }
    public void CheckInv()
    {
        float itemCount=0;
        foreach ( bool full in invFull)
        {
            if(full)
            itemCount++;
        }
        if(itemCount>=ceilObjects.Length)
        inventoryFull=true;
        else inventoryFull=false;
    }
    public void TryToChargeTorch(){
       int ceilCount=listInv.Length-1;
       for (int i = ceilCount; i >=0; i--)
       {
           if(listInv[i].sprite==picker.IMG[1]){
               invFull[i]=false;
               listInv[i].sprite=nullInvSprite;
               torch.ChargeBattery();
               i=0;
           }
           
       }
    }

}
