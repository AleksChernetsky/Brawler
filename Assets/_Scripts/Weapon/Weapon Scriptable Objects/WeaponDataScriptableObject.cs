using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 0)]
public class WeaponDataScriptableObject : ScriptableObject
{
    [SerializeField] protected int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] protected int _reloadTime;
    [SerializeField] private int _magCapacity;

    public int Damage => _damage;
    public float AttackDelay => _attackDelay;
    public int ReloadTime => _reloadTime;
    public int MagCapacity => _magCapacity;
}