using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
    public bool isStateAnimating, isAnimating;
    protected Animator LocalAnimator;

    private void Start()
    {
        LocalAnimator = GetComponent<Animator>();
    }
    
    public void SetAnimationID (int animationId)
    {
        isAnimating = true;
        LocalAnimator.SetInteger("Animation ID", animationId);
    }
    
    public void SetStateID (int stateId)
    {
        isAnimating = true;
        isStateAnimating = true;
        LocalAnimator.SetInteger("State ID", stateId);
    }
    
    public void StateComplete ()
    {
        isStateAnimating = false;
        LocalAnimator.SetInteger("State ID", LocalAnimator.GetInteger("State ID") + 1);
    }
    
    public void AnimationComplete ()
    {
        isAnimating = false;
        LocalAnimator.SetInteger("Animation ID", 0);
        LocalAnimator.SetInteger("State ID", 0);
    }
    
    public void MiniShake()
    {
        Camera.main.GetComponent<CameraControl>().Shake(.1f, 8, 6f);
    }

    protected IEnumerator unsetIntroShake()
    {
        yield return new WaitForSeconds(.4f);
        Camera.main.GetComponent<CameraControl>().setIntroShake(false);
    }
    
    public void MediumShake()
    {
        Camera.main.GetComponent<CameraControl>().setIntroShake(true);
        Camera.main.GetComponent<CameraControl>().Shake(.2f, 14, 12f);
        StartCoroutine(unsetIntroShake());
    }
    
    public void BrainShake()
    {
        Camera.main.GetComponent<CameraControl>().Shake(.2f, 36, 12f);
    }
    
    public void Shake(AnimationEvent shake)
    {
        Camera.main.GetComponent<CameraControl>().Shake(shake.floatParameter, shake.intParameter, 260f);
    }
    
}
