using UnityEngine;
using VContainer;

public class CarMovement : MonoBehaviour
{
    private GameConfig _gameConfig;

    [Inject]
    public void Construct(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _gameConfig.carSpeed * Time.fixedDeltaTime);
    }
}