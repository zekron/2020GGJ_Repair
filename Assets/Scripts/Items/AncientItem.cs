using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class AncientItem : Item
{
    [SerializeField] private List<BrokenAncientSprite> _BrokenSprites;
    [SerializeField] private Sprite _FinalAncientSprite;

    private byte m_UnlockedKeycodeFlag = 0b0;
    private int m_KeyCount;

    private void Start()
    {
        m_KeyCount = GetComponent<AncientDetector>()._Locks.Count;
    }

    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.Ancient;
        _itemStatus.ItemState = GameItemState.StateFour;
    }

    public void ChangeSprite(int keyIndex, float duration = StaticData.DestroyDuration)
    {
        _curSprite.DOComplete();
        _newSprite.DOComplete();

        if (LowBitCount(m_UnlockedKeycodeFlag) == m_KeyCount)
        {
            _newSprite.sprite = _FinalAncientSprite;
        }
        else
        {
            m_UnlockedKeycodeFlag += (byte)(1 << keyIndex);

            for (int i = 0; i < _BrokenSprites.Count; i++)
            {
                if (_BrokenSprites[i]._KeyCode == m_UnlockedKeycodeFlag)
                {
                    _itemSprites = _BrokenSprites[i]._Sprites;
                }
            }
            //_newSprite.sprite = _UnlockedAncientSprites[m_KeyCount == 0 ? _UnlockedAncientSprites.Length - 1 : keyIndex];
            _newSprite.sprite = _itemSprites[(int)_itemStatus.ItemState];
        }

        _newSprite.DOFade(1, duration)
            .OnComplete(
            () =>
            {
                _newSprite.color = StaticData.ColorFadeOut;
            });
        _curSprite.DOFade(0, duration)
            .OnComplete(
            () =>
            {
                _curSprite.sprite = _newSprite.sprite;
                _curSprite.color = StaticData.ColorFull;
            });
    }

    static int LowBitCount(int lowbit)
    {
        int cnt = 0;
        while (lowbit > 0)
        {
            lowbit -= lowbit & -lowbit;
            cnt++;
        }
        return cnt;
    }
}
