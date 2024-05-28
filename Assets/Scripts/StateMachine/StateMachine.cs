public class StateMachine
{
    public BaseState CurrentState { get; set; }

    public void Initialize(BaseState defaultState)
    {
        CurrentState = defaultState;
        CurrentState.EnterState();
    }

    public void SwitchState(BaseState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
