using FluentValidation;

namespace DiscordLite.Features.Authentication.Refresh;

public sealed class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    { 
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
