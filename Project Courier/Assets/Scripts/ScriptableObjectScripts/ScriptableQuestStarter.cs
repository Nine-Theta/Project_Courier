using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "QuestStarterScriptable", menuName = "ScriptableObjects/QuestStarter")]
public class ScriptableQuestStarter : FlagTriggerBase
{
    [SerializeField]
    private ScriptableQuest _questToStart;
    [SerializeField]
    private ScriptableDialogue _startingDialogue;

    [SerializeField]
    private bool _canBeStarted = false;


    public ScriptableQuest QuestToStart { get { return _questToStart; } }
    public ScriptableDialogue StartingDialogue { get { return _startingDialogue; } }

    public UnityEvent<ScriptableQuestStarter> OnQuestStarted;

    public bool CanStart { get { return _canBeStarted; } set { _canBeStarted = value; } }

    private void OnEnable()
    {
        if (OnQuestStarted == null) OnQuestStarted = new UnityEvent<ScriptableQuestStarter>();
    }

    private void OnDisable()
    {
        OnQuestStarted.RemoveAllListeners();
    }

}
