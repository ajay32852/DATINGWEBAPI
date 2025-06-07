using DATINGWEBAPI.DTO.RequestDTO;
using FluentValidation;

namespace DATINGWEBAPI.BAL.Validators
{
    public class UpdateUserProfileRequestValidator : AbstractValidator<UpdateUserProfileRequestDTO>
    {
        public UpdateUserProfileRequestValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(50);
            RuleFor(x => x.LastName).MaximumLength(50);
            RuleFor(x => x.Bio).MaximumLength(500);
            // Add more as needed
        }
    }

}
