namespace ASOL.HireThings.Model
{
    public interface IPredicateBase<T>
    {
        string Name { get; }
        T Value { get; }
        Operator Operator { get; }
    }
}
