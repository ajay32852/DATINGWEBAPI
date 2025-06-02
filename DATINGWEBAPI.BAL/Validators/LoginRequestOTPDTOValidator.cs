using DATINGWEBAPI.BAL.Utilities.Common;
using FluentValidation;
using Microsoft.Extensions.Localization;
using DATINGWEBAPI.DTO.DTOs;

namespace DATINGWEBAPI.BAL.Validators
{
    public class LoginRequestOTPDTOValidator : AbstractValidator<RequestOTPDTO>
    {
        private readonly IStringLocalizer<RequestOTPDTO> _localizer;

        public LoginRequestOTPDTOValidator(IStringLocalizer<RequestOTPDTO> localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage(_localizer[ResponseMessage.InvalidMobileNumber.ToString()])
                .Matches(@"^\d{10}$").WithMessage(_localizer[ResponseMessage.InvalidMobileNumber.ToString()]);

        }
    }
}
