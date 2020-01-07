namespace SOLID.Samples.OCP.After
{
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
}
