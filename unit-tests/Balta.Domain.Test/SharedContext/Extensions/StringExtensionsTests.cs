using Balta.Domain.SharedContext.Extensions;
using System.Text;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    [Fact]
    public void ShouldGenerateBase64FromString()
    {
        // Arrange
        string text = "m1nh0k";
        string expectedBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(text));

        // Act
        var converted = text.ToBase64();

        // Assert
        Assert.Equal(expectedBase64, converted);
    }
}