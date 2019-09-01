using UnityEngine;
using UnityEngine.UI;
public class CameraBattery : MonoBehaviour
{        
    [SerializeField] float lighterBattery;
    [SerializeField] float batteryCoefficient;
    [SerializeField] float batteryDownCoefficient;
    [SerializeField] Image cameraBatteryImg;
    [SerializeField] Image cameraBatteryImgFull;
    [SerializeField] Animation batteryLow;
    [SerializeField] Color batteryUiColor;
    [SerializeField] CameraUi cameraPlayer;
    [SerializeField] Inventory inventory;
    [SerializeField] Keys keys;
    
    void Update()
    {
        if(Input.GetKeyDown(keys.ChargeCamera))
        {
           if(inventory.CheckItemExist(1))
           {
               ChargeBattery();
           }

        }
       
        if(lighterBattery<=batteryDownCoefficient*0.2f&&!batteryLow.isPlaying)
        {
            batteryLow.Play();            
        }
        if(cameraPlayer.CameraOn)
        {
            if(cameraPlayer.NightVision) CameraBatteryStatus(3);
            else CameraBatteryStatus(1);                
        }
        if(lighterBattery<=0)
            cameraPlayer.CameraOn=false;
       
    }
    
    void CameraBatteryStatus(float battery){
        lighterBattery-=Time.fixedDeltaTime*batteryCoefficient*battery;
        cameraBatteryImg.fillAmount=lighterBattery/batteryDownCoefficient;
    }
    public void ChargeBattery(){
        lighterBattery=batteryDownCoefficient;
        batteryLow.Stop();
        cameraBatteryImg.color=batteryUiColor;
        cameraBatteryImgFull.color=batteryUiColor;
        
    }
}
