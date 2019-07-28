using System.Collections;
using System.Collections.Generic;
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
    public float doorFinderLenght;
    public bool canEnterToCar;
    public LayerMask layerMask;
    
   
   
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
    [SerializeField] OpenDoor openDoor;
    [SerializeField] GameObject currentDoor;
    [SerializeField] PaperPicker picker;
    Vector3 defV, sitV;
    [SerializeField] Transform head;
    float defHeadPosY;
    float newHeadPosY;
    [SerializeField] float newHeadPosIdle;
    [SerializeField] float newHeadPosMove;
    [SerializeField] float newHeadPosRun;
    
    [SerializeField] float smoothCameraIdle;
    [SerializeField] float smoothCameraMove;
    [SerializeField] float smoothCameraRun;
    float smoothCamera;
    bool isShake;
    byte playerStatus;

    [SerializeField] AudioSource audioSourse;
    float time;
    float timeStep;
    [SerializeField] float timeStepWalking;
    [SerializeField] float timeStepRunning;
    bool blockMovement;

    public bool BlockMovement { get => blockMovement; set => blockMovement = value; }

    private  void Start ()
     {
         defHeadPosY=head.position.y;
        defV = new Vector3(cameraTransform.localPosition.x, 1, cameraTransform.localPosition.z);
        sitV = new Vector3(cameraTransform.localPosition.x, 0.5f, cameraTransform.localPosition.z);

        controller = GetComponent<CharacterController>();	
	 }

	 private void Update ()
     {
        time+=Time.deltaTime;
        if(picker.GetInventoryEnable&&!blockMovement)
        {
            Move();
            Run();
            SitDown();
            Jump();
            CheckHead();
            DoorFinder();
            MoveSound();
        }
     }


    private void Move()
    { 
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        
        ChechPlayerStatus();
        ShakeCamera();

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
        movement = transform.TransformDirection(movement);
        controller.Move(movement);
    }
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift)) run = true;
        else run = false;
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
    IEnumerator JumpIE() {
        controller.slopeLimit = 90;
        float timeInJumping=0;

        do
        {
            float jumpForce = jumpCurve.Evaluate(timeInJumping);
            controller.Move(Vector3.up * jumpHeight *jumpForce* Time.deltaTime);
            timeInJumping += Time.deltaTime;
            yield return null;
        }
        while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);
        controller.slopeLimit = 45;
        isJumping = false;
        
    }
    void Jump() {
        if (!isJumping && Input.GetKeyDown(KeyCode.Space)&&!sit)
        {
            if (!headColl)
            {
                isJumping = true;
                StartCoroutine(JumpIE());
            }
        
        }
    }
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

        if (Physics.Raycast(transform.position, Vector3.up * 4, out hit))
            headColl = true;
        else headColl = false;
    }

    void DoorFinder()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, doorFinderLenght,layerMask))
        {   
            currentDoor=hit.collider.gameObject;
            openDoor=currentDoor.GetComponent<OpenDoor>();
            if(Input.GetKeyDown(KeyCode.E)){
               
                openDoor.OpenDoor_Coroutine();
            }
        }
         
    }
    void ShakeCamera()
    {   float posY;
        if(isShake)
           posY=newHeadPosY;
        else
            posY=defHeadPosY;
        Vector3 newVector3= new Vector3(head.position.x,posY,head.position.z);
        head.position=Vector3.MoveTowards(head.position,newVector3,Time.deltaTime*smoothCamera);
        if(head.position.y==newVector3.y)
            isShake=!isShake;
    }
    void ChechPlayerStatus()
    {
        if(run)
        {        
            smoothCamera=smoothCameraRun;
            newHeadPosY=newHeadPosRun;
            playerStatus=2;
            timeStep=timeStepRunning;
        }
        else if(moveHorizontal!=0||moveVertical!=0)
        {
            smoothCamera=smoothCameraMove;
            newHeadPosY=newHeadPosMove;
            playerStatus=1;
            timeStep=timeStepWalking;
        }
        
        else
        {
            smoothCamera=smoothCameraIdle; 
            newHeadPosY=newHeadPosIdle; 
            playerStatus=0;           
        }

        
    }
    void MoveSound()
    {
        if(time>=timeStep&&playerStatus!=0)
        {        
            audioSourse.Play();
            time=0;
        }
    }
}
    


