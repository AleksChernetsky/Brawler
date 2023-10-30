using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Weapon weapon;
    public List<Transform> _targets;
    public Transform target = null;

    private EnemyPositionTracker _positionTracker;
    private AnimationHandler _animHandler;
    private GameObject[] _patrolPoints;
    private NavMeshAgent _agent;

    private int _currentPatrolPoint = 0, _distanceToChangePatrolPoint = 2;
    private float ReloadTime;

    public bool IsBlocked;
    public float DetectDistance, AttackDistance, DistanceToEnemy, FireRate;

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }

    public StateMachine StateMachine { get; set; }
    public ChaseState ChaseState { get; set; }
    public AttackState AttackState { get; set; }
    public SearchState SearchState { get; set; }

    private void Awake()
    {
        StateMachine = new StateMachine();

        ChaseState = new ChaseState(this, StateMachine);
        AttackState = new AttackState(this, StateMachine);
        SearchState = new SearchState(this, StateMachine);
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

    private void FixedUpdate()
    {
        if (target != null)
        {
            DistanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
            EnemyBlocked();
        }
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

        _agent.destination = target.transform.position;
    }

    public void Attack()
    {
        _animHandler.PlayAttackAnimation();
        _agent.isStopped = true;
        ReloadTime += Time.deltaTime;
        _agent.transform.LookAt(target.transform.position);

        if (ReloadTime >= FireRate)
        {
            weapon.ShootRifle();
            ReloadTime = 0;
        }
    }

    // -----------------------------------------------------------------------------------------------------------------
    private void EnemyBlocked()
    {
        NavMeshHit hit;

        IsBlocked = NavMesh.Raycast(transform.position, target.transform.position, out hit, NavMesh.AllAreas);
        Debug.DrawLine(transform.position, target.transform.position, IsBlocked ? Color.red : Color.green);

        if (IsBlocked)
        {
            Debug.DrawRay(hit.position, Vector3.up, Color.yellow);
            IsBlocked = true;
        }
        else
        {
            IsBlocked = false;
        }
    }

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
