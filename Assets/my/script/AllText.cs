using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AllText : MonoBehaviour
{
    [SerializeField] Text uiTextDialog;
    [SerializeField] Text uiTextEvents;
    [SerializeField] GameObject textObjDialog;
    [SerializeField] GameObject textObjEvent;
    [SerializeField] Keys key;
    bool[] dialog;
    
    [SerializeField] float dialogTime;
    [SerializeField] float longDialogTime;

    [SerializeField] int dialogCount;
    string [] dialogText;
    string [] dialogEvents; 
    bool showDialogEnabled;
    
    


   
   
  
  

    public Text UiTextDialog => uiTextDialog;
    public Text UiTextEvents => uiTextEvents;

    public GameObject TextObjDialog { get => textObjDialog; set => textObjDialog = value; }
    public GameObject TextObjEvent { get => textObjEvent; set => textObjEvent = value; }
    public bool[] Dialog { get => dialog; set => dialog = value; }
    public string[] DialogText { get => dialogText; set => dialogText = value; }
    public string[] DialogEvents { get => dialogEvents; set => dialogEvents = value; }
    public float DialogTime { get => dialogTime; set => dialogTime = value; }
    public float LongDialogTime { get => longDialogTime; set => longDialogTime = value; }

    void Start() {
        dialog= new bool[dialogCount];
        dialogText= new string[dialogCount];
        DialogEvents= new string[dialogCount];
        dialogText[0]="Camera? I think i need this..";   
        dialogEvents[0]="[ Press "+key.EventKey.ToString()+" to take camera ]";   
        dialogEvents[1]="[ Press "+ key.CameraOnOff.ToString()+" to on/off camera]"; 
        dialogEvents[2]="[ Press "+ key.NightVision.ToString()+" to on/off night vision]";         
    }
    public void SetTextEvent(string text){  uiTextEvents.text=text; }
    public void SetActiveText(GameObject obj,bool value){   obj.SetActive(value);   }

    IEnumerator ShowDialog(Text txt,int index,GameObject obj,float time)
    {   
        showDialogEnabled=true;
        if(!dialog[index])
        {
            txt.text=dialogText[index];
            dialog[index]=true;
            obj.SetActive(true);
            yield return new WaitForSeconds(time);
            obj.SetActive(false);
        }
        showDialogEnabled=false;
        yield break;
    }
    public void StopCoroutine_ShowDialog()
    {
        StopAllCoroutines();
    }
    public void Start_Show_Dialog(Text txt,int index,GameObject obj,float time)
    {   
        if(!showDialogEnabled)
        StartCoroutine(ShowDialog(txt,index,obj,time));
    }
}
