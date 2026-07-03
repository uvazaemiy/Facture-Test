using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class BootstrapState : IAsyncState
{
    public BootstrapState()
    {
        Debug.Log("BootstrapState created.");
    }

    public async UniTask Enter(CancellationToken cancellationToken)
    {
        Debug.Log("Entering BootstrapState...");



        Debug.Log("BootstrapState entered.");
    }

    public async UniTask Exit(CancellationToken cancellationToken)
    {
        Debug.Log("Exiting BootstrapState...");
        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        Debug.Log("BootstrapState exited.");
    }
}