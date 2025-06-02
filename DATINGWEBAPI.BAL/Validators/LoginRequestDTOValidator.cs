using DATINGWEBAPI.BAL.Utilities.Common;
using FluentValidation;
using Microsoft.Extensions.Localization;
using DATINGWEBAPI.DTO.RequestDTO;
namespace DATINGWEBAPI.BAL.Validators
{
    public class LoginRequestDTOValidator : AbstractValidator<LoginRequestDTO>
    {
        private readonly IStringLocalizer<LoginRequestDTOValidator> _localizer;

        public LoginRequestDTOValidator(IStringLocalizer<LoginRequestDTOValidator> localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage(_localizer[ResponseMessage.InvalidMobileNumber.ToString()])
                .Matches(@"^\d{10}$").WithMessage(_localizer[ResponseMessage.InvalidMobileNumber.ToString()]);

            RuleFor(x => x.OTP)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(_localizer[ResponseMessage.InvalidOTP.ToString()])
                .Matches(@"^\d{4}$").WithMessage(_localizer[ResponseMessage.InvalidOTP.ToString()])
                .When(x => !string.IsNullOrEmpty(x.OTP));
        }
    }
}
