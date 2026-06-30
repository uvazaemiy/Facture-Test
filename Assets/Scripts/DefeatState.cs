using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class DefeatState : IAsyncState
{
    public DefeatState()
    {
        Debug.Log("DefeatState created.");
    }

    public async UniTask Enter(CancellationToken cancellationToken)
    {
        Debug.Log("Entering DefeatState...");



        Debug.Log("DefeatState entered.");
    }

    public async UniTask Exit(CancellationToken cancellationToken)
    {
        Debug.Log("Exiting DefeatState...");
        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        Debug.Log("DefeatState exited.");
    }
}