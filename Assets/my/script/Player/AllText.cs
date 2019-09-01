
using UnityEngine;


public class AllText : MonoBehaviour
{
    //Monologue
    string m_cameraFind;

    //Event 
    string e_pickCamera;
    string e_cameraOn;
    string e_nightVision;
    string e_run;
    string e_healing;
    string e_hide;
    string e_hideExit;
    string e_securityInput;
    string e_door;
    string e_changeBattery;
    [SerializeField] Keys key;

    public string E_pickCamera { get => e_pickCamera; set => e_pickCamera = value; }
    public string E_cameraOn { get => e_cameraOn; set => e_cameraOn = value; }
    public string E_nightVision { get => e_nightVision; set => e_nightVision = value; }
    public string E_run { get => e_run; set => e_run = value; }
    public string E_healing { get => e_healing; set => e_healing = value; }
    public string E_hide { get => e_hide; set => e_hide = value; }
    public string E_securityInput { get => e_securityInput; set => e_securityInput = value; }
    public string E_door { get => e_door; set => e_door = value; }
    public string E_hideExit { get => e_hideExit; set => e_hideExit = value; }
    public string E_changeBattery { get => e_changeBattery; set => e_changeBattery = value; }

    void Start() {
      
        m_cameraFind="Camera? I think i need this..";   
        e_pickCamera=" Press "+key.EventKey.ToString()+" to take camera ";   
        e_cameraOn=" Press "+ key.CameraOnOff.ToString()+" to on/off camera"; 
        e_nightVision=" Press "+ key.NightVision.ToString()+" to on/off night vision";         
        e_run="Hold down"+"to run";
        e_changeBattery="Press "+key.ChargeCamera+" to change battery";
        e_healing="Press "+key.Healing+" to heal";
        e_hide="Press "+key.EventKey+" to hide";
        e_hideExit="Press "+key.EventKey+" to exit";
        e_door="Press "+key.DoorOpenClose+" to open door";
        e_securityInput="Press "+key.PasswordInput+" enter password";
    }
    public string InventoryItemText(string name) 
    {
        return "Press "+key.EventKey+" to take "+name;
    }
    
}
