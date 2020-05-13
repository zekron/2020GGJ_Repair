using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageItem : MonoBehaviour
{
    public eItemState _PackageItemState;
    public eItemType _PackageItemType;
    public bool _IsHolding = false;
    public bool _IsEmpty = true;

    public SpriteRenderer _PackageItemImage;

    private void Start()
    {
    }
    public void SetPackageItemSprite(ItemDetector detector)
    {
        _PackageItemState = detector.CurItemState;
        _PackageItemType = detector._ItemType;
        _PackageItemImage.sprite = CharacterPackage.instance
            ._ItemSprite[(int)_PackageItemType][(int)_PackageItemState];
        _PackageItemImage.DOFade(1, 1)
                .OnComplete(() => CharacterAbilities.instance._HoldInHand = false);
        _IsEmpty = false;
    }

    public void HoldPackageItem()
    {
        if (_IsEmpty) return;
        if (CharacterPackage.instance._HoldingItem && !_IsHolding) return;

        _IsHolding = !_IsHolding;

        CharacterPackage.instance.HoldItem(_IsHolding ? this : null);
        _PackageItemImage.DOComplete();
        _PackageItemImage.DOFade(_IsHolding ? 0 : 1, 1);
    }

    public void SetEmpty()
    {
        _IsEmpty = true;
        _PackageItemImage.DOComplete();
        _PackageItemImage.DOFade(0, 0);
    }

    void OnMouseDown()
    {
        HoldPackageItem();
    }
}
