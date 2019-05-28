using currencyconverter.AuthorizationModule;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class LoginValidatorTest
    {
        private IValidator _loginValidator;

        [SetUp]
        public void SetUp()
        {
            _loginValidator = new LoginValidator();
        }

        [Test]
        public void ValidateTest()
        {
            //Given
            var login = "login";
            
            //When
            var actual = _loginValidator.Validate(login);

            //Then
            Assert.IsTrue(actual);
        }

        [TestCase("")]
        [TestCase(null)]
        public void LoginIsEmptyOrNullTest(string login)
        {
            //When
            var actual = _loginValidator.Validate(login);

            //Then
            Assert.IsFalse(actual);
        }
    }
}