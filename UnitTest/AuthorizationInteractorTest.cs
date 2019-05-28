using System;
using System.Reflection;
using System.Threading.Tasks;
using currencyconverter.AuthorizationModule;
using Moq;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class AuthorizationInteractorTest
    {
        private IAuthorizationInteractor _interactor;
        private Mock<IAuthSender> _sender;
        private Mock<IValidator> _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<IValidator>(MockBehavior.Strict);
            _sender = new Mock<IAuthSender>(MockBehavior.Strict);
            _interactor = new AuthorizationInteractor(_validator.Object, _sender.Object);
        }

        [Test]
        public void CtorLoginValidatorTest()
        {
            var expected =
                typeof(AuthorizationInteractor)
                    .GetField("_loginValidator",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(_interactor);
            Assert.AreEqual(_validator.Object, expected);
        }
        
        [Test]
        public void CtorLAuthSenderTest()
        {
            var expected =
                typeof(AuthorizationInteractor)
                    .GetField("_authSender",
                        BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(_interactor);
            Assert.AreEqual(_sender.Object, expected);
        }
        
        [Test]
        public void CtorLoginValidatorNullTest()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                _interactor = new AuthorizationInteractor(null, _sender.Object);
            });
            
            Assert.AreEqual("loginValidator", exception.ParamName);
        }
        
        [Test]
        public void CtorAuthSenderNullTest()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                _interactor = new AuthorizationInteractor(_validator.Object, null);
            });
            
            Assert.AreEqual("authSender", exception.ParamName);
        }
        
        [TestCase(true, EAuthResult.Success)]
        [TestCase(false, EAuthResult.Unauthorized)]
        public async Task LoginTest(bool senderResult, EAuthResult expected)
        {
            //Given
            var login = "login";
            var pass = "pass";
            _validator.Setup(f => f.Validate(login))
                .Returns(true);
            _sender.Setup(f => f.SendAuthRequest(login, pass))
                .Returns(Task.FromResult(senderResult));
            
            //When
            var actual = await _interactor.Login(login, pass);

            //Then
            _validator.Verify(f => f.Validate(login), Times.Once);
            _sender.Verify(f => f.SendAuthRequest(login, pass));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task LoginTest_InvalidLogin()
        {
            //Given
            var login = "invalid_string";
            var pass = "pass";
            var expected = EAuthResult.InvalidData;
            _validator.Setup(f => f.Validate(login))
                .Returns(false);
            
            //When
            var actual = await _interactor.Login(login, pass);

            //Then
            _validator.Verify(f => f.Validate(login), Times.Once);
            Assert.AreEqual(expected, actual);
        }
    }
}