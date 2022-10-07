using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "UIManager", menuName = "ScriptableObjects/Managers/UIManagerScriptable")]
public class UIManagerScriptable : ScriptableObject
{
    [SerializeField] private string PlayerInputMapName;
    [SerializeField] private string UIInputMapName;

    [HideInInspector] public UnityEvent<string> SwitchInputMap;
    [HideInInspector] public UnityEvent<string[]> OnStartDialogue;


    private void OnEnable()
    {
        if (SwitchInputMap == null) SwitchInputMap = new UnityEvent<string>();
        if (OnStartDialogue == null) OnStartDialogue = new UnityEvent<string[]>();
    }

    public void ShowDialogue(string[] pDialogue)
    {
        OnStartDialogue.Invoke(pDialogue);
        StartUI();
    }

    public void StartUI()
    {
        SwitchInputMap.Invoke(UIInputMapName);
    }

    public void StopUI()
    {
        SwitchInputMap.Invoke(PlayerInputMapName);
        Debug.Log("UI stopped");
    }
}
