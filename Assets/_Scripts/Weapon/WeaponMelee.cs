public class WeaponMelee : WeaponMain
{
    private EnemyTracker _enemyTracker;
    private int AttackCount;    

    private void Start()
    {
        _enemyTracker = GetComponent<EnemyTracker>();
        AttackCount = 0;
    }

    public override void Shoot()
    {
        if (attackTimer >= fireRate)
        {
            if (AttackCount == 0)
            {
                CallOnFistAttack();
                if (_enemyTracker.Enemy.TryGetComponent(out VitalitySystem vitalitySystem))
                {
                    vitalitySystem.TakeDamage(damageInfo);
                }
                AttackCount++;
                attackTimer = 0;
            }
            else if (AttackCount == 1)
            {
                CallOnClawAttack();
                if (_enemyTracker.Enemy.TryGetComponent(out VitalitySystem vitalitySystem))
                {
                    vitalitySystem.TakeDamage(damageInfo);
                }
                AttackCount--;
                attackTimer = 0;
            }
        }
    }
}
