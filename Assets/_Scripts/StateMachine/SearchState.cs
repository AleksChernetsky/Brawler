public class SearchState : BaseState
{
    public SearchState(BotActions botActions, StateMachine stateMachine) : base(botActions, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        _botActions.Search();

        if (_botActions.EnemyAlive && _botActions.CanChase)
        {
            _stateMachine.SwitchState(_botActions.ChaseState);
        }
        else if (_botActions.EnemyAlive && _botActions.CanAttack)
        {
            _stateMachine.SwitchState(_botActions.AttackState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
