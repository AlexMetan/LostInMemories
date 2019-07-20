using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AllText : MonoBehaviour
{
    [SerializeField] Text uiText;
    [SerializeField] GameObject textObj;
    [SerializeField] Keys key;
    
    string cameraText;

    
    


    public string CameraText { get => cameraText; set => cameraText = value; }
    public GameObject TextObj { get => textObj; set => textObj = value; }

    void Start() {
        CameraText="Camera? I think i need this..\n[ Press "+key.EventKey.ToString()+" to take camera ]";
    }
    public void SetText(string text)
    {
       uiText.text=text;
    }
    
}
