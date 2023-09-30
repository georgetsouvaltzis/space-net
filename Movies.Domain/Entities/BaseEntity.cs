namespace Movies.Domain.Entities;

public abstract class BaseEntity
{
	public BaseEntity()
	{
		CreatedAt = DateTime.Now;
	}
    public int Id { get; set; }

	public DateTime CreatedAt { get; }
}
