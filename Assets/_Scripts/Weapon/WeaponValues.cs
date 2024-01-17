using UnityEngine;

[CreateAssetMenu(fileName = "WeaponValues", menuName = "ScriptableObjects/WeaponValues", order = 1)]
public class WeaponValues : ScriptableObject
{
    [SerializeField] protected float _damage;
    [SerializeField] protected ForceMode _forceMode;
    [SerializeField] protected float _force;

    public float Damage => _damage;
    public ForceMode ForceMode => _forceMode;
    public float Force => _force;
}