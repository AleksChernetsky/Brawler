public class ChaseState : BaseState
{
    public ChaseState(BotActions botActions, StateMachine stateMachine) : base(botActions, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (_botActions.EnemyAlive && _botActions.CanAttack)
        {
            _stateMachine.SwitchState(_botActions.AttackState);
        }
        else if (!_botActions.EnemyAlive || _botActions.TargetLost)
        {
            _stateMachine.SwitchState(_botActions.SearchState);
        }
        else
        {
            _botActions.Chase();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}