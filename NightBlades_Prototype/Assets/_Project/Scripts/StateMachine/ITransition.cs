namespace NBProtoype
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}
