using System.Collections;
using UnityEngine;
public class Electric : MonoBehaviour
{
    bool lightIsTurned;
    [SerializeField] GameObject[] objects;
    [SerializeField] Material lampMaterial;
  
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
        ChangeLampEmmision(Color.black);
    }

    void Update()
    {
        if(lightIsTurned)
        {
            TurnElectric(true);
            
            ChangeMaterial(leftLightElectric,blackMaterial);
            ChangeMaterial(rightLightElectric,greenMaterial);
            ChangeLampEmmision(Color.white);
            
            
        }
    }
    void TurnElectric(bool value)
    {
        foreach (GameObject obj in objects)
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
        Player_Static.Electric=true;
        handleMove=true;
        lightIsTurned=true;
        while((int)handleTransform.localRotation.eulerAngles.z!=handleAngle)
        {            
            handleTransform.localRotation=Quaternion.RotateTowards(handleTransform.localRotation,Quaternion.AngleAxis(handleAngle,Vector3.forward),handleSpeed); 
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
    void ChangeLampEmmision(Color color)
    {
       lampMaterial.SetColor("_EmissionColor", color);
    }
}
