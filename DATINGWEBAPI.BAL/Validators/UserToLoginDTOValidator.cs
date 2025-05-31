using DATINGWEBAPI.BAL.Utilities.Common;
using FluentValidation;
using Microsoft.Extensions.Localization;
using DATINGWEBAPI.DTO.DTOs;
namespace DATINGWEBAPI.BAL.Validators
{
    public class UserToLoginDTOValidator : AbstractValidator<UserDTO>
    {
        private readonly IStringLocalizer<UserToLoginDTOValidator> _localizer;
        public UserToLoginDTOValidator(IStringLocalizer<UserToLoginDTOValidator> localizer)
        {
            _localizer = localizer;
            RuleFor(x => x.Email).EmailAddress().WithMessage(_localizer[name: ResponseMessage.InvalidUsername.ToString()]);
            RuleFor(x => x.PasswordHash).NotEmpty().MinimumLength(12).WithMessage(_localizer[name: ResponseMessage.InvalidPasswordLength.ToString()]);

        }
    }
}
