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
    private bool _startAvailable = false;
    [SerializeField]
    private bool _isAvailable = false;

    [SerializeField]
    private bool _isRepeatable = false;

    [SerializeField, Tooltip("Determines when the dialogue becomes available to be displayed")]
    private FlagTriggerBase _availabilityTrigger;

    [SerializeField, Tooltip("Determines when the dialogue will no longer be available to be displayed")]
    private FlagTriggerBase _expirationTrigger;

    [SerializeField]
    private string[] _dialogue;

    public UnityEvent OnDialogueComplete;

    public string[] Dialogue { get { return _dialogue; } }

    public bool IsAvailable { get { return _isAvailable; } }
    public bool IsRepeatable { get { return _isRepeatable; } }

    [HideInInspector]
    public UnityEvent<ScriptableDialogue> OnAvailabilityChanged;


    private void OnEnable()
    {
        _isAvailable = _startAvailable;

        if (OnDialogueComplete == null) OnDialogueComplete = new UnityEvent();

        OnDialogueComplete.AddListener(this.OnFlagTriggered.Invoke);

        if(!_isRepeatable)
            OnDialogueComplete.AddListener(DisableOnComplete);

        if (OnAvailabilityChanged == null) OnAvailabilityChanged = new UnityEvent<ScriptableDialogue>();

        if (_availabilityTrigger != null)
            _availabilityTrigger.OnFlagTriggered.AddListener(SetAvailable);

        if (_expirationTrigger != null)
            _expirationTrigger.OnFlagTriggered.AddListener(SetUnavailable);
    }

    private void OnDisable()
    {
        if (_availabilityTrigger != null)
            _availabilityTrigger.OnFlagTriggered.RemoveListener(SetAvailable);

        if (_expirationTrigger != null)
            _expirationTrigger.OnFlagTriggered.RemoveListener(SetUnavailable);
    }

    private void DisableOnComplete()
    {
        OnDialogueComplete.RemoveAllListeners();
    }

    private void SetAvailable()
    {
        if (_isAvailable) return;
        _isAvailable = true;

        OnAvailabilityChanged.Invoke(this);

        Debug.Log(this + ": setavailability true");
    }

    private void SetUnavailable()
    {
        if(!_isAvailable) return;
        _isAvailable = false;

        OnAvailabilityChanged.Invoke(this);

        Debug.Log(this + ": setavailability false");
    }
}
