using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Cysharp.Threading.Tasks;
using System.Threading;

public class GameDebugUI : MonoBehaviour
{
    [Inject] private GameStateMachine _gameStateMachine;
    [Inject] private BootstrapState _bootstrapState;
    [Inject] private ReadyToStartState _readyToStartState;
    [Inject] private GameplayState _gameplayState;
    [Inject] private DefeatState _defeatState;
    [Inject] private VictoryState _victoryState;

    private CancellationTokenSource _cts;

    void Start()
    {
        _cts = new CancellationTokenSource();
    }

    void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    public void ChangeToBootstrapState() => ChangeState(_bootstrapState);
    public void ChangeToReadyToStartState() => ChangeState(_readyToStartState);
    public void ChangeToGameplayState() => ChangeState(_gameplayState);
    public void ChangeToDefeatState() => ChangeState(_defeatState);
    public void ChangeToVictoryState() => ChangeState(_victoryState);

    private async void ChangeState(IAsyncState newState)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();

        try
        {
            await _gameStateMachine.ChangeStateAsync(newState, _cts.Token);
        }
        catch (System.OperationCanceledException)
        {
            Debug.Log($"State change to {newState.GetType().Name} was cancelled.");
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 30), "Bootstrap State"))
        {
            ChangeToBootstrapState();
        }
        if (GUI.Button(new Rect(10, 50, 150, 30), "ReadyToStart State"))
        {
            ChangeToReadyToStartState();
        }
        if (GUI.Button(new Rect(10, 90, 150, 30), "Gameplay State"))
        {
            ChangeToGameplayState();
        }
        if (GUI.Button(new Rect(10, 130, 150, 30), "Defeat State"))
        {
            ChangeToDefeatState();
        }
        if (GUI.Button(new Rect(10, 170, 150, 30), "Victory State"))
        {
            ChangeToVictoryState();
        }
    }
}