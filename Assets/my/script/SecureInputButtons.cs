
using System.Collections.Generic;
using UnityEngine;

public class SecureInputButtons : MonoBehaviour
{   
    
    bool limitOfNumbers;
    [SerializeField] float valueCount;
    [SerializeField] TextMesh text;
    List <int> number;
    string textS;
    string newText;
    [SerializeField] string password;
    [SerializeField] SecureStatus secureStatus;    
    [SerializeField] InputChangeCamera changeCamera;
    [SerializeField] LiftCard liftCard;
    public TextMesh Text { get => text; set => text = value; }

    void Start() 
    {
        number = new List<int>();
        
    }
  
    public void InputButton(int value)
    {
        if(number.Count<valueCount)
        {
            number.Add(value);
            SetNumberFromList();
            NumberOnDisplay();
        }
        
        
    }
    public void DeleteText()
    {
        if(number.Count>0)
        {
            number.RemoveAt(number.Count-1);
            SetNumberFromList();
            NumberOnDisplay();
        }
    }
    void CheckCount()
    {
        if(number.Count>=valueCount)
        {
            limitOfNumbers=true;
        }
        else limitOfNumbers=false;
    }
    void NumberOnDisplay()
    {        
        Text.text=newText;       
    }
    void SetNumberFromList()
    {
        newText="";
        foreach (int item in number)
        {
            newText+=item.ToString();
        }
    }
    public void EnterButton()
    {
        if(newText==password)
        {
            newText="Unblocked";
            NumberOnDisplay();
            secureStatus.UnBlocked=true;
            secureStatus.ChangeColor(secureStatus.UnblockedColor);
            secureStatus.PlayAudio(secureStatus.AcceptedAudio);
            changeCamera.Start_ChangeCamera(changeCamera.DefPos);
            secureStatus.ChangeMaterial();
        }
        else 
        {
            newText="Blocked";
            number.Clear();
            NumberOnDisplay();
            secureStatus.PlayAudio(secureStatus.FailAudio);
        }
    }
   
    
    


}
