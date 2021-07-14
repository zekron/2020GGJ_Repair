using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousPoolItem : Item, IAnimator, IAggressive
{
    [SerializeField] private GameItemState[] _animatedStates;
    [SerializeField] private IntEventChannelSO _inflictDamageEvent;
    [SerializeField] private AttackConfigSO _attackConfigSO;

    private Animator _animator;

    public AttackConfigSO AttackConfig => _attackConfigSO;
    void Start()
    {
        if (_animatedStates.Length != 0)
            _animator = _curSprite.GetComponent<Animator>();

        _itemStateEvent.OnEventRaised += PlayAnimator;
    }
    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.PoisonousPool;
        _itemStatus.ItemState = GameItemState.StateFour;
    }

    public void PlayAnimator(GameItemState state)
    {
        if (_animatedStates.Length == 0) return;

        for (int i = 0; i < _animatedStates.Length; i++)
        {
            if (_animatedStates[i] == _itemStatus.ItemState)
            {
                _animator.enabled = true;
                _animator.SetInteger("State", (int)_itemStatus.ItemState);
                return;
            }
            _animator.enabled = false;
        }

        _animator.SetInteger("State", (int)_itemStatus.ItemState);
        return;
    }

    public void Attack(Damageable damageable)
    {
        //TODO
        Debug.Log(damageable.GetHit);
        if (CanAttack(_itemStatus.ItemState, damageable))
        {
            //damageable.ReceiveAnAttack(_attackConfigSO.AttackStrength);
            _inflictDamageEvent.RaiseEvent(_attackConfigSO.AttackStrength);

            damageable.ResetGetHit(_attackConfigSO.AttackReloadDuration);
        }
    }

    public bool CanAttack(GameItemState state, Damageable damageable)
    {
        return state == GameItemState.StateOne && !damageable.GetHit;
    }
}
