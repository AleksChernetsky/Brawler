using UnityEngine;
using UnityEngine.AI;

public class EnemyTracker : MonoBehaviour
{
    private float _checkCollidersDelay;
    private float DistanceToTarget;
    private Collider[] enemyColliders;

    [Header("Enemy Check Values")]
    [SerializeField] private LayerMask layermask;
    [SerializeField] private float _distanceToCheck;
    [SerializeField] private float _distanceToChase;
    [SerializeField] private float _distanceToAttack;

    [Header("Enemy Values")]
    public Transform Enemy;
    public float DistanceToEnemy;
    public bool EnemyBlocked;

    public float DistanceToCheck { get => _distanceToCheck; set => _distanceToCheck = value; }
    public float DistanceToChase { get => _distanceToChase; set => _distanceToChase = value; }
    public float DistanceToAttack { get => _distanceToAttack; set => _distanceToAttack = value; }

    private void OnDrawGizmos()
    {
        if (Enemy == null) Gizmos.color = Color.white;
        else Gizmos.color = Color.red;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DistanceToCheck);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DistanceToChase);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanceToAttack);
    }

    private void Awake() => DistanceToEnemy = Mathf.Infinity;

    private void Update() => GetTarget();

    private void GetTarget()
    {
        _checkCollidersDelay += Time.deltaTime;
        if (_checkCollidersDelay >= 0.25f)
        {
            enemyColliders = Physics.OverlapSphere(transform.position, DistanceToCheck, layermask);
            EnemySight();
            Enemy = NearestObject();
            _checkCollidersDelay = 0;
        }
    }

    private Transform NearestObject()
    {
        DistanceToEnemy = Mathf.Infinity;
        Transform target = null;

        for (var i = 0; i < enemyColliders.Length; i++)
        {
            if (enemyColliders[i].transform == transform)
                continue;

            DistanceToTarget = Vector3.Distance(transform.position, enemyColliders[i].transform.position);

            if (DistanceToTarget < DistanceToEnemy)
            {
                DistanceToEnemy = DistanceToTarget;
                target = enemyColliders[i].transform;
            }
        }
        return target;
    }

    private void EnemySight()
    {
        NavMeshHit hit;
        if (Enemy != null)
        {
            EnemyBlocked = NavMesh.Raycast(transform.position, Enemy.transform.position, out hit, NavMesh.AllAreas);
            Debug.DrawLine(transform.position, Enemy.transform.position, EnemyBlocked ? Color.red : Color.green);

            if (EnemyBlocked)
            Debug.DrawRay(hit.position, Vector3.up, Color.yellow, 0.5f);
        }
    }
}