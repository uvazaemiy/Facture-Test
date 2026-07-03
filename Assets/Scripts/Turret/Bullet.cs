using UnityEngine;
using VContainer;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private GameConfig _gameConfig;
    private IObjectPool<Bullet> _pool;

    private float _speed;
    private float _damage;
    private float _lifeTime = 3f;
    private float _currentLifeTime;

    [Inject]
    public void Construct(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    public void Initialize(Vector3 position, Quaternion rotation, Vector3 direction, float speed, float damage, IObjectPool<Bullet> pool)
    {
        transform.position = position;
        transform.rotation = rotation;
        _speed = speed;
        _damage = damage;
        _pool = pool;
        _currentLifeTime = _lifeTime;
        gameObject.SetActive(true);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime <= 0)
        {
            ReturnToPool();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bullet hit: {other.name}. Damage: {_damage}");

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        _pool?.Release(this);
    }
}