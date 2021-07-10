/// <summary>
/// Any item can be fetched
/// </summary>
public interface IFetched
{
    public void Fetch();
    public ItemStatus GetFetchedItemStatus();
}