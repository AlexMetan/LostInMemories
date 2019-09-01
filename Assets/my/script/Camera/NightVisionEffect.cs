using UnityEngine.Rendering.PostProcessing;
using UnityEngine;


public class NightVisionEffect : MonoBehaviour
{
    [SerializeField] float grainIntensive;
    [SerializeField] float grainSize;
    PostProcessVolume m_Volume;
    Grain m_Grain;
    Vignette m_vignette;

    void Start()
    {
        GrainInitialize();
        VegnetteInitialize();
       

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Grain,m_vignette);
    } 

    public void SetEffect(bool value)
    {
        m_Grain.enabled.Override(value);     
        m_vignette.enabled.Override(value);
    }
    void GrainInitialize()
    {
        m_Grain = ScriptableObject.CreateInstance<Grain>();
        m_Grain.enabled.Override(false);
        m_Grain.intensity.Override(grainIntensive);
        m_Grain.size.Override(grainSize);
        m_Grain.colored.Override(false);
    }
    void VegnetteInitialize()
    {
        m_vignette=ScriptableObject.CreateInstance<Vignette>();
        m_vignette.enabled.Override(false);
        m_vignette.intensity.Override(.5f);
        m_vignette.smoothness.Override(.5f);
        m_vignette.roundness.Override(1);
        m_vignette.rounded.Override(true);
    }
}
