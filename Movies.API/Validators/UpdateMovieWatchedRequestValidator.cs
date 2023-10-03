using FluentValidation;
using Movies.API.Requests;

namespace Movies.API.Validators;

public class UpdateMovieWatchedRequestValidator : AbstractValidator<UpdateMovieWatchedRequest>
{
	private const int InvalidId = 0;
	public UpdateMovieWatchedRequestValidator()
	{
		RuleFor(request => request.MovieId).NotEqual(InvalidId)
            .WithMessage("Provided MovieID is not valid.");
	}
}
