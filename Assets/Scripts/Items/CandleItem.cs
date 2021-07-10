using UnityEngine;

public class CandleItem : Item, IAnimator, IFetched
{
    [SerializeField] private GameItemState[] _animatedStates;
    private Animator _animator;

    void Start()
    {
        if (_animatedStates.Length != 0)
            _animator = _curSprite.GetComponent<Animator>();

        _itemStateEvent.OnEventRaised += PlayAnimator;
    }

    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.Candle;
        _itemStatus.ItemState = GameItemState.StateFour;
    }

    public void PlayAnimator(GameItemState state)
    {
        if (_animatedStates.Length == 0) return;

        for (int i = 0; i < _animatedStates.Length; i++)
        {
            if (_animatedStates[i] == _itemStatus.ItemState)
            {
                _animator.enabled = true;
                _animator.SetInteger("State", (int)_itemStatus.ItemState);
                return;
            }
            _animator.enabled = false;
        }

        _animator.SetInteger("State", (int)_itemStatus.ItemState);
        return;
    }

    public void Fetch()
    {
        CharacterAbilities.instance.FetchItemObject(gameObject);
    }

    public ItemStatus GetFetchedItemStatus()
    {
        return _itemStatus;
    }
}
