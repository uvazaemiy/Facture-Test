using VContainer;
using VContainer.Unity;
using UnityEngine;

public class RootLifetimeScope : LifetimeScope
{
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private Bullet bulletPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputManager>(Lifetime.Singleton);
        builder.Register<PoolManager>(Lifetime.Singleton).WithParameter(bulletPrefab).AsSelf().AsImplementedInterfaces();
        builder.Register<GameStateMachine>(Lifetime.Singleton);

        builder.Register<BootstrapState>(Lifetime.Singleton);
        builder.Register<ReadyToStartState>(Lifetime.Singleton);
        builder.Register<GameplayState>(Lifetime.Singleton);
        builder.Register<DefeatState>(Lifetime.Singleton);
        builder.Register<VictoryState>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<RageController>(); // Исправлено: RageController регистрируется как компонент в иерархии

        if (gameConfig == null)
        {
            Debug.LogError("GameConfig is not assigned in RootLifetimeScope. Please assign it in the Inspector.");
        }
        else
        {
            builder.RegisterInstance(gameConfig);
        }

        builder.RegisterComponentInHierarchy<GameDebugUI>();
        builder.RegisterComponentInHierarchy<InputHandler>();

        Debug.Log("RootLifetimeScope configured with basic services, game states, and GameConfig.");
    }
}