using UnityEngine;

public class ChaseState : BaseState
{
    public ChaseState(Enemy enemy, StateMachine stateMachine, EnemyTracker enemyTracker) : base(enemy, stateMachine, enemyTracker) {}

    public override void EnterState()
    {
        Debug.Log("Enter Chase State");
    }
    public override void UpdateState()
    {
        if (_enemyTracker.IsBlocked == false && _enemyTracker.DistanceToEnemy <= _enemyTracker.AttackDistance)
        {
            _stateMachine.SwitchState(_enemy.AttackState);
        }
        else if (_enemyTracker.DistanceToEnemy > _enemyTracker.DetectDistance)
        {
            _stateMachine.SwitchState(_enemy.SearchState);
        }
        else
        {
            _enemy.EnemyChase();
        }
    }
    public override void ExitState()
    {
        Debug.Log("Exit Chase State");
    }
}