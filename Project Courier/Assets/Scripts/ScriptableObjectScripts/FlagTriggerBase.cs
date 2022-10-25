using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagTriggerBase : ScriptableObject
{
    [HideInInspector]
    public UnityEvent OnFlagTriggered;

    private void OnEnable()
    {
        if (OnFlagTriggered == null) OnFlagTriggered = new UnityEvent();
    }
}
