using UnityEngine;

public class LiftCard : MonoBehaviour
{
    [SerializeField] Renderer display;
    [SerializeField] Material displayMaterial;
    [SerializeField] LiftButtonMaterial liftButtonMaterial;
    Inventory inventory;
    bool unlocked;

    public bool Unlocked { get => unlocked; set => unlocked = value; }

    void Start()
    {
        inventory=GetComponent<Inventory>();
    }

    public void UnlockDoors(){
        if(inventory.SecurityCard)
        {
            display.material=displayMaterial;
            unlocked=true;
            liftButtonMaterial.ChangeMaterial(0,1);
        }
    }
}
