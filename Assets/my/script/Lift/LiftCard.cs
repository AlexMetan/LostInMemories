using UnityEngine;

public class LiftCard : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Renderer display;
   
   
    Inventory inventory;
    [SerializeField] SecureStatus secureStatus;

    

    void Start()
    {
        inventory=GetComponent<Inventory>();
    }

    public void UnlockDoors(){
        if(inventory.SecurityCard&&!secureStatus.UnBlocked)
        {
            secureStatus.PlayAudio(secureStatus.AcceptedAudio);          
            secureStatus.UnBlocked=true;           
           
        }
    }
}
