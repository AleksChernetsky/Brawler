public class StateMachine
{
    public State CurrentState { get; set; }

    public void Initialize(State defaultState)
    {
        CurrentState = defaultState;
        CurrentState.EnterState();
    }

    public void SwitchState(State newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
