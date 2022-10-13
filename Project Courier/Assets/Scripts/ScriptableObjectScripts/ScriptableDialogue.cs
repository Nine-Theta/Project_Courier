using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueScriptable", menuName = "ScriptableObjects/Dialogue")]
public class ScriptableDialogue : ScriptableObject, IComparable<ScriptableDialogue>
{
    [SerializeField]
    private QuestIDs _id = QuestIDs.Default;

    [SerializeField]
    private ScriptableQuestStage _questStage;

    [SerializeField]
    private string[] _dialogue;

    public QuestIDs ID { get { return _id; } }
    public ScriptableQuestStage QuestStage { get { return _questStage; } }
    public string[] Dialogue { get { return _dialogue; } }

    public UnityEvent OnDialogueComplete;

    private void OnEnable()
    {
        if (OnDialogueComplete == null) OnDialogueComplete = new UnityEvent();
    }

    public int CompareTo(ScriptableDialogue pDialogue)
    {
        //TODO: check if sorting works correctly
        return _questStage.StageNumber.CompareTo(pDialogue.QuestStage.StageNumber);
    }


}
