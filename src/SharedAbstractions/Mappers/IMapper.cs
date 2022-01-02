namespace SharedAbstractions.Mappers
{
    public interface IMapper<in TModelIn, TResult>
    {
        TResult Map(TModelIn model);
    }
}