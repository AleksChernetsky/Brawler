using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(EnemyActions enemyActions, StateMachine stateMachine) : base(enemyActions, stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Enter Attack State");
    }
    public override void UpdateState()
    {
        if (_enemyActions.EnemyAlive && _enemyActions.CanAttack)
        {
            _enemyActions.Attack();
        }
        else if (_enemyActions.EnemyAlive && !_enemyActions.CanAttack)
        {
            _stateMachine.SwitchState(_enemyActions.ChaseState);
        }
        else if (!_enemyActions.EnemyAlive || _enemyActions.TargetLost)
        {
            _stateMachine.SwitchState(_enemyActions.SearchState);
        }
    }
    public override void ExitState()
    {

    }
}
