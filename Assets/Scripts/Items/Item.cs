using DG.Tweening;
using UnityEngine;

public abstract class Item : MonoBehaviour, IDetected
{
    [SerializeField] protected Sprite[] _itemSprites;
    [SerializeField] protected SpriteRenderer _curSprite;
    [SerializeField] protected SpriteRenderer _newSprite;

    [Header("Item Info")]
    [SerializeField] protected Detector _detector;
    [SerializeField] protected ItemStatus _itemStatus;

    [Header("Event Channel")]
    [SerializeField] protected ItemStateEventChannelSO _itemStateEvent;

    public Detector MyDetector { get => _detector; }

    private void Start()
    {
        _detector = GetComponent<Detector>();

        int length = _itemSprites.Length;
        if (length > 0)
        {
            _curSprite.sprite = _itemSprites[length - 1];
            _newSprite.sprite = _itemSprites[length - 2];
        }
    }

    public virtual void ChangeSprite(float duration = 0.2F)
    {
        if (_itemSprites.Length <= 0) return;

        _curSprite.DOComplete();
        _newSprite.DOComplete();

        _newSprite.sprite = _itemSprites[(int)_itemStatus.ItemState];

        //_CurSprite.sprite = _NewSprite.sprite;
        //if (state > ItemState.eStateOne)
        //    _NewSprite.sprite = _ObjectSprites[(int)state - 1];

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
                _curSprite.sprite = _itemSprites[(int)_itemStatus.ItemState];
                if (_itemStatus.ItemState > GameItemState.StateOne)
                    _newSprite.sprite = _itemSprites[(int)_itemStatus.ItemState - 1];
                _curSprite.color = StaticData.ColorFull;
            });
    }

    public void SetItemState(GameItemState state)
    {
        if (state < GameItemState.StateOne) return;

        _itemStatus.ItemState = state;
        _itemStateEvent.RaiseEvent(_itemStatus.ItemState);
    }
    public ItemStatus GetDetectedItemStatus()
    {
        return _itemStatus;
    }
}

[System.Serializable]
public struct ItemStatus
{
    [SerializeField] private GameItemType _itemType;
    [SerializeField] private GameItemState _itemState;

    public GameItemType ItemType { get => _itemType; set => _itemType = value; }
    public GameItemState ItemState { get => _itemState; set => _itemState = value; }
}
