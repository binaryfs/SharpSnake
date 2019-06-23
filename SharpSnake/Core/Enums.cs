namespace SharpSnake.Core
{
    public enum StateId
    {
        None,
        Menu,
        Options,
        Play,
        GameOver,
        Pause
    }

    public enum StateChangeCommand
    {
        PushState,
        PopState,
        ClearStack
    }
}
