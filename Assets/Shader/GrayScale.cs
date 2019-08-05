using UnityEngine;


public class GrayScale : MonoBehaviour {
    [SerializeField] Material mat;
    [SerializeField] float power;
    void Start() 
    {
        mat.SetFloat( "_Power", power );
    }

    void OnRenderImage( RenderTexture source, RenderTexture destination ) 
    {
        Graphics.Blit( source, destination, mat );
    }
}