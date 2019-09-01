
public static class Player_Static 
{
    static bool blockMovement;
    static bool playerDie;
    static bool inventoryOpen;
    static float shakeValue;
    static bool playerVisible;
    static bool playerInLocker;
    static bool doorText;
    static bool electric;
    public static bool BlockMovement { get => blockMovement; set => blockMovement = value; }
    public static bool PlayerDie { get => playerDie; set => playerDie = value; }
    public static bool InventoryOpen { get => inventoryOpen; set => inventoryOpen = value; }
    public static bool PlayerInLocker { get => playerInLocker; set => playerInLocker = value; }
    public static float ShakeValue { get => shakeValue; set => shakeValue = value; }
    public static bool DoorText { get => doorText; set => doorText = value; }
    public static bool Electric { get => electric; set => electric = value; }
}

