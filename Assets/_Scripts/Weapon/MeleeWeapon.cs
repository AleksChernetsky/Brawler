
using UnityEngine;

public class MeleeWeapon : WeaponMain
{
    [Header("Effects")]
    [SerializeField] private ParticleSystem _fistSlashEffect;
    [SerializeField] private ParticleSystem _clawSlashEffect;

    [Header("Sounds")]
    [SerializeField] private AudioClip _fistHitSound;
    [SerializeField] private AudioClip _clawHitSound;

    private EnemyTracker _enemyTracker;
    private AudioSource _audioSource;
    private int AttackCount;

    private void Start()
    {
        _enemyTracker = GetComponent<EnemyTracker>();
        _audioSource = GetComponent<AudioSource>();
        AttackCount = 0;
    }

    public override void Shoot()
    {
        if (attackTimer >= FireRate)
        {
            if (AttackCount == 0)
            {
                CallOnFistAttack();
                AttackCount++;
                attackTimer = 0;
            }
            else if (AttackCount == 1)
            {
                CallOnClawAttack();
                AttackCount--;
                attackTimer = 0;
            }
        }
    }
    private void Hit() // call in melee attack animations events
    {
        if (_enemyTracker.Enemy.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            vitalitySystem.TakeDamage(damageInfo);
            if (AttackCount == 1)
            {
                _audioSource.PlayOneShot(_fistHitSound);
                _fistSlashEffect.Emit(1);
            }
            else if (AttackCount == 0)
            {
                _audioSource.PlayOneShot(_clawHitSound);
                _clawSlashEffect.Emit(1);
            }
        }
    }
}
