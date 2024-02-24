using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform Player;
    public float CheckRange;
    public bool EnemyBlocked;
    public int safeDistance;
    private Vector3 safePoint;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        EnemySight();
        if (!EnemyBlocked)
        {
            if (!_agent.hasPath)
            {
                _agent.SetDestination(SafePosition());
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            randomPoint.y = 0;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                Debug.DrawRay(randomPoint, Vector3.up, Color.white);
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    private Vector3 SafePosition()
    {
        NavMeshHit hit;
        Vector3 result;
        if (RandomPoint(transform.position, CheckRange, out result))
        {
            bool canHide = NavMesh.Raycast(result, Player.position, out hit, NavMesh.AllAreas);

            Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
            Vector3 toOther = hit.position - transform.position.normalized;
            float dotProduct = Vector3.Dot(forward, toOther);

            if (dotProduct < 0)
            {
                if (canHide)
                {
                    safePoint = hit.position;
                }
                print("The other transform is behind me!");
            }

            
        }
        return safePoint;
    }

    private void EnemySight()
    {
        NavMeshHit hit;
        if (Player != null)
        {
            EnemyBlocked = NavMesh.Raycast(transform.position, Player.position, out hit, NavMesh.AllAreas);
            Debug.DrawLine(transform.position, Player.position, EnemyBlocked ? Color.red : Color.green);

            if (EnemyBlocked)
                Debug.DrawRay(hit.position, Vector3.up, Color.yellow);
        }
    }
}