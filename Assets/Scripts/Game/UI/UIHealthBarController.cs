using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarController : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO _inflictDamageEvent;
    [SerializeField] private IntEventChannelSO _inflictHealingEvent;
    [SerializeField] private UIHeart[] _heartArray;

    private int _currentHealth = default;

    private void OnEnable()
    {
        _inflictDamageEvent.OnEventRaised += ReceiveDamage;
        _inflictHealingEvent.OnEventRaised += ReceiveHealing;
    }
    private void OnDisable()
    {
        _inflictDamageEvent.OnEventRaised -= ReceiveDamage;
        _inflictHealingEvent.OnEventRaised -= ReceiveHealing;
    }
    private void ReceiveDamage(int index)
    {
        if (index == 0 || _currentHealth - index < 0) return;

        int tempHealth = _currentHealth;
        _currentHealth -= index;

        SetHealthBar(tempHealth, _currentHealth);
    }
    private void ReceiveHealing(int index)
    {
        if (index == 0 || _currentHealth + index > 3) return;

        int tempHealth = _currentHealth;
        _currentHealth += index;

        SetHealthBar(tempHealth, _currentHealth);
    }
    private void SetHealthBar(int previous, int current)
    {
        if (current > previous)
        {
            for (int i = previous; i < current; i++)
            {
                _heartArray[i].SetSprite();
            }
        }
        else
        {
            for (int i = previous; i > current; i--)
            {
                _heartArray[i - 1].SetSprite();
            }
        }
    }
}
