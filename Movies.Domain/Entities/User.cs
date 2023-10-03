namespace Movies.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public WatchList WatchList { get; set; }

    public int WatchListId { get; set; }
}
