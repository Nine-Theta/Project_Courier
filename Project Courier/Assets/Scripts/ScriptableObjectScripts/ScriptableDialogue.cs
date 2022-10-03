using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueScriptable", menuName = "ScriptableObjects/ScriptableDialogue")]
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
    public int CompareTo(ScriptableDialogue pDialogue)
    {
        return _questStage.StageNumber.CompareTo(pDialogue.QuestStage.StageNumber);
    }
}
