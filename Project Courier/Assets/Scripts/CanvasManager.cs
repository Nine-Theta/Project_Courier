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
    private int _dialogueIndex = 0;

    private string[] _dialogue;

    private void Awake()
    {
        {
            _dialogueBox.SetActive(false);
            _eventSystem.gameObject.SetActive(false);
        }
    }

    public void InitializeDialogue(string[] pDialogue)
    {
        _dialogueBox.SetActive(true);
        _eventSystem.gameObject.SetActive(true);

        _dialogue = pDialogue;
        DisplayText(pDialogue[0]);
    }

    private void DisplayText(string pSentence)
    {
        _dialogueText.text = pSentence;
    }

    public void ProgressDialogue()
    {
        if (_dialogueIndex >= _dialogue.Length-1)
        {
            CloseDialogue();
        }
        else
        {
            _dialogueIndex++;
            DisplayText(_dialogue[_dialogueIndex]);
        }      
    }

    public void CloseDialogue()
    {
        _dialogueBox.SetActive(false);
        _eventSystem.gameObject.SetActive(false);

        _dialogueIndex = 0;
        _dialogue = null;
    }
}
