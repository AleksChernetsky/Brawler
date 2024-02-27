public class HideState : BaseState
{
    public HideState(EnemyActions enemyActions, StateMachine stateMachine) : base(enemyActions, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        _enemyActions.Hide();
        if (!_enemyActions.LowHealth)
        {
            _stateMachine.SwitchState(_enemyActions.SearchState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}