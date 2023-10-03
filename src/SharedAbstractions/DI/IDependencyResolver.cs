namespace SharedAbstractions.DI
{
    public interface IDependencyResolver
    {
        THandler Resolve<THandler>();

        IEnumerable<THandler> ResolveMany<THandler>();
    }
}