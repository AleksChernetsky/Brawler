using UnityEngine;

[CreateAssetMenu(fileName = "MachineGunData", menuName = "ScriptableObjects/MachineGunData", order = 2)]
public class MachineGunDataScriptableObject : WeaponDataScriptableObject
{
    [SerializeField] private float _spreadFactor;
    [SerializeField] protected ForceMode _forceMode;
    [SerializeField] protected float _force;
    public float SpreadFactor => _spreadFactor;
    public ForceMode ForceMode => _forceMode;
    public float Force => _force;
}