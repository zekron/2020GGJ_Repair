using UnityEngine;

public class RoseItem : Item, IFetched, IAggressive
{
    [SerializeField] private IntEventChannelSO _inflictDamageEvent;
    [SerializeField] private AttackConfigSO _attackConfigSO;
    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.Rose;
        _itemStatus.ItemState = GameItemState.StateFour;
    }

    public void Fetch()
    {
        CharacterAbilities.instance.FetchItemObject(this);
    }

    public ItemStatus GetFetchedItemStatus()
    {
        return _itemStatus;
    }

    public void Attack(Damageable damageableTemp)
    {
        if (CanAttack(_itemStatus.ItemState, damageableTemp))
        {
            //damageable.ReceiveAnAttack(_attackConfigSO.AttackStrength);
            _inflictDamageEvent.RaiseEvent(_attackConfigSO.AttackStrength);

            damageableTemp.ResetGetHit(_attackConfigSO.AttackReloadDuration);
        }
    }

    public bool CanAttack(GameItemState state, Damageable damageableTemp)
    {
        return !damageableTemp.GetHit && !damageableTemp.IsDead;
    }
}
