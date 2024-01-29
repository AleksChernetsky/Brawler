using UnityEngine;

[CreateAssetMenu(fileName = "SMGData", menuName = "ScriptableObjects/SMGData", order = 3)]
public class SMGDataScriptableObject : WeaponDataScriptableObject
{
    [SerializeField] private float _spreadFactor;
    [SerializeField] protected ForceMode _forceMode;
    [SerializeField] protected float _force;

    public float SpreadFactor => _spreadFactor;
    public ForceMode ForceMode => _forceMode;
    public float Force => _force;
}