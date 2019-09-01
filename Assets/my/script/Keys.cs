
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] KeyCode eventKey;   
    [SerializeField] KeyCode inventory;
    [SerializeField] KeyCode videoCam;
    [SerializeField] KeyCode chargeCamera;
    [SerializeField] KeyCode takeItem;
    [SerializeField] KeyCode cameraOnOff;  
    [SerializeField] KeyCode nightVision;
    [SerializeField] KeyCode rotateHeadLeft;
    [SerializeField] KeyCode rotateHeadRight;
    [SerializeField] KeyCode passwordInput;
    [SerializeField] KeyCode doorOpenClose;
    [SerializeField] KeyCode healing;
    public KeyCode EventKey { get => eventKey; set => eventKey = value; }
    public KeyCode Inventory { get => inventory; set => inventory = value; }
    public KeyCode VideoCam { get => videoCam; set => videoCam = value; }
    public KeyCode ChargeCamera { get => chargeCamera; set => chargeCamera = value; }
    public KeyCode TakeItem { get => takeItem; set => takeItem = value; }
    public KeyCode CameraOnOff { get => cameraOnOff; set => cameraOnOff = value; }
    public KeyCode NightVision { get => nightVision; set => nightVision = value; }
    public KeyCode RotateHeadLeft { get => rotateHeadLeft; set => rotateHeadLeft = value; }
    public KeyCode RotateHeadRight { get => rotateHeadRight; set => rotateHeadRight = value; }
    public KeyCode PasswordInput { get => passwordInput; set => passwordInput = value; }
    public KeyCode DoorOpenClose { get => doorOpenClose; set => doorOpenClose = value; }
    public KeyCode Healing { get => healing; set => healing = value; }
}
