using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private GameObject _dialogueBox;

    [SerializeField]
    private TextMeshProUGUI _dialogueText;
    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private UIManagerScriptable _uiHandler;

    private NpcDialogue _dialogue = new NpcDialogue("default", new string[] {"lorem", "ipsum"}, Color.magenta, Color.magenta);
    private int _dialogueIndex = 0;

    private void Awake()
    {
        {
            _dialogueBox.SetActive(false);
            _eventSystem.gameObject.SetActive(false);
            _uiHandler.OnStartDialogue.AddListener(InitializeDialogue);
        }
    }

    public void InitializeDialogue(NpcDialogue pDialogue)
    {
        _dialogueBox.SetActive(true);
        _eventSystem.gameObject.SetActive(true);

        _dialogue = pDialogue;
        DisplayText(pDialogue.NpcName, pDialogue.Dialogue[0], pDialogue.NameColor, pDialogue.DialogueColor);
    }

    private void DisplayText(string pSentence) { DisplayText(_dialogue.NpcName, pSentence, _dialogue.NameColor, _dialogue.DialogueColor); }
    private void DisplayText(string pName, string pSentence, Color pNameColor, Color pDialogueColor)
    {
        _nameText.text = pName;
        _nameText.color = pDialogueColor;

        _dialogueText.text = pSentence;
        _dialogueText.color = pDialogueColor;
    }

    public void ProgressDialogue()
    {
        if (_dialogueIndex >= _dialogue.Dialogue.Length - 1)
        {
            CloseDialogue();
        }
        else
        {
            _dialogueIndex++;
            DisplayText(_dialogue.Dialogue[_dialogueIndex]);
        }
    }

    public void CloseDialogue()
    {
        _dialogueBox.SetActive(false);
        _eventSystem.gameObject.SetActive(false);

        _dialogueIndex = 0;

        _uiHandler.StopUI();
    }
}
