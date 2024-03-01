public class HideState : BaseState
{
    public HideState(BotActions botActions, StateMachine stateMachine) : base(botActions, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (_botActions.LowHealth)
        {
            _botActions.Hide();
        }
        else if(_botActions.EnemyAlive && _botActions.EnemyNearDeath)
        {
            _stateMachine.SwitchState(_botActions.AttackState);
        }
        else
        {
            _stateMachine.SwitchState(_botActions.SearchState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}