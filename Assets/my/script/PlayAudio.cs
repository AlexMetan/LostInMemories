using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public void PlaySound(AudioSource audioSource){
        if(!audioSource.isPlaying) audioSource.Play();
    }
}
