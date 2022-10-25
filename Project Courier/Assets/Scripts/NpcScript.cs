using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NpcScript : InteractableBase
{
    [SerializeField]
    private string _npcName = "Dummy";

    //[SerializeField]
    //private QuestManagerScriptable _questManager;

    [SerializeField]
    private UIManagerScriptable _uiHandler;

    [SerializeField]
    private Color _npcColor = Color.magenta;
    [SerializeField]
    private Color _npcSpeechColor = Color.magenta;

    private PriorityPoolManager<ScriptableDialogue> _availableDialogues = new PriorityPoolManager<ScriptableDialogue>();

    private List<ScriptableDialogue> _inactivePool = new List<ScriptableDialogue>();

    public string NPCName { get { return _npcName; } }

    public UnityEvent<NpcScript> OnInteractionStart;

    private void Awake()
    {
        for (int i = 0; i < _dialoguePool.Count; i++)
        {

            if (_dialoguePool[i].IsAvailable)
                _availableDialogues.AddItem(_dialoguePool[i]);
            else
                _inactivePool.Add(_dialoguePool[i]);


            _dialoguePool[i].OnAvailabilityChanged.AddListener(OnDialogueFlagged);
        }

    }

    private void OnEnable()
    {
        if (OnInteractionStart == null) OnInteractionStart = new UnityEvent<NpcScript>();
    }


    public override void StartInteraction()
    {
        OnInteractionStart.Invoke(this);

        Debug.Log("Interaction Started");

        ScriptableDialogue output = _availableDialogues.GetHighestPriority();

        //Not a great solution, can't think of a better way rn
        if (!output.IsRepeatable) _availableDialogues.TryRemoveItem(output);

        DisplayDialogue(output.Dialogue);
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

    private void OnDialogueFlagged(ScriptableDialogue pDialogue)
    {
        if (pDialogue.IsAvailable)
        {
            _availableDialogues.AddItem(pDialogue);
            _inactivePool.Remove(pDialogue);
        }
        else
        {
            _inactivePool.Add(pDialogue);
            _availableDialogues.TryRemoveItem(pDialogue);
        }
    }


    //[SerializeField] private List<ScriptableQuestStarter> _questStarters;

    [Header("Dialogues")]

    [SerializeField] private List<ScriptableDialogue> _dialoguePool;
}
