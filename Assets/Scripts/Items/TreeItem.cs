public class TreeItem : Item, IFetched
{
    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.Tree;
        _itemStatus.ItemState = GameItemState.StateFour;
    }

    public void Fetch()
    {
        CharacterAbilities.instance.FetchItemObject(this);
    }

    public ItemStatus GetFetchedItemStatus()
    {
        return _itemStatus;
    }
}
