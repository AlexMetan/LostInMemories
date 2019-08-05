using System.Collections;
using UnityEngine;
public class Electric : MonoBehaviour
{
    bool lightIsTurned;
    [SerializeField] GameObject[] lights;
    [SerializeField] Animation[] lightAnimation;
    bool animPlayed;
    [SerializeField] Transform handleTransform;
    [SerializeField] float handleSpeed;
    [SerializeField] float handleAngle;
    [SerializeField] Renderer leftLightElectric;
    [SerializeField] Renderer rightLightElectric;
    [SerializeField] Material greenMaterial;
    [SerializeField] Material redMaterial;
    [SerializeField] Material blackMaterial;
    bool handleMove;
    AudioSource electricitySound;
    bool audioPlayed;
    public bool LightIsTurned { get => lightIsTurned; set => lightIsTurned = value; }

    void Start() {
        electricitySound=GetComponent<AudioSource>();    
    }

    void Update()
    {
        if(lightIsTurned)
        {
            TurnElectric(true);
            
            ChangeMaterial(leftLightElectric,blackMaterial);
            ChangeMaterial(rightLightElectric,greenMaterial);
            if(!animPlayed){
                AnimationPlay();
               
            }
            
        }
    }
    void TurnElectric(bool value)
    {
        foreach (GameObject obj in lights)
        {
            obj.SetActive(value);
        }
        if(!electricitySound.isPlaying&&!audioPlayed)
        {
            electricitySound.Play();
             audioPlayed=true;
        }
    }
    IEnumerator HandleMove()
    {   
        handleMove=true;
        lightIsTurned=true;
        while(handleTransform.transform.localRotation.eulerAngles.z!=handleAngle)
        {
            
            handleTransform.localRotation=Quaternion.RotateTowards(handleTransform.localRotation,Quaternion.AngleAxis(handleAngle,Vector3.forward),handleSpeed) ;
            Debug.Log(handleTransform.localEulerAngles);
            // if(handleTransform.localEulerAngles){
            //      Debug.Log("hello");
            // }
            
            yield return null;
            
        }
        yield break;
        
    }
    public void Start_HandleMove()
    {
        if(!handleMove)
            StartCoroutine(HandleMove());

    }
    void ChangeMaterial(Renderer renderer,Material material)
    {
        renderer.material=material;
    }
    void AnimationPlay()
    {
        foreach (Animation anim in lightAnimation)
        {
            anim.Play();
        }
        animPlayed=true;
    }
}
