public class ChaseState : BaseState
{
    public ChaseState(EnemyActions enemyActions, StateMachine stateMachine) : base(enemyActions, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (_enemyActions.EnemyAlive && _enemyActions.CanAttack && !_enemyActions.LowHealth)
        {
            _stateMachine.SwitchState(_enemyActions.AttackState);
        }
        else if (_enemyActions.EnemyAlive && _enemyActions.CanChase && !_enemyActions.LowHealth)
        {
            _enemyActions.Chase();
        }
        else if (_enemyActions.EnemyAlive && _enemyActions.TargetLost)
        {
            _stateMachine.SwitchState(_enemyActions.SearchState);
        }
        else if (_enemyActions.LowHealth == true)
        {
            _stateMachine.SwitchState(_enemyActions.HideState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}