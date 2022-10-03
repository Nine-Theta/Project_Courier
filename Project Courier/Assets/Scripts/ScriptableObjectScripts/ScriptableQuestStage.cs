using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageScriptable", menuName = "ScriptableObjects/ScriptableQuestStage")]
public class ScriptableQuestStage : ScriptableObject
{
    [SerializeField]
    private string _stageName = "";

    [SerializeField]
    private bool _optional = false;

    [SerializeField, Multiline]
    private string _stageDescription;

    private bool _completed = false;

    private ScriptableQuestStage _previousStage = null;

    private byte _stageNumber = 0;

    public bool Completed { get { return _completed; } }
    
    public bool Optional { get { return _optional; } }

    public byte StageNumber { get { return _stageNumber; } }

    public void Init(ScriptableQuestStage pPreviousStage, byte pStageNumber)
    {
        _previousStage = pPreviousStage;
        _stageNumber = pStageNumber;
    }

    public void CompleteStage()
    {
        if(_previousStage == null || _previousStage.Completed || _previousStage.Optional)
        {
            _completed = true;
        }
    }

}
