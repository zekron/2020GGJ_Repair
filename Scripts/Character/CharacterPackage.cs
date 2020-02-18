using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterPackage : MonoBehaviour
{
    public static CharacterPackage instance = null;
    public PackageItem[] _ItemImages;

    public SpriteRenderer _HoldingItemSprite;
    public PackageItem _HoldingItem;
    public GameObject _Hands;

    public Sprite[] _TulipSprites;
    public Sprite[] _RoseSprites;
    public Sprite[] _TreeSprites;
    public Sprite[] _TombSprites;
    public Sprite[] _CandleSprites;

    public Sprite[][] _ItemSprite;

    void Start()
    {
        instance = this;
        _ItemSprite = new Sprite[5][] { _TulipSprites, _RoseSprites, _TreeSprites, _TombSprites, _CandleSprites };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveItem(ItemDetector detector, GameObject item)
    {
        for (int i = 0; i < _ItemImages.Length; i++)
        {
            if (!_ItemImages[i]._IsEmpty) continue;

            _ItemImages[i].SetPackageItemSprite(detector);
            break;
        }
    }

    public void HoldItem(PackageItem item)
    {
        _HoldingItem = item;
        if (_HoldingItem)
        {
            _HoldingItemSprite.sprite = item._PackageItemImage.sprite;
        }
        else
        {
            _HoldingItemSprite.sprite = null;
        }
        _HoldingItemSprite.DOComplete();
        _HoldingItemSprite.DOFade(_HoldingItem ? 1 : 0, 1).OnComplete(() => _Hands.SetActive(_HoldingItem));
    }

    public void UseItem()
    {
        _HoldingItemSprite.sprite = null;
        _HoldingItem = null;

        _HoldingItemSprite.DOComplete();
        _HoldingItemSprite.DOFade(_HoldingItem ? 1 : 0, 1).OnComplete(() => _Hands.SetActive(_HoldingItem));

        for (int i = 0; i < _ItemImages.Length; i++)
        {
            if (_ItemImages[i]._IsHolding)
            {
                _ItemImages[i]._IsHolding = false;
                _ItemImages[i]._IsEmpty = true;
            }
        }
    }
}
