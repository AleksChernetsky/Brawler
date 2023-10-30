public class BaseState
{
    protected Enemy _enemy;
    protected StateMachine _stateMachine;

    public BaseState(Enemy enemy, StateMachine stateMachine)
    {
        this._enemy = enemy;
        this._stateMachine = stateMachine;
    }

    public virtual void EnterState()
    {
    }

    public virtual void UpdateState()
    {
    }

    public virtual void ExitState()
    {
    }
}