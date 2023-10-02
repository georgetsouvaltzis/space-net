using Movies.Domain.Entities;

namespace Movies.Domain.Repositories
{
    public interface IMoviesRepository
    {
        void AddBulk(List<Movie> movies);
    }
}
