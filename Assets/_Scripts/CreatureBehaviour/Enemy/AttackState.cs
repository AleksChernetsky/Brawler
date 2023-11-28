using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(Enemy enemy, StateMachine stateMachine, EnemyTracker enemyTracker) : base(enemy, stateMachine, enemyTracker) { }

    public override void EnterState()
    {
        Debug.Log("Enter Attack State");
    }
    public override void UpdateState()
    {
        if (_enemyTracker.target != null)
        {
            if (_enemyTracker.IsBlocked == true || _enemyTracker.DistanceToEnemy > _enemyTracker.AttackDistance)
            {
                _stateMachine.SwitchState(_enemy.ChaseState);
            }
            else
            {
                _enemy.Attack();
            }
        }
        else
        {
            _enemyTracker.target = null;
            _stateMachine.SwitchState(_enemy.SearchState);
        }
    }
    public override void ExitState()
    {

    }
}