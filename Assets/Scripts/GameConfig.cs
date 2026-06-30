using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Car Parameters")]
    public float carSpeed = 10f;
    public float carHealth = 100f;

    [Header("Turret Parameters")]
    public float turretDamage = 10f;
    public float turretFireRate = 0.5f;

    [Header("Fury Mechanics")]
    public float furyDuration = 5f;
    public float furyDamageMultiplier = 2f;

    [Header("Barrels Mechanics")]
    public int maxBarrels = 3;
    public float barrelExplosionRadius = 5f;

    public GameConfig()
    {
        Debug.Log("GameConfig created.");
    }
}