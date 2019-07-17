using UnityEngine;

public class UIscript : MonoBehaviour
{
   [SerializeField] GameObject handImage;

   public void HandActive(bool value){
       handImage.SetActive(value);
   }
}
