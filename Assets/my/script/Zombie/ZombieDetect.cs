using UnityEngine;

public class ZombieDetect : MonoBehaviour
{
    [SerializeField] float timeConst;
    ZombieMoving zombie;
    void Start()
    {
        zombie=GetComponent<ZombieMoving>();
    }
    void OnTriggerStay(Collider col) 
    {
        if(!Player_Static.PlayerInLocker)
        {
            if(col.gameObject.tag=="Character")
            {
                zombie.PlayerDetected=true;
            }
        }
        
    }
    void OnTriggerExit(Collider col) 
    {
       if(col.gameObject.tag=="Character")
        {
            zombie.PlayerDetected=false;
            zombie.StillFollow=true;
            zombie.Timer=timeConst;
        } 
    }
    void Update() 
    {
        if(!zombie.PlayerDetected&&zombie.StillFollow)
        {
            if(Player_Static.PlayerInLocker)
            {
                zombie.StillFollow=false;
               
            }    
        }
 
    }
}
