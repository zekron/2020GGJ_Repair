using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Object/Event/Int Event Channel")]
public class IntEventChannelSO : EventChannelBaseSO
{
    public UnityAction<int> OnEventRaised;
    public void RaiseEvent(int index)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(index);
    }
}
