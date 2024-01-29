
public class MeleeWeapon : Weapon<WeaponDataScriptableObject>
{
    private EnemyTracker _enemyTracker;
    private int _attackCount;
    public bool DistanceToJump => _enemyTracker.DistanceToEnemy <= 5; 

    private void Start()
    {
        _enemyTracker = GetComponent<EnemyTracker>();
        _attackCount = 0;
    }

    public override void Shoot()
    {
        if (CanAttack && _attackCount == 0)
        {
            CanAttack = false;
            CallOnFistAttack();
            if (_enemyTracker.Enemy.TryGetComponent(out VitalitySystem vitalitySystem))
            {
                vitalitySystem.TakeDamage(_damageInfo);
            }
            _attackCount++;
            StartCoroutine(ResetAttackCooldown());
        }
        else if (CanAttack && _attackCount == 1)
        {
            CanAttack = false;
            CallOnClawAttack();
            if (_enemyTracker.Enemy.TryGetComponent(out VitalitySystem vitalitySystem))
            {
                vitalitySystem.TakeDamage(_damageInfo);
            }
            _attackCount--;
            StartCoroutine(ResetAttackCooldown());
        }
    }
}
