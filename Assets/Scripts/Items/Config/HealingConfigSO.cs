using UnityEngine;

[CreateAssetMenu(fileName = "HealingConfig", menuName = "Entity Config/Healing Config")]
public class HealingConfigSO : EventChannelBaseSO
{
    [SerializeField] private int _healingStrength;

    public int HealingStrength => _healingStrength;
}
