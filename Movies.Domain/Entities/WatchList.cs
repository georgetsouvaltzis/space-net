namespace Movies.Domain.Entities;

public class WatchList : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<Movie> Movies { get; set; }
}
