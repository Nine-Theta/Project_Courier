using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StageScriptable", menuName = "ScriptableObjects/QuestStage")]
public class ScriptableQuestStage : ScriptableObject
{
    [SerializeField]
    private string _stageName = "";

    [SerializeField]
    private bool _optional = false;

    [SerializeField, Multiline]
    private string _stageDescription;
    [SerializeField]
    private bool _completed = false;

    [SerializeField]
    private ScriptableQuestStage _previousStage = null;

    [SerializeField]
    private byte _stageNumber = 0;

    public bool Completed { get { return _completed; } }

    public bool Optional { get { return _optional; } }

    public byte StageNumber { get { return _stageNumber; } }

    public UnityEvent<ScriptableQuestStage> OnStageCompleted;

    private void OnEnable()
    {
        //Reset Values to default

        _completed = false;

        Debug.LogWarning(this.name + ": enabl");

        if (OnStageCompleted == null) OnStageCompleted = new UnityEvent<ScriptableQuestStage>();
    }

    private void OnDisable()
    {
        //Reset Values to default

        _completed = false;

        OnStageCompleted.RemoveAllListeners();
    }

    public void Init(ScriptableQuestStage pPreviousStage, byte pStageNumber)
    {
        _previousStage = pPreviousStage;
        _stageNumber = pStageNumber;
    }

    public void CompleteStage()
    {
        Debug.Log("complete stage called");

        try
        {

            if (_completed == false && (_previousStage == null || _previousStage.Completed || _previousStage.Optional))
            {
                _completed = true;
                OnStageCompleted.Invoke(this);
            }
        }
        catch(Exception e)
        {
            Debug.LogError(this.name + ": yeeted: " + e);
        }
    }
}
