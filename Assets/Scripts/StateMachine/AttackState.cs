
public class AttackState : BaseState
{
    public AttackState(BotActions botActions, StateMachine stateMachine) : base(botActions, stateMachine) { }

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
            else if(!_botActions.EnemyNearDeath)
            {
                _stateMachine.SwitchState(_botActions.HideState);
            }
        }
        else if (_botActions.EnemyAlive && !_botActions.CanAttack)
        {
            _stateMachine.SwitchState(_botActions.ChaseState);
        }
        else if (!_botActions.EnemyAlive || _botActions.TargetLost)
        {
            _stateMachine.SwitchState(_botActions.SearchState);
        }
        else
        {
            _botActions.Attack();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
