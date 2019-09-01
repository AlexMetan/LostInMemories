
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image damageImage;
    [SerializeField] CameraUi cameraUi;
    [SerializeField] GameOverEffect effect;
    Animation dieAnimation;
    float damage=0;    
    Player_Controller controller;    
    Transform thisTransform;
    void Start()
    {
        thisTransform=transform;
        dieAnimation=GetComponent<Animation>();
        controller=GetComponent<Player_Controller>();
        
    }
    public void SetDamage(float damage)
    {
        this.damage+=damage;
        var color = new Color(damageImage.color.r,damageImage.color.g,damageImage.color.b,this.damage);
        damageImage.color=color;
        if(this.damage>=1f)
            FallingDown();
    }
    public void Healing()
    {
        damage=0;
        var color = new Color(damageImage.color.r,damageImage.color.g,damageImage.color.b,this.damage);
        damageImage.color=color;
    }
    void FallingDown()
    {
        Player_Static.BlockMovement=true;  
        Player_Static.PlayerDie=true; 
        StartCoroutine(GameOverAnim());
        effect.enabled=true;
        if(cameraUi.CameraOn)
        {
            StartCoroutine(cameraUi.IE_CameraOff());
            cameraUi.NightVisionEffects(false);
        }   
    }
    IEnumerator GameOverAnim()
    {
        while(thisTransform.eulerAngles.x!=280)
        {
            thisTransform.rotation=Quaternion.RotateTowards(thisTransform.rotation,Quaternion.AngleAxis(-80,Vector3.right),1f);
            yield return null;
        }
        yield  break;
    }
}
