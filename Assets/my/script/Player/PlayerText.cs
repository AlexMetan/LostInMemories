using UnityEngine.UI;
using UnityEngine;

public class PlayerText : MonoBehaviour
{
    [Header("Text Objects")]
    [SerializeField] GameObject[] textObjects;
    [Header("Text UI")]
    [SerializeField] Text[] textUI;
    AllText allText;

    public AllText AllText { get => allText; set => allText = value; }

    void Start() 
    {
        allText=GetComponent<AllText>();    
    }
    public void SetText(int textID,string text)
    {
        textUI[textID].text=text;
    }
    public void SetTextVisible(int textID, bool value)
    {
        textObjects[textID].SetActive(value);
    }
    public void SetTextVisible(bool value)
    {
        foreach(GameObject txt in textObjects)
        {
            txt.SetActive(value);
        }
    }
}
