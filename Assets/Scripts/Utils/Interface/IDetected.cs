/// <summary>
/// Can be influenced by DestroyDetector
/// </summary>
public interface IDetected
{
    public void ChangeSprite(float duration = StaticData.DestroyDuration);
    public ItemStatus GetDetectedItemStatus();
}