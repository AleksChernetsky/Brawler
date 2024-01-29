using UnityEngine;
using UnityEngine.AI;

public class EnemyActions : MonoBehaviour
{
    private WeaponMain _weaponMain;
    private EnemyTracker _enemyTracker;
    private NavMeshAgent _agent;
    private VitalitySystem _vitalitySystem;

    public bool CanChase => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToChase;
    public bool CanAttack => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToAttack && !_enemyTracker.IsBlocked;
    public bool TargetLost => _enemyTracker.DistanceToEnemy > _enemyTracker.DistanceToChase || _enemyTracker.Enemy != null;
    public bool EnemyAlive => _enemyTracker.Enemy != null;

    public StateMachine StateMachine { get; set; }
    public SearchState SearchState { get; set; }
    public ChaseState ChaseState { get; set; }
    public AttackState AttackState { get; set; }

    private void Awake()
    {
        StateMachine = new StateMachine();
        SearchState = new SearchState(this, StateMachine);
        ChaseState = new ChaseState(this, StateMachine);
        AttackState = new AttackState(this, StateMachine);

        _weaponMain = GetComponent<WeaponMain>();
        _enemyTracker = GetComponent<EnemyTracker>();
        _agent = GetComponent<NavMeshAgent>();
        _vitalitySystem = GetComponent<VitalitySystem>();
    }
    private void Start()
    {
        StateMachine.Initialize(SearchState);
        _vitalitySystem.OnDeath += DeathState;
    }

    private void Update()
    {
        if (StateMachine.CurrentState != null)
        {
            StateMachine.CurrentState.UpdateState();
        }
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
    private void DeathState() => StateMachine.CurrentState = null;

}