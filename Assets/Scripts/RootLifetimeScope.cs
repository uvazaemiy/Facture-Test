using VContainer;
using VContainer.Unity;
using UnityEngine;

public class InputSystem
{
    public InputSystem()
    {
        Debug.Log("InputSystem initialized.");
    }
}

public class PoolManager
{
    public PoolManager()
    {
        Debug.Log("PoolManager initialized.");
    }
}

public class RootLifetimeScope : LifetimeScope
{
    [SerializeField] private GameConfig gameConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputSystem>(Lifetime.Singleton);
        builder.Register<PoolManager>(Lifetime.Singleton);
        builder.Register<GameStateMachine>(Lifetime.Singleton);

        builder.Register<BootstrapState>(Lifetime.Singleton);
        builder.Register<ReadyToStartState>(Lifetime.Singleton);
        builder.Register<GameplayState>(Lifetime.Singleton);
        builder.Register<DefeatState>(Lifetime.Singleton);
        builder.Register<VictoryState>(Lifetime.Singleton);

        if (gameConfig == null)
        {
            Debug.LogError("GameConfig is not assigned in RootLifetimeScope. Please assign it in the Inspector.");
        }
        else
        {
            builder.RegisterInstance(gameConfig);
        }

        builder.RegisterComponentInHierarchy<GameDebugUI>();

        Debug.Log("RootLifetimeScope configured with basic services, game states, and GameConfig.");
    }
}