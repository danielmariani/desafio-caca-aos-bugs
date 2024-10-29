using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.Test.Mocks;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    [Fact]
    public void ShouldLowerCaseEmail()
    {
        // Arrange
        string address = "M1NH0K@BALTA.IO";
        string expected = "m1nh0k@balta.io";

        // Act
        var email = Email.ShouldCreate(address, new MockDateTimeProvider());

        // Assert
        Assert.Equal(expected, email.Address);
    }
    
    [Fact]
    public void ShouldTrimEmail()
    {
        // Arrange
        string address = " m1nh0k@balta.io ";
        string expected = "m1nh0k@balta.io";

        // Act
        var email = Email.ShouldCreate(address, new MockDateTimeProvider());

        // Assert
        Assert.Equal(expected, email.Address);
    }

    [Fact]
    public void ShouldFailIfEmailIsNull()
    {
        // Arrange
        string address = null;

        // Act / Assert
        Assert.Throws<NullReferenceException>(() =>
            Email.ShouldCreate(address, new MockDateTimeProvider()));
    }

    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {
        // Arrange
        string address = string.Empty;

        // Act / Assert
        Assert.Throws<InvalidEmailException>(() =>
            Email.ShouldCreate(address, new MockDateTimeProvider()));
    }

    [Theory]
    [InlineData("emailSemArroba")]
    [InlineData("@sufixo")]
    [InlineData("prefixo@")]
    [InlineData("@duploarroba@")]
    [InlineData("email com espaço@gmail.com")]
    public void ShouldFailIfEmailIsInvalid(string address)
    {
        // Act / Assert
        Assert.Throws<InvalidEmailException>(() =>
            Email.ShouldCreate(address, new MockDateTimeProvider()));
    }

    [Fact]
    public void ShouldPassIfEmailIsValid()
    {
        // Arrange
        string address = "emailvalido@balta.io";

        // Act
        var email = Email.ShouldCreate(address, new MockDateTimeProvider());

        // Assert
        Assert.Equal(address, email.Address);
    }

    [Fact]
    public void ShouldHashEmailAddress()
    {
        // Arrange
        string address = "emailvalido@balta.io";
        var expectedHash = Convert.ToBase64String(Encoding.ASCII.GetBytes(address));

        // Act
        var email = Email.ShouldCreate(address, new MockDateTimeProvider());

        // Assert
        Assert.Equal(expectedHash, email.Hash);
    }
    
    [Fact]
    public void ShouldExplicitConvertFromString()
    {
        // Arrange
        string address = "emailvalido@balta.io";

        // Act
        Email email = (Email)address;

        string strEmail = email;

        // Assert
        Assert.Equal(address, strEmail);
    }

    [Fact]
    public void ShouldExplicitConvertToString()
    {
        // Arrange
        string address = "emailvalido@balta.io";

        // Act
        var email = Email.ShouldCreate(address, new MockDateTimeProvider());

        string strEmail = (string)email;

        // Assert
        Assert.Equal(address, strEmail);
    }

    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod()
    {
        // Arrange
        string address = "emailvalido@balta.io";

        // Act
        var email = Email.ShouldCreate(address, new MockDateTimeProvider());

        string strEmail = email.ToString();

        // Assert
        Assert.Equal(address, strEmail);
    }
}