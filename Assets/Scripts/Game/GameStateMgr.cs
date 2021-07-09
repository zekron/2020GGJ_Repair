using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMgr : MonoBehaviour
{
    [SerializeField] private GameStateEventChannelSO _gameStateEvent;

    private eGameState _gameState = eGameState.eNull;
    public eGameState GameState { get => _gameState; }

    public void SetGameState(eGameState state)
    {
        _gameState = state;
        _gameStateEvent.RaiseEvent(_gameState);
    }
}
