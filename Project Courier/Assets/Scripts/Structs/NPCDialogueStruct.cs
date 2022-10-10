using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NpcDialogue
{
    public readonly string NpcName;
    public readonly string[] Dialogue;
    public readonly Color NameColor;
    public readonly Color DialogueColor;

    public NpcDialogue(string pNpcName, string[] pDialogue, Color pNameColor, Color pDialogueColor)
    {
        NpcName = pNpcName;
        Dialogue = pDialogue;
        NameColor = pNameColor;
        DialogueColor = pDialogueColor;
    }
}
