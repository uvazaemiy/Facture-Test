using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class PoolManager : IStartable
{
    private readonly Bullet _bulletPrefab;
    private readonly IObjectResolver _resolver;

    private ObjectPool<Bullet> _bulletPool;

    public PoolManager(Bullet bulletPrefab, IObjectResolver resolver)
    {
        _bulletPrefab = bulletPrefab;
        _resolver = resolver;
        Debug.Log("PoolManager constructed.");
    }

    public void Start()
    {
        _bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true, 100, 1000);
        Debug.Log("PoolManager pools initialized.");
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = _resolver.Instantiate(_bulletPrefab);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Object.Destroy(bullet.gameObject);
    }

    public Bullet GetBullet()
    {
        return _bulletPool.Get();
    }

    public IObjectPool<Bullet> GetBulletPool()
    {
        return _bulletPool;
    }

    public void ReleaseBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }
}