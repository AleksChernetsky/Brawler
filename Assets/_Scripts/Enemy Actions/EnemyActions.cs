using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(VitalitySystem))]
[RequireComponent(typeof(EnemyTracker))]

public class EnemyActions : MonoBehaviour
{
    private WeaponMain _weaponMain;
    private EnemyTracker _enemyTracker;
    private NavMeshAgent _agent;
    private VitalitySystem _vitalitySystem;

    public bool CanChase => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToChase;
    public bool CanAttack => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToAttack && !_enemyTracker.Sight;
    public bool TargetLost => _enemyTracker.DistanceToEnemy > _enemyTracker.DistanceToChase || _enemyTracker.Enemy != null;
    public bool EnemyAlive => _enemyTracker.Enemy != null;
    public bool LowHealth => _vitalitySystem.CurrentHealth <= _vitalitySystem.MaxHealth / 3;

    public StateMachine StateMachine { get; set; }
    public SearchState SearchState { get; set; }
    public ChaseState ChaseState { get; set; }
    public AttackState AttackState { get; set; }
    public HideState HideState { get; set; }

    private void Awake()
    {
        StateMachine = new StateMachine();
        SearchState = new SearchState(this, StateMachine);
        ChaseState = new ChaseState(this, StateMachine);
        AttackState = new AttackState(this, StateMachine);
        HideState = new HideState(this, StateMachine);

        _weaponMain = GetComponent<WeaponMain>();
        _enemyTracker = GetComponent<EnemyTracker>();
        _agent = GetComponent<NavMeshAgent>();
        _vitalitySystem = GetComponent<VitalitySystem>();
    }
    private void Start()
    {
        StateMachine.Initialize(HideState);
        _vitalitySystem.OnDeath += DeathState;
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
        _agent.velocity = _agent.desiredVelocity;
    }

    public void EnemySearch()
    {
        if (!_agent.hasPath)
        {
            _agent.SetDestination(PatrolArea.Instance.GetRandomPoint());
        }
    }
    public void EnemyChase()
    {
        _agent.SetDestination(_enemyTracker.Enemy.position);
    }
    public void Attack()
    {
        _agent.SetDestination(gameObject.transform.position);
        _agent.transform.LookAt(_enemyTracker.Enemy.position);
        _weaponMain.Shoot();
    }
    public void Hide()
    {

    }
    private void DeathState() => StateMachine.CurrentState.ExitState();

    bool SavePoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, _agent.areaMask))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}