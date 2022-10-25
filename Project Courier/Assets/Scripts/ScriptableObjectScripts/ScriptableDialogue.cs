using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueScriptable", menuName = "ScriptableObjects/Dialogue")]
public class ScriptableDialogue : FlagTriggerBase, IPriorizable
{
    [SerializeField]
    private PriorityLevel _priority = PriorityLevel.Normal;

    public PriorityLevel Priority { get { return _priority; }  set { } }

    [SerializeField]
    private bool _isAvailable = false;

    [SerializeField, Tooltip("Determines when the dialogue becomes available to be displayed")]
    private FlagTriggerBase _availabilityTrigger;

    [SerializeField, Tooltip("Determines when the dialogue will no longer be available to be displayed")]
    private FlagTriggerBase _expirationTrigger;

    [SerializeField]
    private string[] _dialogue;

    public UnityEvent OnDialogueComplete;


    public string[] Dialogue { get { return _dialogue; } }

    public bool IsAvailable { get { return _isAvailable; } }

    [HideInInspector]
    public UnityEvent<ScriptableDialogue> OnAvailabilityChanged;


    private void OnEnable()
    {
        if (OnDialogueComplete == null) OnDialogueComplete = new UnityEvent();

        if (OnAvailabilityChanged == null) OnAvailabilityChanged = new UnityEvent<ScriptableDialogue>();

        if (_availabilityTrigger != null)
            _availabilityTrigger.OnFlagTriggered.AddListener(SetAvailability(true));

        if (_expirationTrigger != null)
            _expirationTrigger.OnFlagTriggered.AddListener(SetAvailability(false));
    }

    private void OnDisable()
    {
        if (_availabilityTrigger != null)
            _availabilityTrigger.OnFlagTriggered.RemoveListener(SetAvailability(true));

        if (_expirationTrigger != null)
            _expirationTrigger.OnFlagTriggered.RemoveListener(SetAvailability(false));
    }

    private UnityAction SetAvailability(bool pAvailability)
    {
        _isAvailable = pAvailability;

        OnAvailabilityChanged.Invoke(this);

        return null;
    }
}
