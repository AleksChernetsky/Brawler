using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Weapon weapon;

    public EnemyTracker enemyTracker;
    private AnimationHandler _animHandler;
    private GameObject[] _patrolPoints;
    private NavMeshAgent _agent;

    private int _currentPatrolPoint = 0, _distanceToChangePatrolPoint = 2;
    private float ReloadTime;
    public float FireRate;

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }

    public StateMachine StateMachine { get; set; }
    public ChaseState ChaseState { get; set; }
    public AttackState AttackState { get; set; }
    public SearchState SearchState { get; set; }

    private void Awake()
    {
        StateMachine = new StateMachine();

        ChaseState = new ChaseState(this, StateMachine, enemyTracker);
        AttackState = new AttackState(this, StateMachine, enemyTracker);
        SearchState = new SearchState(this, StateMachine, enemyTracker);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

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
            _currentPatrolPoint = Random.Range(0, _patrolPoints.Length);
    }

    public void EnemyChase()
    {
        _agent.isStopped = false;
        _animHandler.PlayRunAnimation();

        _agent.destination = enemyTracker.target.transform.position;
    }

    public void Attack()
    {
        _animHandler.PlayAttackAnimation();
        _agent.isStopped = true;
        ReloadTime += Time.deltaTime;
        _agent.transform.LookAt(enemyTracker.target.transform.position);

        if (ReloadTime >= FireRate)
        {
            weapon.ShootRifle();
            ReloadTime = 0;
        }
    }

    // -----------------------------------------------------------------------------------------------------------------


    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
