namespace Wings.Domain.Entities
{
    public interface IHasConcurrencyStamp
    {
        string ConcurrencyStamp { get; set; }
    }
}
