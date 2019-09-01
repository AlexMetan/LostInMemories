using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverEffect : MonoBehaviour
{
    Vignette m_vignette;
    PostProcessVolume m_Volume;
    bool vignetteEnable;
    float vigneteIntensity;
    void Start()
    {        
        VegnetteInitialize();
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 110f,m_vignette);
    } 
    void VegnetteInitialize()
    {
        m_vignette=ScriptableObject.CreateInstance<Vignette>();
        m_vignette.enabled.Override(true);
        m_vignette.intensity.Override(0f);       
    }
    void Update()
    {
       if(vigneteIntensity<10)
       {            
            vigneteIntensity+=Time.deltaTime*4;                
            m_vignette.intensity.Override(vigneteIntensity);
       }
       else
       {
           SceneManager.LoadScene(1);
       }
      

    }
}
