using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NpcScript : InteractableBase
{
    [SerializeField]
    private string _npcName = "Dummy";

    private GlobalQuestManager _questManager;
    private GlobalUIHandler _uiHandler;

    public string NPCName { get { return _npcName; } }

    private Dictionary<QuestIDs, List<ScriptableDialogue>> _supportedQuests = new Dictionary<QuestIDs, List<ScriptableDialogue>>();

    public UnityEvent<NpcScript> OnInteractionStart;

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
        if(_questManager == null) { _questManager = WorldManager.Instance.QuestManager; }
        if(_uiHandler == null) { _uiHandler = WorldManager.Instance.UIHandler; }
    }

    public override void StartInteraction()
    {
        OnInteractionStart.Invoke(this);

        Debug.Log("Interaction Started");

        QuestIDs[] ids = _supportedQuests.Keys.ToArray();

        ScriptableDialogue outputText = RandomDialogues[Random.Range(0, RandomDialogues.Count)];

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

        DisplayDialogue(outputText.Dialogue);
    }



    private void DisplayDialogue(string[] pDialogue)
    {
        foreach(string s in pDialogue)
        {
            Debug.Log(_npcName + ": " + s);
        }

        _uiHandler.StartDialogue(pDialogue);
    }

    public override void EndInteraction()
    {
    }

    public List<ScriptableDialogue> RandomDialogues;

    public List<ScriptableDialogue> QuestDialogues;

    public List<ScriptableDialogue> PostQuestCompletionDialogues;

    //TODO: dialogue that gets added to the random pool after quests get completed.


}
