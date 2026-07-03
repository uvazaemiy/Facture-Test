        using UnityEngine;
        using VContainer;
        using Cysharp.Threading.Tasks;
        using System;
        using System.Threading;

        public class RageController : MonoBehaviour
        {
            private GameConfig _gameConfig;

            private float _currentRage;
            private bool _isRageActive;
            private CancellationTokenSource _rageCancellationTokenSource;

            public event Action<float> OnRageChanged;
            public event Action OnRageActivated;
            public event Action OnRageDeactivated;

            public bool IsRageActive => _isRageActive;
            public float CurrentRage => _currentRage;

            [Inject]
            public void Construct(GameConfig gameConfig)
            {
                _gameConfig = gameConfig;
            }

            void Start()
            {
                _currentRage = 0;
                _isRageActive = false;
                _rageCancellationTokenSource = new CancellationTokenSource();
                StartRageDecay();
            }

            void OnDestroy()
            {
                _rageCancellationTokenSource?.Cancel();
                _rageCancellationTokenSource?.Dispose();
            }

            public void AddRage(float amount)
            {
                if (_isRageActive) return;

                _currentRage = Mathf.Min(_currentRage + amount, 100f);
                OnRageChanged?.Invoke(_currentRage);

                _rageCancellationTokenSource?.Cancel();
                _rageCancellationTokenSource?.Dispose();
                _rageCancellationTokenSource = new CancellationTokenSource();
                StartRageDecay();

                if (_currentRage >= 100f && !_isRageActive)
                {
                    ActivateRageModeAsync().Forget();
                }
            }

            private async UniTaskVoid ActivateRageModeAsync()
            {
                _isRageActive = true;
                OnRageActivated?.Invoke();
                Debug.Log("Rage Mode Activated!");

                try
                {
                    _rageCancellationTokenSource?.Cancel();
                    _rageCancellationTokenSource?.Dispose();
                    _rageCancellationTokenSource = new CancellationTokenSource();

                    await UniTask.Delay(TimeSpan.FromSeconds(_gameConfig.furyDuration), cancellationToken: _rageCancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("Rage mode duration cancelled.");
                }
                finally
                {
                    DeactivateRageMode();
                }
            }

            private void DeactivateRageMode()
            {
                if (!_isRageActive) return;

                _isRageActive = false;
                _currentRage = 0;
                OnRageChanged?.Invoke(_currentRage);
                OnRageDeactivated?.Invoke();
                Debug.Log("Rage Mode Deactivated.");

                _rageCancellationTokenSource?.Cancel();
                _rageCancellationTokenSource?.Dispose();
                _rageCancellationTokenSource = new CancellationTokenSource();
                StartRageDecay();
            }

            private async UniTaskVoid StartRageDecay()
            {
                if (_isRageActive) return;

                CancellationToken token = _rageCancellationTokenSource.Token;
                while (_currentRage > 0 && !token.IsCancellationRequested && !_isRageActive)
                {
                    _currentRage = Mathf.Max(_currentRage - _gameConfig.furyDecayRate * Time.deltaTime, 0f);
                    OnRageChanged?.Invoke(_currentRage);
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
            }
        }
        