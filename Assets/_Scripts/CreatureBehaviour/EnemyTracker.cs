using UnityEngine;
using UnityEngine.AI;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField] private LayerMask layermask;
    [SerializeField] private float checkRadius;

    private Collider[] enemyColliders;

    public bool IsBlocked;
    public float DistanceToEnemy, DetectDistance, AttackDistance;

    public Transform target;

    void OnDrawGizmosSelected()
    {
        if (target == null) Gizmos.color = Color.white;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    private void Start()
    {
        DistanceToEnemy = float.MaxValue;
    }

    private void Update()
    {
        enemyColliders = Physics.OverlapSphere(transform.position, checkRadius, layermask);

        foreach (var enemies in enemyColliders)
        {
            if (enemies.transform != transform)
            {
                DistanceToEnemy = (transform.position - enemies.transform.position).sqrMagnitude;

                if (DistanceToEnemy <= DetectDistance)
                {
                    target = enemies.transform;
                    EnemyBlocked();
                }
                else
                {
                    target = null;
                    DistanceToEnemy = float.MaxValue;
                }
            }
        }
    }

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
}