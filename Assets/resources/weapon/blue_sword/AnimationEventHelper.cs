using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent onAttackPerformed, onAnimationEnd;

    public void triggerAttack()
    {
        onAttackPerformed?.Invoke();
    }
    
    public void animationEnd()
    {
        onAnimationEnd?.Invoke();
    }
}
