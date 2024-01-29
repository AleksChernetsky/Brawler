using UnityEngine;

[CreateAssetMenu(fileName = "SniperRifleData", menuName = "ScriptableObjects/SniperRifleData", order = 4)]
public class SniperRifleDataScriptableObject : WeaponDataScriptableObject
{    
    [SerializeField] protected ForceMode _forceMode;
    [SerializeField] protected float _force;
    public ForceMode ForceMode => _forceMode;
    public float Force => _force;
}