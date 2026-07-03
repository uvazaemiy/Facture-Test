using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class ReadyToStartState : IAsyncState
{
    public ReadyToStartState()
    {
        Debug.Log("ReadyToStartState created.");
    }

    public async UniTask Enter(CancellationToken cancellationToken)
    {
        Debug.Log("Entering ReadyToStartState...");
        await UniTask.Delay(500, cancellationToken: cancellationToken);
        Debug.Log("ReadyToStartState entered.");
    }

    public async UniTask Exit(CancellationToken cancellationToken)
    {
        Debug.Log("Exiting ReadyToStartState...");
        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        Debug.Log("ReadyToStartState exited.");
    }
}