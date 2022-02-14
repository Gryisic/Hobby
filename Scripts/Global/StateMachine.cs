public class StateMachine 
{
    private State _state;

    public StateMachine(State initialState)
    {
        _state = initialState;

        _state.Enter();
    }

    public void ChangeState(State state)
    {
        _state.Exit();

        _state = state;

        _state.Enter();
    }
}
