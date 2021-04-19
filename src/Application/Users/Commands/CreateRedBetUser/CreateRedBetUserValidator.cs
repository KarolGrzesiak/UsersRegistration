using FluentValidation;

namespace Application.Users.Commands.CreateRedBetUser
{
    public class CreateRedBetUserValidator : AbstractValidator<CreateRedBetUser>
    {

        public CreateRedBetUserValidator()
        {
            RuleFor(u=>u.FirstName)
                .MaximumLength(200).WithMessage($"{nameof(CreateRedBetUser.FirstName)} must not exceed 200 characters")
                .NotEmpty().WithMessage($"{nameof(CreateRedBetUser.FirstName)} is required");
            RuleFor(u=>u.LastName)
                .MaximumLength(200).WithMessage($"{nameof(CreateRedBetUser.LastName)} must not exceed 200 characters")
                .NotEmpty().WithMessage($"{nameof(CreateRedBetUser.LastName)} is required");
            RuleFor(u=>u.Street)
                .NotEmpty().WithMessage($"{nameof(CreateRedBetUser.Street)} is required");
            RuleFor(u=>u.BuildingNumber)
                .NotEmpty().WithMessage($"{nameof(CreateRedBetUser.BuildingNumber)} is required");
            RuleFor(u=>u.ZipCode)
                .NotEmpty().WithMessage($"{nameof(CreateRedBetUser.ZipCode)} is required");
            RuleFor(u=>u.FavouriteFootballTeam)
                .NotEmpty().WithMessage($"{nameof(CreateRedBetUser.FavouriteFootballTeam)} is required");
        }
    }
}