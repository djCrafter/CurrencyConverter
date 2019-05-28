using currencyconverter.AuthorizationModule;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class PasswordValidatorTest
    {
        private IValidator _passwordValidator;

        [SetUp]
        public void Setup()
        {
            _passwordValidator = new PasswordValidator();
        }

        [Test]
        public void PasswordValidateTest()
        {
            //Given
            var password = "password";

            //When
            var actual = _passwordValidator.Validate(password);

            //Then
            Assert.IsTrue(actual);
        }

        [TestCase("")]
        [TestCase(null)]
        public void PasswordNullOrEmptyTest(string password)
        {
            //When
            var actual = _passwordValidator.Validate(password);

            //Then 
            Assert.IsFalse(actual);
        }

    }
}
