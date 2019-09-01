using System.Collections;
using UnityEngine;

public class Player_Controller : MonoBehaviour {
    [Header("Move Speed")]
    public float movespeed;
    public float sittingMoveSpeed;
    public float runningSPeed;
    public float sittingRunSpeed;
    public float jumpHeight;
    public float speedSit;
    [Header("Else")]
    public float gravity = -9.8f;
    public Transform cameraTransform;
    public bool headColl;
    public float defSize, sitSize;
  

  
    
   
   
    //Private
    [SerializeField]
    AnimationCurve jumpCurve;
    CharacterController controller;
    float moveHorizontal;
    float moveVertical;
    bool run;
    bool sit;
    float totalSpeed;
    bool isGrounded;
    Vector3 movement;
    bool isJumping;
    
    [SerializeField] PaperPicker picker;
    Vector3 defV, sitV;
   
    byte playerStatus;

    [SerializeField] AudioSource audioSourse;
    float time;
    float timeStep;
    [SerializeField] float timeStepWalking;
    [SerializeField] float timeStepRunning;
    bool blockShakeCamera;
    Transform thisTransform;
    bool isMoving;
    
    public bool BlockShakeCamera { get => blockShakeCamera; set => blockShakeCamera = value; }
    public byte PlayerStatus { get => playerStatus; set => playerStatus = value; }

    private  void Start ()
    {
        thisTransform=transform;
        defV = new Vector3(cameraTransform.localPosition.x, 1, cameraTransform.localPosition.z);
        sitV = new Vector3(cameraTransform.localPosition.x, 0.5f, cameraTransform.localPosition.z);
        controller = GetComponent<CharacterController>();	
    }

	 private void FixedUpdate ()
     {
        time+=Time.deltaTime;
        if(!Player_Static.BlockMovement&&!Player_Static.InventoryOpen)
        {
            Move();
            Run();
            CheckHead();            
            MoveSound();
        }
        
       PlayerState();
     }


    private void Move()
    { 
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        
              

        if (!run && !sit)
            totalSpeed = movespeed;
        else if (run && !sit)
            totalSpeed = runningSPeed;
        else if (sit && !run)
            totalSpeed = sittingMoveSpeed;
        else totalSpeed = sittingRunSpeed;
       movement = new Vector3(moveHorizontal, 0, moveVertical) * totalSpeed;
                 
        
       
        movement.y -= gravity * Time.deltaTime;
        movement *= Time.deltaTime;
        movement = thisTransform.TransformDirection(movement);
        
        controller.Move(movement);
     
    }
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift)&&IsMoving()) 
            run = true;
        else
             run = false;
    }
    private void SitDown()
    {
        if (sit)
        {
            StartCoroutine(Sit(sitV));
        }
        else if(!sit&&cameraTransform.localPosition.y!=1)
        {
            if (!headColl)
            {
                StartCoroutine(Sit(defV));
            }
           
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            sit = true;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            sit = false;
        }

    }
    // IEnumerator JumpIE() {
    //     controller.slopeLimit = 90;
    //     float timeInJumping=0;

    //     do
    //     {
    //         float jumpForce = jumpCurve.Evaluate(timeInJumping);
    //         controller.Move(Vector3.up * jumpHeight *jumpForce* Time.deltaTime);
    //         timeInJumping += Time.deltaTime;
    //         yield return null;
    //     }
    //     while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);
    //     controller.slopeLimit = 45;
    //     isJumping = false;
        
    // }
    // void Jump() {
    //     if (!isJumping && Input.GetKeyDown(KeyCode.Space)&&!sit)
    //     {
    //         if (!headColl)
    //         {
    //             isJumping = true;
    //             StartCoroutine(JumpIE());
    //         }
        
    //     }
    // }
    IEnumerator Sit(Vector3 pos)
    {
       cameraTransform.localPosition = Vector3.MoveTowards(cameraTransform.localPosition, pos, speedSit * Time.deltaTime);
        if (sit)
            controller.height = sitSize;
        else
            controller.height = defSize;
        yield return null;
    }
    void CheckHead() {
        RaycastHit hit;

        if (Physics.Raycast(thisTransform.position, Vector3.up * 4, out hit))
            headColl = true;
        else headColl = false;
    }
    
   

 
    void MoveSound()
    {
        if(time>=timeStep&&Player_Static.ShakeValue>1)
        {   
            audioSourse.Play();
            time=0;
        }
    }
    bool IsMoving()
    {
        if(moveHorizontal!=0||moveVertical!=0)
            return true;
        else 
            return false;

    }
    public void PlayerState()
    {
        if(run)
        {
            Player_Static.ShakeValue=5;
            timeStep=timeStepRunning;
        }
         
        else if (!run&&IsMoving())
        {
            Player_Static.ShakeValue=2;
            timeStep=timeStepWalking;
        }
        else 
        Player_Static.ShakeValue=.2f;
    }
  
    
}
    


