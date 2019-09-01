using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controll : MonoBehaviour {
    public enum RotaionAxis { MouseX=1,MouseY=2 }
    public RotaionAxis axes = RotaionAxis.MouseX;
    public float sensHorizontal = 10.0f;
    public float sensVertical = 10.0f;
    public float minVert =-45;
    public float maxVert = 45;
    public float rotationR = 0;
    public float rotationSpeed;
    [SerializeField] float rotationBackAngle; 
    [SerializeField] PaperPicker picker;
    [SerializeField] Animation animationScared;
    Transform thisTransform;
    float rotationY;
    int rotate;
    Quaternion defQ, rotQ;
    bool isRotate;
    bool blockRotation;
    
    void Start()
    {
        thisTransform=transform;
    }
    // Update is called once per frame
    void Update () {
        
        if(!Player_Static.BlockMovement&&!Player_Static.InventoryOpen)
        {
            if (axes == RotaionAxis.MouseX)
                thisTransform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
            else if (axes == RotaionAxis.MouseY)
            {
                if (!isRotate&&thisTransform.localRotation.y==0)
                {
                    rotationR -= Input.GetAxis("Mouse Y") * sensVertical;
                    rotationR = Mathf.Clamp(rotationR, minVert, maxVert);
                    rotationY = thisTransform.localEulerAngles.y;
                    thisTransform.localEulerAngles = new Vector3(rotationR, rotationY, 0); 
                }
                RotateCameraY();
            }
        }
                  
        
    }
    IEnumerator Rotate(Quaternion qut) {
        thisTransform.localRotation = Quaternion.RotateTowards(thisTransform.localRotation,qut,Time.deltaTime*rotationSpeed);
        yield return null;
    }


    void RotateCameraY() 
    { 
        defQ = Quaternion.Euler(thisTransform.localRotation.x, 0, thisTransform.localPosition.z);
        rotQ = Quaternion.Euler(thisTransform.localRotation.x, rotationBackAngle, thisTransform.localPosition.z);

        if (Input.GetKeyDown(KeyCode.X))
            isRotate = true;

        else if (Input.GetKeyUp(KeyCode.X))
            isRotate = false;

        if (isRotate)
        {
            StartCoroutine(Rotate(rotQ));
        }
        else if (!isRotate&& thisTransform.localRotation.y != 0)
        {
            StartCoroutine(Rotate(defQ));
            rotationR = 0.0001f;
                
        }
    }
    public void SetCursorVisible(bool value) => Cursor.visible = value;
    public void AnimationScared()
    {
        if(axes==RotaionAxis.MouseY)
             animationScared.Play();
    }


}
