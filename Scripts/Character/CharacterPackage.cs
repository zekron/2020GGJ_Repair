using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterPackage : MonoBehaviour
{
    public Image[] _ItemImages;

    public Sprite[] _TulipSprites;
    public Sprite[] _RoseSprites;
    public Sprite[] _TreeSprites;
    public Sprite[] _TombSprites;
    public Sprite[] _CandleSprites;

    private Sprite[][] m_ItemSprite;

    void Start()
    {
        m_ItemSprite = new Sprite[5][] { _TulipSprites, _RoseSprites, _TreeSprites, _TombSprites, _CandleSprites };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveItem(ItemType item, ItemState state)
    {
        for (int i = 0; i < _ItemImages.Length; i++)
        {
            if (_ItemImages[i].sprite != null) continue;

            _ItemImages[i].sprite = m_ItemSprite[(int)item][(int)state];
            _ItemImages[i].DOColor(StaticData.ColorFadeOut, 0.5f);
            break;
        }
    }

    public void HoldItem()
    {

    }
}
