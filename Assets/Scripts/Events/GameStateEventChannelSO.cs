using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Object/Event/GameState Event Channel")]
public class GameStateEventChannelSO : EventChannelBaseSO
{
    public UnityAction<eGameState> OnEventRaised;
    public void RaiseEvent(eGameState state)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state);
    }
}
