using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunData", menuName = "ScriptableObjects/ShotGunData", order = 1)]
public class ShotGunDataScriptableObject : WeaponDataScriptableObject
{
    [SerializeField, Range(1, 2)] private float _forceRandomFactor;
    [SerializeField] private float _spreadFactor;
    [SerializeField] protected ForceMode _forceMode;
    [SerializeField] protected float _force;

    public float ForceRandomFactor => _forceRandomFactor;
    public float SpreadFactor => _spreadFactor;
    public ForceMode ForceMode => _forceMode;
    public float Force => _force;
}