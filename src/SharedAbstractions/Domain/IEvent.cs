namespace SharedAbstractions.Domain
{
    public interface IEvent
    {
        DateTime Created => DateTime.UtcNow;
    }
}