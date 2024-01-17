using UnityEngine;
using UnityEngine.AI;

public class EnemyTracker : MonoBehaviour
{
    private float DistanceToTarget;
    private Collider[] enemyColliders;

    [Header("Enemy Check Values")]
    [SerializeField] private LayerMask layermask;
    [SerializeField] private float _distanceToCheck;
    [SerializeField] private float _distanceToChase;
    [SerializeField] private float _distanceToRangeAttack;
    //[SerializeField] private float _distanceToMeleeAttack;

    [Header("Enemy Values")]
    public GameObject Enemy;
    public float DistanceToEnemy;
    public bool IsBlocked;

    public float DistanceToCheck { get => _distanceToCheck; set => _distanceToCheck = value; }
    public float DistanceToChase { get => _distanceToChase; set => _distanceToChase = value; }
    public float DistanceToAttack { get => _distanceToRangeAttack; set => _distanceToRangeAttack = value; }

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

    private void Awake()
    {
        DistanceToEnemy = Mathf.Infinity;
    }

    private void Update()
    {
        enemyColliders = Physics.OverlapSphere(transform.position, DistanceToCheck, layermask);
        Enemy = NearestObject();
        EnemyBlocked();
    }

    private GameObject NearestObject()
    {
        DistanceToEnemy = Mathf.Infinity;
        GameObject target = null;

        for (var i = 0; i < enemyColliders.Length; i++)
        {
            DistanceToTarget = Vector3.Distance(transform.position, enemyColliders[i].transform.position);
            if (DistanceToTarget < DistanceToEnemy && DistanceToTarget > 0)
            {
                DistanceToEnemy = DistanceToTarget;
                target = enemyColliders[i].gameObject;
            }
        }
        return target;
    }

    private void EnemyBlocked()
    {
        NavMeshHit hit;
        if (Enemy != null)
        {
            IsBlocked = NavMesh.Raycast(transform.position, Enemy.transform.position, out hit, NavMesh.AllAreas);
            Debug.DrawLine(transform.position, Enemy.transform.position, IsBlocked ? Color.red : Color.green);

            if (IsBlocked)
                Debug.DrawRay(hit.position, Vector3.up, Color.yellow);
        }
    }
}