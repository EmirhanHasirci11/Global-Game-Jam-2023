using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnAnimationEventTriggered;
    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }
}
