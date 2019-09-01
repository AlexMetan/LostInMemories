using UnityEngine;
using UnityEngine.AI;

public class ZombieMoving : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform [] path;
    [SerializeField] GameObject zombieRay;
    Animator zombieAnimation;
    Transform zombie;
    bool playerDetected;
    byte pathIndex;
    bool stillFollow;
    float timer;
    bool atack;
    public bool PlayerDetected { get => playerDetected; set => playerDetected = value; }
    public bool StillFollow { get => stillFollow; set => stillFollow = value; }
    public float Timer { get => timer; set => timer = value; }
    public Animator ZombieAnimation { get => zombieAnimation; set => zombieAnimation = value; 
    }

    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        zombieAnimation=GetComponent<Animator>();
        zombie=GetComponent<Transform>();
        timer=0;
    }
    void Update()
    {
        if(Player_Static.PlayerDie)
        {
            ZombieFollow(SetPath(),.4f);
            AnimationSpeed(1);
            stillFollow=false;
            zombieAnimation.SetBool("atack",false);
        }
        else
        {
             if(playerDetected)
            {
                ZombieFollow(player.position,1.5f);  
                AnimationSpeed(1.8f);
                zombieRay.SetActive(true);
            }
       
            else
            {            
                if(timer>0)
                {
                    ZombieFollow(player.position,1.5f);
                    AnimationSpeed(1.8f);
                }            
                else 
                {
                    
                    zombieRay.SetActive(false);
                    ZombieFollow(SetPath(),.4f);
                    AnimationSpeed(1);
                    stillFollow=false;
                }
                        
            }
        if(stillFollow)
            timer-=Time.deltaTime;
        } 
    }
    Vector3 SetPath()
    {
        float closeEnough = 1.6f;
        if (Mathf.Abs(zombie.position.x - path[pathIndex].position.x) <= closeEnough && Mathf.Abs(zombie.position.z - path[pathIndex].position.z) <= closeEnough)
        {
            if(pathIndex>=path.Length-1)
            {
                pathIndex=0;
                return path[pathIndex].position;
            }
            else
            {
                pathIndex++;
                return path[pathIndex].position;
            }
        }
        return path[pathIndex].position;
    }
 
    void ZombieFollow(Vector3 pos, float speed)
    {
        
        agent.SetDestination(pos);
        agent.speed=speed;
        
    }
    void AnimationSpeed(float animSpeed)
    {
        zombieAnimation.speed=animSpeed;
    }
    
}
