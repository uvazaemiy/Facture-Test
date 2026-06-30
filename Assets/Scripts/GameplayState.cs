using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class GameplayState : IAsyncState
{
    public GameplayState()
    {
        Debug.Log("GameplayState created.");
    }

    public async UniTask Enter(CancellationToken cancellationToken)
    {
        Debug.Log("Entering GameplayState...");
        
        
        
        Debug.Log("GameplayState entered.");
    }

    public async UniTask Exit(CancellationToken cancellationToken)
    {
        Debug.Log("Exiting GameplayState...");
        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        Debug.Log("GameplayState exited.");
    }
}