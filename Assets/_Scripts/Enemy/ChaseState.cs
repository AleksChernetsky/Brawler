using UnityEngine;

public class ChaseState : BaseState
{
    public ChaseState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine) {}

    public override void EnterState()
    {
        Debug.Log("Enter Chase State");
    }
    public override void UpdateState()
    {
        if (_enemy.IsBlocked == false && _enemy.DistanceToEnemy <= _enemy.AttackDistance)
        {
            _stateMachine.SwitchState(_enemy.AttackState);
        }
        else if (_enemy.DistanceToEnemy > _enemy.DetectDistance || _enemy.target is null)
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