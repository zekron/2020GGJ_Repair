using DG.Tweening;

public class TileItem : Item
{
    public override void ChangeSprite(float duration = 0.2F)
    {
        if (_itemSprites.Length <= 0) return;

        _curSprite.DOComplete();
        _newSprite.DOComplete();

        _newSprite.sprite = _itemSprites[(int)_itemStatus.ItemState];

        _curSprite.DOFade(0, duration)
            .OnComplete(
            () =>
            {
                _curSprite.sprite = _newSprite.sprite;
                if (_itemStatus.ItemState > GameItemState.StateOne)
                    _newSprite.sprite = _itemSprites[(int)_itemStatus.ItemState - 1];
                _curSprite.color = StaticData.ColorFull;
            });
    }
}
