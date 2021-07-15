using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireItem : Item, IAnimator, IAggressive
{
    [SerializeField] private IntEventChannelSO _inflictDamageEvent;
    [SerializeField] private AttackConfigSO _attackConfigSO;
    [SerializeField] private GameItemState[] _animatedStates;

    private Animator _animator;
    void Start()
    {
        if (_animatedStates.Length != 0)
            _animator = _curSprite.GetComponent<Animator>();

        _itemStateEvent.OnEventRaised += PlayAnimator;
    }
    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.Fire;
        _itemStatus.ItemState = GameItemState.StateOne;
    }

    public override void ChangeSprite(float duration = 0.2F)
    {
        return;
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
        if (CanAttack(_itemStatus.ItemState, damageable))
        {
            //damageable.ReceiveAnAttack(_attackConfigSO.AttackStrength);
            _inflictDamageEvent.RaiseEvent(_attackConfigSO.AttackStrength);

            damageable.ResetGetHit(_attackConfigSO.AttackReloadDuration);
        }
    }

    public bool CanAttack(GameItemState state, Damageable damageable)
    {
        return state == GameItemState.StateOne && !damageable.GetHit && !damageable.IsDead;
    }
}
