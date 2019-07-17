using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{
    [SerializeField] PlayAudio playAudio;
    [SerializeField] AudioSource sound;
    BoxCollider colliderBox;
    void Start() {
        colliderBox=GetComponent<BoxCollider>();
    }
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Character")
        {
            playAudio.PlaySound(sound);
            colliderBox.enabled=false;
        }         
    }
}
