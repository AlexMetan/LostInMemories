using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Keys keys;
    PlayerHealth playerHealth;
    void Start()
    {
        playerHealth=GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keys.Healing)&&!Player_Static.PlayerDie)
        {
            if(inventory.CheckItemExist(3))
            {
                playerHealth.Healing();
            }
        }
    }
}
