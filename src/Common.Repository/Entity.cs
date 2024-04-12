namespace Common.Repository;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? UpdatedDate { get; protected set; }
    public int Version { get; protected set; }
}
