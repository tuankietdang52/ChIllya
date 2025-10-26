namespace ChIllya.Models.Mapper
{
    public interface IMapper<TSource, TResult>
    {
        TResult Map(TSource source);
    }
}
