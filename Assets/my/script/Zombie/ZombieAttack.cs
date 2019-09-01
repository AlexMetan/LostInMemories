using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] ZombieMoving zombie;
    [SerializeField] PlayerHealth health;
    bool attack;
    float timer=0;
    
    

    void OnTriggerStay(Collider col) 
    {
        if(col.gameObject.tag=="Character")
        {   
            if(!Player_Static.PlayerDie)
            {
                zombie.ZombieAnimation.SetBool("atack",true);
                attack=true;      
            }
            else
            {
                zombie.ZombieAnimation.SetBool("atack",false);
                attack=false; 
                timer=0; 
            }      
        }    
    }
    void OnTriggerExit(Collider col) 
    {
       if(col.gameObject.tag=="Character")
        {
            zombie.ZombieAnimation.SetBool("atack",false);
            attack=false; 
            timer=0; 
        }    
    }
    void FixedUpdate() 
    {
        if(attack)
            timer+=Time.fixedDeltaTime;
        if(timer>=1f)
        {
            health.SetDamage(.25f);
            timer=0f;
        }
    }
}
    
