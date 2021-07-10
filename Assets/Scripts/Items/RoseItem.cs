﻿public class RoseItem : Item, IFetched
{
    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.Rose;
        _itemStatus.ItemState = GameItemState.StateFour;
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
