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

    private void Awake()
    {
        _enemyTracker = GetComponent<EnemyTracker>();
        _animHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
    }

    // -----------------------------------------------------------------------------------------------------------------

    public void SearchEnemy()
    {
        _agent.isStopped = false;
        _agent.destination = _patrolPoints[_currentPatrolPoint].transform.position;

        if (_agent.remainingDistance < _distanceToChangePatrolPoint)
            _currentPatrolPoint = Random.Range(0, _patrolPoints.Length);
    }
    public void ChaseEnemy()
    {
        //_agent.isStopped = false;
        //_agent.destination = _enemyTracker.target[0].position;
    }
    public void Attack()
    {
        //_agent.isStopped = true;
        //ReloadTime += Time.deltaTime;
        //_agent.transform.LookAt(_enemyTracker.target[0].position);

        //if (ReloadTime >= FireRate)
        //{
        //    _weapon.Shoot();
        //    _animHandler.PlayAttackAnimation();
        //    ReloadTime = 0;
        //}
    }
}
