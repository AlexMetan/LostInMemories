
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] KeyCode eventKey;   
    [SerializeField] KeyCode inventory;
    [SerializeField] KeyCode torch;
    [SerializeField] KeyCode chargeTorch;
    [SerializeField] KeyCode takeItem;
    [SerializeField] KeyCode cameraOnOff;  
    [SerializeField] KeyCode nightVision;
    public KeyCode EventKey { get => eventKey; set => eventKey = value; }
    public KeyCode Inventory { get => inventory; set => inventory = value; }
    public KeyCode Torch { get => torch; set => torch = value; }
    public KeyCode ChargeTorch { get => chargeTorch; set => chargeTorch = value; }
    public KeyCode TakeItem { get => takeItem; set => takeItem = value; }
    public KeyCode CameraOnOff { get => cameraOnOff; set => cameraOnOff = value; }
    public KeyCode NightVision { get => nightVision; set => nightVision = value; }
}
