using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcScript : InteractableBase
{
    [SerializeField]
    private string _npcName = "Dummy";

    private GlobalQuestManager _questManager;

    public string NPCName { get { return _npcName; } }

    private Dictionary<QuestIDs, List<ScriptableDialogue>> _supportedQuests = new Dictionary<QuestIDs, List<ScriptableDialogue>>();

    private void Awake()
    {
        for(int i = 0; i < QuestDialogues.Count; i++)
        {
            if (!_supportedQuests.ContainsKey(QuestDialogues[i].ID))
            {
                _supportedQuests.Add(QuestDialogues[i].ID, new List<ScriptableDialogue>());
            }
            _supportedQuests[QuestDialogues[i].ID].Add(QuestDialogues[i]);
            _supportedQuests[QuestDialogues[i].ID].Sort();
        }
    }

    private void Start()
    {
        if(_questManager == null)
        {
            _questManager = WorldManager.Instance.QuestManager;
        }
    }

    public override void StartInteraction()
    {
        Debug.Log("Interaction Started");

        QuestIDs[] ids = _supportedQuests.Keys.ToArray();

        ScriptableDialogue outputText = RandomDialogues[Random.Range(0, RandomDialogues.Count - 1)];

        for (int i = 0; i < _supportedQuests.Count; i++)
        {
            if (_questManager.QuestsActive.ContainsKey(ids[i]))
            {
                if (_questManager.QuestsActive[ids[i]].CurrentStage >= _supportedQuests[ids[i]].First().QuestStage.StageNumber)
                {
                    outputText = _supportedQuests[ids[i]].First();
                    _supportedQuests[ids[i]].RemoveAt(0);
                    if (_supportedQuests[ids[i]].Count <= 0) _supportedQuests.Remove(ids[i]);
                    break;
                }
            }
        }



        Debug.Log(_npcName + ": " + outputText.Dialogue);
    }

    public override void EndInteraction()
    {
    }

    public List<ScriptableDialogue> RandomDialogues;

    public List<ScriptableDialogue> QuestDialogues;


}
