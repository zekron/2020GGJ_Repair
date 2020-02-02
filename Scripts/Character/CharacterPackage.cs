using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterPackage : MonoBehaviour
{
    public static CharacterPackage instance = null;
    public SpriteRenderer[] _ItemImages;

    public Sprite[] _TulipSprites;
    public Sprite[] _RoseSprites;
    public Sprite[] _TreeSprites;
    public Sprite[] _TombSprites;
    public Sprite[] _CandleSprites;

    private Sprite[][] m_ItemSprite;

    void Start()
    {
        instance = this;
        m_ItemSprite = new Sprite[5][] { _TulipSprites, _RoseSprites, _TreeSprites, _TombSprites, _CandleSprites };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveItem(ItemDetector detector, GameObject item)
    {
        for (int i = 0; i < _ItemImages.Length; i++)
        {
            if (_ItemImages[i].sprite != null) continue;

            _ItemImages[i].sprite = m_ItemSprite[(int)detector._ItemType][(int)detector._CurItemState];
            item.transform.DOLocalMove(_ItemImages[i].transform.localPosition, 1)
                .OnComplete(() => CharacterAbilities.instance._HoldInHand = false);
            _ItemImages[i].DOFade(1, 1)
                .OnComplete(() => Destroy(item));
            break;
        }
    }

    public void HoldItem()
    {

    }
}
