using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMgr : MonoBehaviour
{
    [SerializeField] private GameStateEventChannelSO _gameStateEvent;

    private GameState _gameState = GameState.Null;
    public GameState GameState { get => _gameState; }

    public void SetGameState(GameState state)
    {
        _gameState = state;
        _gameStateEvent.RaiseEvent(_gameState);
    }
}
