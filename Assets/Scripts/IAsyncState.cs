using Cysharp.Threading.Tasks;
using System.Threading;

public interface IAsyncState
{
    UniTask Enter(CancellationToken cancellationToken);
    UniTask Exit(CancellationToken cancellationToken);
}