using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScriptQuest", menuName = "ScriptableObjects/ScriptableQuest")]
public class ScriptableQuest : ScriptableObject
{
    [SerializeField]
    private QuestIDs _id;

    [SerializeField]
    private string _questName = "Lorem Ipsum";

    [SerializeField]
    private float _moneyReward = 1;

    [SerializeField]
    private byte _currentStage = 0;

    public ScriptableQuestStage[] Stages;

    public UnityEvent OnActive;
    public UnityEvent OnComplete;

    public QuestIDs ID { get { return _id; } }

    public string QuestName { get { return _questName; } }

    public float MoneyReward { get { return _moneyReward; } }

    public byte CurrentStage { get { return _currentStage; } }

    public bool IsActive { get { return (_currentStage > 0 && _currentStage < 255); } }

    private void OnEnable()
    {
        //Reset Values to default

        _currentStage = 0;

        if(OnActive == null) OnActive = new UnityEvent();
        if(OnComplete == null) OnComplete = new UnityEvent();

        for (int i = Stages.Length-1; i> 0; i--)
        {
            Stages[i].Init(Stages[i - 1], (byte)i);
            Stages[i].OnStageCompleted.AddListener(HandleStageComplete);
        }
        Stages[0].OnStageCompleted.AddListener(HandleStageComplete);
    }

    private void OnDisable()
    {
        //Reset Values to default

        _currentStage = 0;

        OnActive.RemoveAllListeners();
        OnComplete.RemoveAllListeners();
    }

    private void HandleStageComplete(ScriptableQuestStage pStage)
    {
        Debug.LogError(OnActive.);

        _currentStage = (byte)(pStage.StageNumber+1);

        try
        {
            if (pStage.StageNumber == 0) OnActive.Invoke();
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e);
        }

        if (pStage.StageNumber == Stages.Length)
        {
            _currentStage = 255;
            OnComplete.Invoke();
        }
    }
}
