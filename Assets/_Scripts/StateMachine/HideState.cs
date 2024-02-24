
public class HideState : BaseState
{
    public HideState(EnemyActions enemyActions, StateMachine stateMachine) : base(enemyActions, stateMachine) { }

    public override void EnterState()
    {
        //Debug.Log("Enter Hide State");
    }
    public override void UpdateState()
    {
        if (_enemyActions.EnemyAlive)
        {
            _enemyActions.Hide();
        }
    }
    public override void ExitState()
    {

    }
}