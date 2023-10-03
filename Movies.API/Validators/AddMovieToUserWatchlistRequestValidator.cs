using FluentValidation;
using Movies.API.Requests;

namespace Movies.API.Validators;

public class AddMovieToUserWatchlistRequestValidator : AbstractValidator<AddMovieToUserWatchlistRequest>
{
	private const int InvalidId = 0;
	public AddMovieToUserWatchlistRequestValidator()
	{
		RuleFor(request => request.MovieId).NotEqual(InvalidId)
			.WithMessage("Provided MovieID is not valid.");
	}
}
