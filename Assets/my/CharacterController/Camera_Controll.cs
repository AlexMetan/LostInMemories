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
    [SerializeField] PaperPicker picker;
    [SerializeField] Animation animationScared;
    float rotationY;
    int rotate;
    Quaternion defQ, rotQ;
    bool isRotate;
    bool blockRotation;
    bool blockMovement;

    public bool BlockMovement { get => blockMovement; set => blockMovement = value; }

    // Update is called once per frame
    void Update () {
        if(picker.GetInventoryEnable&&!blockRotation&&!blockMovement){
        if (axes == RotaionAxis.MouseX)
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
        else if (axes == RotaionAxis.MouseY)
        {
            if (!isRotate&&transform.localRotation.y==0){rotationR -= Input.GetAxis("Mouse Y") * sensVertical;
                rotationR = Mathf.Clamp(rotationR, minVert, maxVert);
                rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(rotationR, rotationY, 0); }
            RotateCameraY();
        }
        }
        if(animationScared.isPlaying)
            blockRotation=true;
        else blockRotation =false;
           
        
    }
    IEnumerator Rotate(Quaternion qut) {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation,qut,Time.deltaTime*rotationSpeed);
        yield return null;
    }


void RotateCameraY() { 
            defQ = Quaternion.Euler(transform.localRotation.x, 0, transform.localPosition.z);
            rotQ = Quaternion.Euler(transform.localRotation.x, 145, transform.localPosition.z);

        if (Input.GetKeyDown(KeyCode.X))
            isRotate = true;

        else if (Input.GetKeyUp(KeyCode.X))
            isRotate = false;

        if (isRotate)
    {
        StartCoroutine(Rotate(rotQ));
    }
    else if (!isRotate&& transform.localRotation.y != 0)
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
