using System.Collections;
using System.Collections.Generic;
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
            HandleMove(handleAngle);
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
    void HandleMove(float angle)
    {
        handleTransform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.AngleAxis(angle,Vector3.forward),handleSpeed*Time.deltaTime) ;
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
