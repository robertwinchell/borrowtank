namespace ASOL.HireThings.Model
{
    public interface IPredicate
    {
        string Serialize(bool escapeSQL = false);
    }
}
