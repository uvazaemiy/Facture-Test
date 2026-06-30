using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class GameStateMachine
{
    private IAsyncState _currentState;

    public GameStateMachine()
    {
        Debug.Log("GameStateMachine initialized.");
    }

    public async UniTask ChangeStateAsync(IAsyncState newState, CancellationToken cancellationToken)
    {
        if (_currentState != null)
        {
            await _currentState.Exit(cancellationToken);
        }
        _currentState = newState;
        await _currentState.Enter(cancellationToken);
        Debug.Log($"Changed state to: {newState.GetType().Name}");
    }
}