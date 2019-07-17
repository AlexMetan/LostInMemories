using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraBattery : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject torch;
    bool isTurned;
    [SerializeField] float lighterBattery;
    [SerializeField] float batteryCoefficient;
    [SerializeField] float batteryDownCoefficient;
    [SerializeField] Image torchBatteryImg;
    [SerializeField] Image torchBatteryImgFull;
    [SerializeField] Animation batteryLow;
    [SerializeField] Color batteryUiColor;
    [SerializeField] CameraUi cameraPlayer;
    // Update is called once per frame
    void Update()
    {
       
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
        torchBatteryImg.fillAmount=lighterBattery/batteryDownCoefficient;
    }
    public void ChargeBattery(){
        lighterBattery=batteryDownCoefficient;
        batteryLow.Stop();
        torchBatteryImg.color=batteryUiColor;
        torchBatteryImgFull.color=batteryUiColor;
        
    }
}
