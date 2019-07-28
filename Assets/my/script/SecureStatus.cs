using UnityEngine;

public class SecureStatus : MonoBehaviour
{
    [SerializeField] Material materialColor;
    [SerializeField] Color unblockedColor;
    [SerializeField] Color lockedColor;
     
    [SerializeField] AudioSource failAudio;
    [SerializeField] AudioSource acceptedAudio;
    [SerializeField] LiftButtonMaterial liftButtonMaterial;
    bool unBlocked;
    public bool UnBlocked { get => unBlocked; set => unBlocked = value; }
    public Color UnblockedColor { get => unblockedColor; set => unblockedColor = value; }
    public Color LockedColor { get => lockedColor; set => lockedColor = value; }
    public AudioSource FailAudio { get => failAudio; set => failAudio = value; }
    public AudioSource AcceptedAudio { get => acceptedAudio; set => acceptedAudio = value; }
    public Material MaterialColor { get => materialColor; set => materialColor = value; }

    void Start() 
    {
        ChangeColor(lockedColor);
    }

    public void ChangeColor(Color color)
    {
        materialColor.SetColor("_EmissionColor",color);
    }
    public void PlayAudio(AudioSource audio)
    {
        if(!audio.isPlaying)
            audio.Play();
    }
    public void ChangeMaterial()
    {
        liftButtonMaterial.ChangeMaterial(0,1);         
    }
    
}
