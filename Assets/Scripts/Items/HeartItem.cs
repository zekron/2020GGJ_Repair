using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : Item
{
    [SerializeField] private IntEventChannelSO _inflictHealingEvent;
    [SerializeField] private HealingConfigSO _healingConfigSO;
    const int INVISIBLE_LAYER = 10;
    const int ITEM_LAYER = 5;

    private void OnEnable()
    {
    }

    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.Heart;
        _itemStatus.ItemState = GameItemState.StateDefault;
    }

    public override void ChangeSprite(float duration = 0.2F)
    {
        _curSprite.DOFade(0, duration).OnComplete(() => _curSprite.gameObject.layer = INVISIBLE_LAYER);
    }

    public void Heal(Damageable damageable)
    {
        _inflictHealingEvent.RaiseEvent(_healingConfigSO.HealingStrength);
    }
}
