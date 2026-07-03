    using UnityEngine;
    using VContainer;
    using Cysharp.Threading.Tasks;

    public class TurretShooter : MonoBehaviour
    {
        [SerializeField] private Transform _bulletSpawnPoint;

        private InputManager _inputManager;
        private PoolManager _poolManager;
        private GameConfig _gameConfig;
        private RageController _rageController;

        private float _lastFireTime;
        private float _currentFireRate;
        private float _currentDamage;

        [Inject]
        public void Construct(InputManager inputManager, PoolManager poolManager, GameConfig gameConfig, RageController rageController)
        {
            _inputManager = inputManager;
            _poolManager = poolManager;
            _gameConfig = gameConfig;
            _rageController = rageController;
        }

        void Start()
        {
            UpdateShootingParameters();
            _rageController.OnRageActivated += OnRageActivated;
            _rageController.OnRageDeactivated += OnRageDeactivated;
        }

        void OnDestroy()
        {
            _rageController.OnRageActivated -= OnRageActivated;
            _rageController.OnRageDeactivated -= OnRageDeactivated;
        }

        void Update()
        {
            if (_inputManager.IsFireButtonPressed)
            {
                if (Time.time >= _lastFireTime + (1f / _currentFireRate))
                {
                    Shoot();
                    _lastFireTime = Time.time;
                }
            }
        }

        private void Shoot()
        {
            if (_bulletSpawnPoint == null)
            {
                Debug.LogError("Bullet Spawn Point is not assigned in TurretShooter.");
                return;
            }

            Bullet bullet = _poolManager.GetBullet();
            if (bullet != null)
            {
                bullet.Initialize(
                    _bulletSpawnPoint.position,
                    _bulletSpawnPoint.rotation,
                    _bulletSpawnPoint.forward,
                    _gameConfig.bulletSpeed,
                    _currentDamage,
                    _poolManager.GetBulletPool()
                );
            }
        }

        private void OnRageActivated()
        {
            UpdateShootingParameters();
        }

        private void OnRageDeactivated()
        {
            UpdateShootingParameters();
        }

        private void UpdateShootingParameters()
        {
            if (_rageController.IsRageActive)
            {
                _currentFireRate = _gameConfig.turretFireRate * _gameConfig.furyFireRateMultiplier;
                _currentDamage = _gameConfig.turretDamage * _gameConfig.furyDamageMultiplier;
                Debug.Log($"Shooting parameters updated: Rage Active. Fire Rate: {_currentFireRate}, Damage: {_currentDamage}");
            }
            else
            {
                _currentFireRate = _gameConfig.turretFireRate;
                _currentDamage = _gameConfig.turretDamage;
                Debug.Log($"Shooting parameters updated: Normal. Fire Rate: {_currentFireRate}, Damage: {_currentDamage}");
            }
        }
    }
    