using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animation playerAnimation;

    void Start()
    {
        playerAnimation=GetComponent<Animation>();
    }
    public void PlayAnimationPlayer(string name)
    {
        playerAnimation.Play(name);
    }
}
