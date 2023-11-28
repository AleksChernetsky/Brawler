using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Weapon _weapon;

    private EnemyTracker _enemyTracker;
    private AnimationHandler _animHandler;
    private GameObject[] _patrolPoints;
    private NavMeshAgent _agent;

    private int _currentPatrolPoint = 0, _distanceToChangePatrolPoint = 2;
    private float ReloadTime;
    public float FireRate;

    public StateMachine StateMachine { get; set; }
    public ChaseState ChaseState { get; set; }
    public AttackState AttackState { get; set; }
    public SearchState SearchState { get; set; }    

    private void Awake()
    {
        _enemyTracker = GetComponent<EnemyTracker>();

        StateMachine = new StateMachine();

        ChaseState = new ChaseState(this, StateMachine, _enemyTracker);
        AttackState = new AttackState(this, StateMachine, _enemyTracker);
        SearchState = new SearchState(this, StateMachine, _enemyTracker);
    }

    private void Start()
    {
        _animHandler = GetComponent<AnimationHandler>();
        _agent = GetComponent<NavMeshAgent>();
        _patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");

        StateMachine.Initialize(SearchState);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    // -----------------------------------------------------------------------------------------------------------------    

    public void EnemySearch()
    {
        _agent.isStopped = false;
        _animHandler.PlayRunAnimation();

        _agent.destination = _patrolPoints[_currentPatrolPoint].transform.position;

        if (_agent.remainingDistance < _distanceToChangePatrolPoint)
            _currentPatrolPoint = UnityEngine.Random.Range(0, _patrolPoints.Length);
    }

    public void EnemyChase()
    {
        _agent.isStopped = false;
        _animHandler.PlayRunAnimation();

        _agent.destination = _enemyTracker.target.transform.position;
    }

    public void Attack()
    {
        _animHandler.PlayAttackAnimation();
        _agent.isStopped = true;
        ReloadTime += Time.deltaTime;
        _agent.transform.LookAt(_enemyTracker.target.transform.position);

        if (ReloadTime >= FireRate)
        {
            _weapon.ShootShotGun();
            ReloadTime = 0;
        }
    }
}
