using UnityEngine;

public class LiftButtonMaterial : MonoBehaviour
{
    [SerializeField] Renderer[] button;
    [SerializeField] Material[] material;
   
    public void ChangeMaterial(int indexButton,int indexMaterial)
    {
        button[indexButton].material=material[indexMaterial];
    }
}
