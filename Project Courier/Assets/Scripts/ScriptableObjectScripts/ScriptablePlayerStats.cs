using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptablePlayerStats : ScriptableObject
{
    [SerializeField] private float _baseHealth = 100f;
    [SerializeField] private float _baseStamina = 100f;

    [SerializeField] private float _currentHealth = 100f;
    [SerializeField] private float _currentStamina = 100f;

    [SerializeField] private float _money = 0;

    private void OnEnable()
    {
        _currentHealth = _baseHealth;
        _currentStamina = _baseStamina;
    }

    public void addMoney(float pReward)
    {
        _money += pReward;
    }
}
