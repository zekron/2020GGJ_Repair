using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Object/Event/GameState Event Channel")]
public class GameStateEventChannelSO : EventChannelBaseSO
{
    public UnityAction<GameState> OnEventRaised;
    public void RaiseEvent(GameState state)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state);
    }
}
