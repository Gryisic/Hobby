public class StateMachine 
{
    protected State _state;

    public StateMachine(State initialState)
    {
        _state = initialState;
        _state.Enter();
    }

    protected virtual void ChangeState(State newState)
    {
        if (_state != newState)
        {
            _state.Exit();
            _state = newState;
            _state.Enter();
        }
    }
}
