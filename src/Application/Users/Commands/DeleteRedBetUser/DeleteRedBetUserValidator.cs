using FluentValidation;

namespace Application.Users.Commands.DeleteRedBetUser
{
    public class DeleteRedBetUserValidator : AbstractValidator<DeleteRedBetUser>
    {
        public DeleteRedBetUserValidator()
        {
            RuleFor(u=>u.Id)
                .NotEmpty().WithMessage($"{nameof(DeleteRedBetUser.Id)} is required");
        }
    }
}