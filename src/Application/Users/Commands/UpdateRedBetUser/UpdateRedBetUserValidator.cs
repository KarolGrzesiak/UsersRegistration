using FluentValidation;

namespace Application.Users.Commands.UpdateRedBetUser
{
    public class UpdateRedBetUserValidator : AbstractValidator<UpdateRedBetUser>
    {
        public UpdateRedBetUserValidator()
        {
            RuleFor(u=>u.Id)
                .NotEmpty().WithMessage($"{nameof(UpdateRedBetUser.Id)} is required");
            RuleFor(u=>u.FirstName)
                .MaximumLength(200).WithMessage($"{nameof(UpdateRedBetUser.FirstName)} must not exceed 200 characters")
                .NotEmpty().WithMessage($"{nameof(UpdateRedBetUser.FirstName)} is required");
            RuleFor(u=>u.LastName)
                .MaximumLength(200).WithMessage($"{nameof(UpdateRedBetUser.LastName)} must not exceed 200 characters")
                .NotEmpty().WithMessage($"{nameof(UpdateRedBetUser.LastName)} is required");
            RuleFor(u=>u.Street)
                .NotEmpty().WithMessage($"{nameof(UpdateRedBetUser.Street)} is required");
            RuleFor(u=>u.BuildingNumber)
                .NotEmpty().WithMessage($"{nameof(UpdateRedBetUser.BuildingNumber)} is required");
            RuleFor(u=>u.ZipCode)
                .NotEmpty().WithMessage($"{nameof(UpdateRedBetUser.ZipCode)} is required");
            RuleFor(u=>u.FavouriteFootballTeam)
                .NotEmpty().WithMessage($"{nameof(UpdateRedBetUser.FavouriteFootballTeam)} is required");

        }
    }
}