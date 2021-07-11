using DG.Tweening;
using UnityEngine;

public class CharacterPackage : MonoBehaviour
{
    public static CharacterPackage instance = null;
    public PackageItem[] _ItemImages;

    public SpriteRenderer _HoldingItemSprite;
    public PackageItem _HoldingItem;
    public GameObject _Hands;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ClearItems()
    {
        for (int i = 0; i < _ItemImages.Length; i++)
        {
            _ItemImages[i].SetEmpty();
        }
    }
    public void SaveItem(IFetched fetchedItem)
    {
        for (int i = 0; i < _ItemImages.Length; i++)
        {
            if (!_ItemImages[i]._IsEmpty) continue;

            _ItemImages[i].SetPackageItemSprite(fetchedItem);
            break;
        }
    }

    public void HoldItem(PackageItem item)
    {
        _HoldingItem = item;
        _HoldingItemSprite.DOComplete();
        if (_HoldingItem)
        {
            _HoldingItemSprite.DOFade(1, 1).OnStart(() =>
            {
                _Hands.SetActive(_HoldingItem);
                _HoldingItemSprite.sprite = item._ImagePackageItem.sprite;
            });
        }
        else
        {
            _HoldingItemSprite.DOFade(0, 1).OnComplete(() =>
            {
                _Hands.SetActive(_HoldingItem);
                _HoldingItemSprite.sprite = null;
            });
        }
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
