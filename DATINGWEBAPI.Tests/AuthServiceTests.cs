using System.Threading.Tasks;
using AutoMapper;
using DATINGWEBAPI.BAL.Services;
using DATINGWEBAPI.BLL.Utilities.CustomExceptions;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using DATINGWEBAPI.DTO.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Moq;
using NUnit.Framework;

namespace DATINGWEBAPI.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<IUserRepository> _userRepoMock;
        private Mock<IVerificationCodeRepository> _verificationRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IStringLocalizer<AuthService>> _localizerMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _localizerMock = new Mock<IStringLocalizer<AuthService>>();
            _configurationMock = new Mock<IConfiguration>();
            _userRepoMock = new Mock<IUserRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _verificationRepoMock = new Mock<IVerificationCodeRepository>();

            _authService = new AuthService(
                _mapperMock.Object,
                _localizerMock.Object,
                _configurationMock.Object,
                _userRepoMock.Object,
                _httpContextAccessorMock.Object,
                _verificationRepoMock.Object
            );
        }

        [Test]
        public async Task RequestOTP_NewUser_ShouldCreateUserAndReturnOTP()
        {
            string mobile = "9000000001".ToString();
            var request = new RequestOTPDTO { MobileNumber = mobile };

           // _userRepoMock.Setup(r => r.GetUserByMobileAsync(mobile))
                        // .ReturnsAsync((USER)null);

            _userRepoMock.Setup(r => r.AddNewUser(It.IsAny<USER>()))
                         .Callback<USER>(u => u.USERID = 19)
                         .ReturnsAsync((USER u) => u);

            _verificationRepoMock.Setup(v => v.saveVerificationCode(It.IsAny<VERIFICATIONCODE>()))
                      .ReturnsAsync((VERIFICATIONCODE code) => code);
            var result = await _authService.RequestOTP(request);
            Assert.IsNotNull(result);
            Assert.AreEqual(mobile, result.MobileNumber);
            Assert.AreNotEqual(0, result.OTP);
        }

        [Test]
        public void RequestOTP_BlockedUser_ShouldThrowUserBlockedException()
        {
            // user 1
            string mobile = "9000000001".ToString();
            var blockedUser = new USER { USERID = 19, PHONENUMBER = mobile, ISBLOCKED = true };
            // user 2
            string mobile2 = "7048963056".ToString();
            var blockedUser2 = new USER { USERID = 31, PHONENUMBER = mobile2, ISBLOCKED = true };



            _userRepoMock.Setup(r => r.GetUserByMobileAsync(mobile2))
                         .ReturnsAsync(blockedUser2);

            _localizerMock.Setup(l => l[It.IsAny<string>()])
                          .Returns(new LocalizedString("BlockedUser", "User is blocked."));

            var ex = Assert.ThrowsAsync<UserBlockedException>(() =>
                _authService.RequestOTP(new RequestOTPDTO { MobileNumber = mobile }));

            Assert.AreEqual("User is blocked.", ex.Message);
        }

        [Test]
        public void RequestOTP_InvalidUser_ShouldThrowUserNotFoundException()
        {
            string mobile = "9000000001".ToString();

            _userRepoMock.Setup(r => r.GetUserByMobileAsync(mobile))
                         .ReturnsAsync((USER)null);

            _localizerMock.Setup(l => l[It.IsAny<string>()])
                          .Returns(new LocalizedString("InvalidUser", "User not found."));

            var ex = Assert.ThrowsAsync<UserNotFoundException>(() =>
                _authService.RequestOTP(new RequestOTPDTO { MobileNumber = mobile }));

            Assert.AreEqual("User not found.", ex.Message);
        }


    }
}
