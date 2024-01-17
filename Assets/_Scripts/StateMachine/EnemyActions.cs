using UnityEngine;
using UnityEngine.AI;

public class EnemyActions : MonoBehaviour
{
    [SerializeField] protected Weapon _weapon;

    private EnemyTracker _enemyTracker;
    private AnimationHandler _animHandler;
    private NavMeshAgent _agent;

    private float ReloadTime;
    public float FireRate;
    public float patrolAreaStepRadius;

    public bool CanChase => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToChase;
    public bool CanAttack => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToAttack && !_enemyTracker.IsBlocked;
    public bool TargetLost => _enemyTracker.DistanceToEnemy > _enemyTracker.DistanceToChase || _enemyTracker.Enemy != null;
    public bool EnemyAlive => _enemyTracker.Enemy != null;

    public StateMachine StateMachine { get; set; }
    public ChaseState ChaseState { get; set; }
    public AttackState AttackState { get; set; }
    public SearchState SearchState { get; set; }

    private void Awake()
    {
        StateMachine = new StateMachine();
        SearchState = new SearchState(this, StateMachine);
        ChaseState = new ChaseState(this, StateMachine);
        AttackState = new AttackState(this, StateMachine);

        _enemyTracker = GetComponent<EnemyTracker>();
        _animHandler = GetComponent<AnimationHandler>();
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        StateMachine.Initialize(SearchState);
    }
    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public void EnemySearch()
    {
        _agent.isStopped = false;

        if (!_agent.hasPath)
        {
            _agent.SetDestination(PatrolArea.Instance.GetRandomPoint());
        }

    }
    public void EnemyChase()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_enemyTracker.Enemy.transform.position);
    }
    public void Attack()
    {
        _animHandler.PlayAttackAnimation();

        _agent.isStopped = true;
        ReloadTime += Time.deltaTime;
        _agent.transform.LookAt(_enemyTracker.Enemy.transform.position);

        if (ReloadTime >= FireRate)
        {
            _weapon.Shoot();
            ReloadTime = 0;
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, patrolAreaStepRadius);
    }
#endif
}