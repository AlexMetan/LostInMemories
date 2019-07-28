using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecureButtonClicke : MonoBehaviour
{
    enum ButtonFunction
    {
        number,delete,enter
    }
    [SerializeField] ButtonFunction buttonFunction;
    [SerializeField] int number;
    [SerializeField] SecureInputButtons buttons;
    [SerializeField] float newPosition;
    [SerializeField] float pressSpeed;
    
    Transform buttonTransform;
    float defPosition=0f;    
    bool isClicked;
    void Start()
    {
        buttonTransform=GetComponent<Transform>();
    }
    void OnMouseDown() 
    {   
        if(!isClicked)
        {
        if(buttonFunction==ButtonFunction.number)
            buttons.InputButton(number);
        if(buttonFunction==ButtonFunction.delete)
            buttons.DeleteText();
        if(buttonFunction==ButtonFunction.enter)
            buttons.EnterButton();
        StartCoroutine(ClickButton());           
        }      
    }
    IEnumerator ClickButton()
    {   
        isClicked=true;
        Vector3 newVector= new Vector3(newPosition,0,0);
        while(buttonTransform.localPosition.x!=newPosition)
        {
            buttonTransform.localPosition=Vector3.MoveTowards(buttonTransform.localPosition,newVector,pressSpeed*Time.deltaTime);
            yield return null;
        }
        while(buttonTransform.localPosition.x!=0)
        {
            buttonTransform.localPosition=Vector3.MoveTowards(buttonTransform.localPosition,Vector3.zero,pressSpeed*Time.deltaTime);
            yield return null;
        }
        isClicked=false;
        yield break;
    }
}
