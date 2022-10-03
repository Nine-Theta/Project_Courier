using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestIDs { Default, MQ_1 }

public class GlobalQuestManager : MonoBehaviour
{
    private static Dictionary<QuestIDs, ScriptableQuest> _questsBacklog;
    private static Dictionary<QuestIDs, ScriptableQuest> _questsActive;
    private static Dictionary<QuestIDs, ScriptableQuest> _questsComplete;

    public Dictionary<QuestIDs, ScriptableQuest> QuestsBacklog { get { return _questsBacklog; } }
    public Dictionary<QuestIDs, ScriptableQuest> QuestsActive { get { return _questsActive; } }
    public Dictionary<QuestIDs, ScriptableQuest> QuestsComplete { get { return _questsComplete; } }

    private void Awake()
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

        for(int i = 0; i < Quests.Count; i++)
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

        }
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
