using MediatR;
using Movies.Application.Commands;
using Movies.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Handlers
{
    public class UpdateMovieWatchedCommandHandler : IRequestHandler<UpdateMovieWatchedCommand>
    {
        private readonly IUserRepository _userRepository;
        public UpdateMovieWatchedCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<Unit> Handle(UpdateMovieWatchedCommand request, CancellationToken cancellationToken)
        {
            _userRepository.
            throw new NotImplementedException();
        }
    }
}
