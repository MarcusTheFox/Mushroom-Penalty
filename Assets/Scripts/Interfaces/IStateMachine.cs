public interface IStateMachine 
{
    public IState CurrentState { get; }

    public void Initialize(IState startingState);
    public void Update();
    public void ChangeState(IState newState);
}
