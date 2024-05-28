
public class BaseState
{
    protected BotActions _botActions;
    protected StateMachine _stateMachine;

    public BaseState(BotActions botActions, StateMachine stateMachine)
    {
        _botActions = botActions;
        _stateMachine = stateMachine;
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }
}
