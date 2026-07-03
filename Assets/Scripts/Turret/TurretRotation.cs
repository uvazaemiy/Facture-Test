using UnityEngine;
using VContainer;

public class TurretRotation : MonoBehaviour
{
    [SerializeField] private Transform _turretHeadTransform;

    private InputManager _inputManager;
    private GameConfig _gameConfig;

    [Inject]
    public void Construct(InputManager inputManager, GameConfig gameConfig)
    {
        _inputManager = inputManager;
        _gameConfig = gameConfig;
    }

    void Update()
    {
        if (_turretHeadTransform == null) return;

        Vector3 targetPosition = _inputManager.AimTargetPosition;
        Vector3 lookAtTarget = new Vector3(targetPosition.x, _turretHeadTransform.position.y, targetPosition.z);

        Vector3 direction = (lookAtTarget - _turretHeadTransform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _turretHeadTransform.rotation = Quaternion.RotateTowards(
                _turretHeadTransform.rotation,
                targetRotation,
                _gameConfig.turretRotationSpeed * Time.deltaTime
            );
        }
    }
}