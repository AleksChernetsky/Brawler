using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPositionTracker : MonoBehaviour
{
    public List<Transform> _targets;

    private void Start()
    {
        var objects = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        foreach (var obj in objects)
        {
            _targets.Add(obj.transform);
        }
    }

    private void FindClosestTarget()
    {
        var bestDistance = float.MaxValue;
        var bestUnit = default(EnemyPositionTracker);

        foreach (var enemy in _targets)
        {
            var distSqr = (transform.position - enemy.position).sqrMagnitude;//получаем квадрат расстояния до врага
            if (distSqr < bestDistance)//ближе чем предыдущий кандидат?
            {
                //запоминаем врага
                bestDistance = distSqr;
                //bestUnit = enemy;
            }            
        }
    }
}