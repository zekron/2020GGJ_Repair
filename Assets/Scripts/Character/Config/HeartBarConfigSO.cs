using UnityEngine;

[CreateAssetMenu(fileName = "HeartBarConfig", menuName = "Entity Config/HeartBar Config")]
public class HeartBarConfigSO : ScriptableObject
{
    private const int HEART_EMPTY = 0;
    private const int HEART_FULL = 1;

    [Tooltip("Initial heart sprites")]
    [SerializeField] private Sprite[] _healthBarSprites;
    public Sprite[] HealthBarSprites => _healthBarSprites;

    public int GetHeartIndex(bool isFull)
    {
        return isFull ? HEART_FULL : HEART_EMPTY;
    }
}
