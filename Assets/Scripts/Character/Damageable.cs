using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private HealthConfigSO _healthConfigSO;
    [SerializeField] private VoidEventChannelSO _onDieEvnet;
    [SerializeField] private IntEventChannelSO _inflictDamageEvent;
    [SerializeField] private IntEventChannelSO _inflictHealingEvent;
    private int _currentHealth = default;
    public bool IsDead { get; set; }
    public bool GetHit { get; set; }
    public int CurrentHealth => _currentHealth;


    void Start()
    {
        _currentHealth = _healthConfigSO.MaxHealth;
        //OnDieEvnet.OnEventRaised += ResetHealth;
        _inflictDamageEvent.OnEventRaised += ReceiveAttack;
        _inflictHealingEvent.OnEventRaised += ReceiveHealing;
    }

    private void ReceiveAttack(int damage)
    {
        if (IsDead)
            return;

        _currentHealth -= damage;
        GetHit = true;
        if (_currentHealth <= 0)
        {
            IsDead = true;
            _onDieEvnet.RaiseEvent();
        }
    }

    private void ReceiveHealing(int healing)
    {
        _currentHealth = _currentHealth + healing > _healthConfigSO.MaxHealth ?
            _healthConfigSO.MaxHealth : _currentHealth + healing;
        IsDead = false;
    }
}
