
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] Material[] materials;
    Renderer objRender; 

    void Start()
    {
        objRender=GetComponent<Renderer>();
    }
    public void ChangeMat(int value)
    {
        objRender.material=materials[value];
    }
}
