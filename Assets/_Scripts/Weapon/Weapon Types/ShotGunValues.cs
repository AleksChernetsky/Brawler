using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunValues", menuName = "ScriptableObjects/ShotGunValues", order = 1)]
public class ShotGunValues : WeaponValues
{    
    [SerializeField, Range(1, 2)] private float _forceRandomFactor;
    [SerializeField] private int _projectileCount;
    [SerializeField] private float _spreadFactor;

    public float ForceRandomFactor => _forceRandomFactor;
    public int ProjectileCount => _projectileCount;
    public float SpreadFactor => _spreadFactor;
}