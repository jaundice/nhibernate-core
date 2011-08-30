namespace NHibernateClient
{
    public interface IFutureValue<T>
    {
        T Value { get; }
    }
}