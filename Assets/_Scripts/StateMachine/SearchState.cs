public class SearchState : BaseState
{
    public SearchState(EnemyActions enemyActions, StateMachine stateMachine) : base(enemyActions, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        _enemyActions.Search();

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
        base.ExitState();
    }
}
