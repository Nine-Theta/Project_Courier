using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUIHandler : MonoBehaviour
{
    [SerializeField]
    private CanvasManager _canvas;

    public void StartDialogue(string[] pDialogue)
    {
        _canvas.InitializeDialogue(pDialogue);
    }
}
