namespace SharpSnake.Core
{
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