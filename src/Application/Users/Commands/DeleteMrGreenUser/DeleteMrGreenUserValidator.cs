using FluentValidation;

namespace Application.Users.Commands.DeleteMrGreenUser
{
    public class DeleteMrGreenUserValidator : AbstractValidator<DeleteMrGreenUser>
    {
        public DeleteMrGreenUserValidator()
        {
            RuleFor(u=>u.Id)
                .NotEmpty().WithMessage($"{nameof(DeleteMrGreenUser.Id)} is required");
        }
    }
}