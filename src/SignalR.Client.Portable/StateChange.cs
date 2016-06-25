namespace SignalR.Client.Portable
{
    public class StateChange
    {
        public ConnectionState OldState { get; private set; }
        public ConnectionState NewState { get; private set; }

        public StateChange(ConnectionState oldState, ConnectionState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
