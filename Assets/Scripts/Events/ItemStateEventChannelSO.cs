using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Object/Event/ItemState Event Channel")]
public class ItemStateEventChannelSO : EventChannelBaseSO
{
    public UnityAction<GameItemState> OnEventRaised;
    public void RaiseEvent(GameItemState state)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state);
    }
}
