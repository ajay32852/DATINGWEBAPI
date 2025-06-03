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

        }

    }
}
