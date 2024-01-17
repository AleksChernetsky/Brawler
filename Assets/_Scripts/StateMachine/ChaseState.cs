using UnityEngine;

public class ChaseState : State
{
    public ChaseState(EnemyActions enemyActions, StateMachine stateMachine) : base(enemyActions, stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Enter Chase State");
    }
    public override void UpdateState()
    {
        if (_enemyActions.EnemyAlive && _enemyActions.CanAttack)
        {
            _stateMachine.SwitchState(_enemyActions.AttackState);
        }
        else if (_enemyActions.EnemyAlive && _enemyActions.CanChase)
        {
            _enemyActions.EnemyChase();
        }
        else if (_enemyActions.EnemyAlive && _enemyActions.TargetLost)
        {
            _stateMachine.SwitchState(_enemyActions.SearchState);
        }
    }
    public override void ExitState()
    {

    }
}
