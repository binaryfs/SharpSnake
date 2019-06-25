namespace SharpSnake.Core
{
    /// <summary>
    /// Encapsulates data that is sent to a <see cref="StateStack"/> instance when requesting state changes.
    /// </summary>
    public struct StateChangeInfo
    {
        public readonly StateChangeCommand Command;
        public readonly StateId StateId;

        public StateChangeInfo(StateChangeCommand command, StateId stateId = StateId.None)
        {
            Command = command;
            StateId = stateId;
        }
    }
}