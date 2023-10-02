using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Commands
{
    public class UpdateMovieWatchedCommand : IRequest
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
}
