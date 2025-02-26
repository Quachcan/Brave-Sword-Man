namespace Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine
{
    public class OldPlayerStateMachine
    {
        public OldPlayerState CurrentState { get; private set; }
    
        public void Initialize(OldPlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(OldPlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
