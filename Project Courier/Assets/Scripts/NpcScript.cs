using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NpcScript : InteractableBase
{
    [SerializeField]
    private string _npcName = "Dummy";

    [SerializeField]
    private QuestManagerScriptable _questManager;

    [SerializeField]
    private UIManagerScriptable _uiHandler;

    [SerializeField]
    private Color _npcColor = Color.magenta;
    [SerializeField]
    private Color _npcSpeechColor = Color.magenta;

    private Dictionary<QuestIDs, List<ScriptableDialogue>> _supportedQuests = new Dictionary<QuestIDs, List<ScriptableDialogue>>();

    public string NPCName { get { return _npcName; } }

    public UnityEvent<NpcScript> OnInteractionStart;

    private void Awake()
    {
        for (int i = 0; i < _questDialogues.Count; i++)
        {
            if (!_supportedQuests.ContainsKey(_questDialogues[i].ID))
            {
                _supportedQuests.Add(_questDialogues[i].ID, new List<ScriptableDialogue>());
            }
            _supportedQuests[_questDialogues[i].ID].Add(_questDialogues[i]);
            _supportedQuests[_questDialogues[i].ID].Sort();
        }
    }

    private void OnEnable()
    {
        if (OnInteractionStart == null) OnInteractionStart = new UnityEvent<NpcScript>();
    }

    private void Start()
    {

    }

    public override void StartInteraction()
    {
        OnInteractionStart.Invoke(this);

        Debug.Log("Interaction Started");

        QuestIDs[] ids = _supportedQuests.Keys.ToArray();

        ScriptableDialogue outputText = _randomDialogues[Random.Range(0, _randomDialogues.Count)];

        for(int i = 0; i < _questStarters.Count; i++)
        {
            if (_questStarters[i].CanStart)
            {

            }
        }

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

    private void RemoveStageFromList(ScriptableQuestStage pStage)
    {

    }



    private void DisplayDialogue(string[] pDialogue)
    {
        foreach (string s in pDialogue)
        {
            Debug.Log(_npcName + ": " + s);
        }

        _uiHandler.ShowDialogue(new NpcDialogue(_npcName, pDialogue, _npcColor, _npcSpeechColor));
    }

    public override void EndInteraction()
    {

    }


    [SerializeField] private List<ScriptableQuestStarter> _questStarters;

    [Header("Dialogues")]

    [SerializeField] private List<ScriptableDialogue> _randomDialogues;

    [SerializeField] private List<ScriptableDialogue> _questDialogues;

    [SerializeField] private List<ScriptableDialogue> _postQuestCompletionDialogues;

    //TODO: dialogue that gets added to the random pool after quests get completed.


}
