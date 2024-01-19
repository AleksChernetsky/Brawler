using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunValues", menuName = "ScriptableObjects/ShotGunValues", order = 1)]
public class ShotGunValues : WeaponValues
{
    [SerializeField, Range(1, 2)] private float _forceRandomFactor;
    [SerializeField, Tooltip("Only even numbers")] private int _projectileNumber;
    [SerializeField] private float _spreadFactor;

    public float ForceRandomFactor => _forceRandomFactor;
    public int ProjectileNumber => _projectileNumber;
    public float SpreadFactor => _spreadFactor;
}