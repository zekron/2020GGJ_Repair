using DG.Tweening;
using UnityEngine;

public class CharacterPackage : MonoBehaviour
{
    public static CharacterPackage instance = null;
    public PackageItem[] _ItemImages;

    public PackageItem _HoldingItem;

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
        GameMgr.Instance.GameProtagonist.SetHands(_HoldingItem ? _HoldingItem._ImagePackageItem.sprite : null);
    }

    public void UseItem()
    {
        _HoldingItem = null;

        GameMgr.Instance.GameProtagonist.SetHands(_HoldingItem ? _HoldingItem._ImagePackageItem.sprite : null);

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
