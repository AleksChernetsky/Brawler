public class HideState : BaseState
{
    public HideState(BotActions botActions, StateMachine stateMachine) : base(botActions, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (_botActions.LowHealth || !_botActions.HasAmmo)
        {
            if (_botActions.HasAmmo && _botActions.EnemyNearDeath && _botActions.CanAttack)
            {
                _botActions.Attack();
            }
            else
            {
                _botActions.Hide();
            }
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