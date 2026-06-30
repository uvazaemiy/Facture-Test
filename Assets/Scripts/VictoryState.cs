using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class VictoryState : IAsyncState
{
    public VictoryState()
    {
        Debug.Log("VictoryState created.");
    }

    public async UniTask Enter(CancellationToken cancellationToken)
    {
        Debug.Log("Entering VictoryState...");
        await UniTask.Delay(1000, cancellationToken: cancellationToken);
        Debug.Log("VictoryState entered.");
    }

    public async UniTask Exit(CancellationToken cancellationToken)
    {
        Debug.Log("Exiting VictoryState...");
        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        Debug.Log("VictoryState exited.");
    }
}