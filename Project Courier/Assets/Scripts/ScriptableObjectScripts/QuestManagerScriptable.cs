using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum QuestIDs { Default, MQ_1 }

[CreateAssetMenu(fileName = "QuestManager", menuName = "ScriptableObjects/Managers/QuestManagerScriptable")]
public class QuestManagerScriptable : ScriptableObject
{
    private static Dictionary<QuestIDs, ScriptableQuest> _questsBacklog;
    private static Dictionary<QuestIDs, ScriptableQuest> _questsActive;
    private static Dictionary<QuestIDs, ScriptableQuest> _questsComplete;

    public Dictionary<QuestIDs, ScriptableQuest> QuestsBacklog { get { return _questsBacklog; } }
    public Dictionary<QuestIDs, ScriptableQuest> QuestsActive { get { return _questsActive; } }
    public Dictionary<QuestIDs, ScriptableQuest> QuestsComplete { get { return _questsComplete; } }

    [HideInInspector]
    public UnityEvent<QuestIDs> QuestActivated;
    [HideInInspector]
    public UnityEvent<QuestIDs> QuestCompleted;

    private void OnEnable()
    {
        if (_questsBacklog == null)
        {
            //TODO: Save & Load Quest progress to file
            Init();
        }
    }

    private void Init()
    {
        _questsBacklog = new Dictionary<QuestIDs, ScriptableQuest>();
        _questsActive = new Dictionary<QuestIDs, ScriptableQuest>();
        _questsComplete = new Dictionary<QuestIDs, ScriptableQuest>();

        if(QuestActivated == null) QuestActivated = new UnityEvent<QuestIDs>();
        if(QuestCompleted == null) QuestCompleted = new UnityEvent<QuestIDs>();

        for (int i = 0; i < Quests.Count; i++)
        {
            switch (Quests[i].CurrentStage)
            {
                case 0:
                    _questsBacklog.Add(Quests[i].ID, Quests[i]);
                    break;
                case 255:
                    _questsComplete.Add(Quests[i].ID, Quests[i]);
                    break;
                default:
                    _questsActive.Add(Quests[i].ID, Quests[i]);
                    break;
            }

            Quests[i].OnActive.AddListener(HandleQuestActivated(Quests[i].ID));
            Quests[i].OnComplete.AddListener(HandleQuestCompleted(Quests[i].ID));
        }
    }

    public UnityAction HandleQuestActivated(QuestIDs pID)
    {
        if (!SetQuestActive(pID))
        {
            Debug.LogError("Something Went wrong activating the quest");
        }
        else
        {
            QuestActivated.Invoke(pID);
        }

        return null;
    }

    public UnityAction HandleQuestCompleted(QuestIDs pID)
    {
        if (!SetQuestComplete(pID))
        {
            Debug.LogError("Something Went wrong completing the quest");
        }
        else
        {
            QuestCompleted.Invoke(pID);
        }

        return null;
    }

    public bool SetQuestActive(QuestIDs pID)
    {
        if (_questsBacklog.TryGetValue(pID, out ScriptableQuest quest))
        {
            _questsBacklog.Remove(pID);
            return _questsActive.TryAdd(pID, quest);
        }

        Debug.LogError("Quest not in Backlog dictionary");
        return false;
    }

    public bool SetQuestComplete(QuestIDs pID)
    {
        if (_questsActive.TryGetValue(pID, out ScriptableQuest quest))
        {
            _questsActive.Remove(pID);
            return _questsComplete.TryAdd(pID, quest);
        }

        Debug.LogError("Quest not in Active dictionary");
        return false;
    }

    public List<ScriptableQuest> Quests;
}
