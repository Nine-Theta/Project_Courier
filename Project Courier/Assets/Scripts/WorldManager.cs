using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GlobalQuestManager))]
public class WorldManager : MonoBehaviour
{
    private static WorldManager _instance = null;

    private GlobalQuestManager _questManager;

    private GlobalUIHandler _uiHandler;


    public static WorldManager Instance { get { return _instance; } }

    public GlobalQuestManager QuestManager { get { return _questManager; } }

    public GlobalUIHandler UIHandler { get { return _uiHandler; } }

    private void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;

        _questManager = GetComponent<GlobalQuestManager>();
        _uiHandler = GetComponent<GlobalUIHandler>();
    }
}
