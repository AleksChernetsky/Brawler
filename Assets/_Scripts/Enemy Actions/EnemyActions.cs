using System.Collections;

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

    [SerializeField] public LayerMask HidableLayers;
    private Collider[] HidableColliders = new Collider[10];
    private Coroutine GetHideCoroutine;
    private Coroutine AttackCoroutine;

    public bool CanChase => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToChase;
    public bool CanAttack => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToAttack && !_enemyTracker.EnemyBlocked;
    public bool TargetLost => _enemyTracker.DistanceToEnemy > _enemyTracker.DistanceToChase || _enemyTracker.Enemy != null;
    public bool EnemyAlive => _enemyTracker.Enemy != null;
    public bool LowHealth => _vitalitySystem.CurrentHealth <= _vitalitySystem.MaxHealth * 0.5f;

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
        StateMachine.Initialize(SearchState);
        _vitalitySystem.OnDeath += DeathState;
    }

    private void Update()
    {
        if (StateMachine.CurrentState != null)
        {
            StateMachine.CurrentState.UpdateState();
            _agent.velocity = _agent.desiredVelocity;
        }
    }

    public void Search()
    {
        if (!_agent.hasPath)
            _agent.SetDestination(PatrolArea.Instance.GetRandomPoint());
    }
    public void Chase()
    {
        _agent.SetDestination(_enemyTracker.Enemy.position);
    }
    public void Attack()
    {
        if (AttackCoroutine != null)
            StopCoroutine(AttackCoroutine);

        if (EnemyAlive)
            AttackCoroutine = StartCoroutine(AttackAction(_enemyTracker.Enemy));
    }
    public void Hide()
    {
        if (GetHideCoroutine != null)
            StopCoroutine(GetHideCoroutine);

        if (EnemyAlive)
            GetHideCoroutine = StartCoroutine(GetCoverAction(_enemyTracker.Enemy));
    }
    private IEnumerator AttackAction(Transform Target)
    {
        WaitForSeconds Wait = new WaitForSeconds(_weaponMain.FireRate);
        while (true)
        {
            _agent.SetDestination(transform.position);
            _agent.transform.LookAt(Target.position);
            _weaponMain.Shoot();
            break;
        }
        yield return Wait;
    }
    private IEnumerator GetCoverAction(Transform Target)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.2f);
        while (true)
        {
            for (int i = 0; i < HidableColliders.Length; i++)
                HidableColliders[i] = null;

            int hits = Physics.OverlapSphereNonAlloc(transform.position, _enemyTracker.DistanceToCheck, HidableColliders, HidableLayers);

            int hitReduction = 0;
            for (int i = 0; i < hits; i++)
            {
                if (Vector3.Distance(HidableColliders[i].transform.position, Target.position) < _enemyTracker.DistanceToChase)
                {
                    HidableColliders[i] = null;
                    hitReduction++;
                }
            }
            hits -= hitReduction;
            System.Array.Sort(HidableColliders, ColliderArraySortComparer);

            for (int i = 0; i < hits; i++)
            {
                if (NavMesh.SamplePosition(HidableColliders[i].transform.position, out NavMeshHit hit, 2f, _agent.areaMask))
                {
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, _agent.areaMask))
                    {
                        Debug.LogError($"Unable to find edge close to {hit.position}");
                    }
                    if (Vector3.Dot(hit.normal, (Target.position - hit.position).normalized) < 0 && !_agent.hasPath)
                    {
                        _agent.SetDestination(hit.position);
                        break;
                    }
                }
            }
            yield return Wait;
        }
    }
    private int ColliderArraySortComparer(Collider A, Collider B)
    {
        if (A == null && B != null)
        {
            return 1;
        }
        else if (A != null && B == null)
        {
            return -1;
        }
        else if (A == null && B == null)
        {
            return 0;
        }
        else
        {
            return Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position));
        }
    }
    private void DeathState() => StateMachine.CurrentState = null;
}