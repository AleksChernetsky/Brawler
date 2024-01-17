using UnityEngine;

public class SearchState : BaseState
{
    public SearchState(EnemyActions enemyActions, StateMachine stateMachine) : base(enemyActions, stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Enter Search State");
    }
    public override void UpdateState()
    {
        _enemyActions.EnemySearch();

        if (_enemyActions.EnemyAlive && _enemyActions.CanChase)
        {
            _stateMachine.SwitchState(_enemyActions.ChaseState);
        }
        else if (_enemyActions.EnemyAlive && _enemyActions.CanAttack)
        {
            _stateMachine.SwitchState(_enemyActions.AttackState);
        }
    }
    public override void ExitState()
    {

    }
}
