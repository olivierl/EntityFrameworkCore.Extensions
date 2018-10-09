using Xunit;

namespace EntityFrameworkCore.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void ToSnakeCase_ReturnLowerCaseAndUnderscore()
        {
            const string inputText = "ThisIsSomeText";

            var result = inputText.ToSnakeCase();

            Assert.Equal("this_is_some_text", result);
        }

        [Fact]
        public void ToSnakeCase_ReturnSameTextWhenAlreadySnakeCase()
        {
            const string inputText = "this_is_some_text";

            var result = inputText.ToSnakeCase();

            Assert.Equal("this_is_some_text", result);
        }
    }
}
