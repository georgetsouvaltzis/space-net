using MediatR;
using System.Reflection;

namespace Movies.Application.Commands
{
    public class AddMovieToUserWatchlistCommand : IRequest
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
}
