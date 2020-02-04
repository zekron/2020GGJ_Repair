using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageItem : MonoBehaviour
{
    public ItemState _PackageItemState;
    public ItemType _PackageItemType;
    public bool _IsHolding = false;
    public bool _IsEmpty = true;

    public SpriteRenderer _PackageItemSprite;

    private void Start()
    {
        _PackageItemSprite = GetComponent<SpriteRenderer>();
    }
    public void SetPackageItemSprite(ItemDetector detector)
    {
        _PackageItemState = detector._CurItemState;
        _PackageItemType = detector._ItemType;
        _PackageItemSprite.sprite = CharacterPackage.instance
            ._ItemSprite[(int)_PackageItemType][(int)_PackageItemState];
        _PackageItemSprite.DOFade(1, 1)
                .OnComplete(() => CharacterAbilities.instance._HoldInHand = false);
        _IsEmpty = false;
    }

    public void HoldPackageItem()
    {
        CharacterPackage.instance.HoldItem(_IsHolding ? this : null);
        _PackageItemSprite.DOComplete();
        _PackageItemSprite.DOFade(_IsHolding ? 0 : 1, 1);
    }

    void OnMouseDown()
    {
        if (_IsEmpty) return;
        if (CharacterPackage.instance._HoldingItem && !_IsHolding) return;

        _IsHolding = !_IsHolding;
        HoldPackageItem();
    }
}
