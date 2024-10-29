using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.Test.Mocks;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {
        // Arrange
        string plainText = null;

        // Act / Assert
        Assert.Throws<InvalidPasswordException>(() =>
            Password.ShouldCreate(plainText));
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {
        // Arrange
        string plainText = string.Empty;

        // Act / Assert
        Assert.Throws<InvalidPasswordException>(() =>
            Password.ShouldCreate(plainText));
    }

    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        // Arrange
        string plainText = " ";

        // Act / Assert
        Assert.Throws<InvalidPasswordException>(() =>
            Password.ShouldCreate(plainText));
    }


    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("1234567")]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars(string plainText)
    {
        // Act / Assert
        Assert.Throws<InvalidPasswordException>(() =>
            Password.ShouldCreate(plainText));
    }

    [Theory]
    [InlineData("0123456789012345678901234567890123456789012345678")]
    [InlineData("01234567890123456789012345678901234567890123456789")]
    [InlineData("012345678901234567890123456789012345678901234567890")]
    [InlineData("0123456789012345678901234567890123456789012345678901")]
    [InlineData("01234567890123456789012345678901234567890123456789012")]
    [InlineData("012345678901234567890123456789012345678901234567890123")]
    [InlineData("0123456789012345678901234567890123456789012345678901234")]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars(string plainText)
    {
        // Act / Assert
        Assert.Throws<InvalidPasswordException>(() =>
            Password.ShouldCreate(plainText));
    }

    [Fact]
    public void ShouldHashPassword()
    {
        // Arrange
        string plainText = "çlkadhfkajhdsfakjsad";

        // Act
        var pwd = Password.ShouldCreate(plainText);

        // Assert
        Assert.NotEqual("", pwd.Hash);
    }
    
    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        // Arrange
        string plainText = "çlkadhfkajhdsfakjsad";
        var pwd = Password.ShouldCreate(plainText);
        string expectedHash = pwd.Hash;

        // Act
        var mached = Password.ShouldMatch(expectedHash, plainText);

        // Assert
        Assert.True(mached);
    }
    
    [Fact]
    public void ShouldGenerateStrongPassword()
    {
        // Arrange
        var specials = "!@#$%ˆ&*(){}[];".ToList();

        // Act
        var generated = Password.ShouldGenerate(includeSpecialChars: true);


        // Assert
        Assert.True(specials.Any(special => generated.Contains(special)));
    }

    [Fact]
    public void ShouldImplicitConvertToString()
    {
        // Arrange
        string plainText = "çlkadhfkajhdsfakjsad";

        // Act
        var pwd = Password.ShouldCreate(plainText);

        string strPwd = pwd;

        // Assert
        Assert.Equal(pwd, strPwd);
    }

    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        // Arrange
        string plainText = "çlkadhfkajhdsfakjsad";

        // Act
        var pwd = Password.ShouldCreate(plainText);

        string strPwd = pwd.ToString();

        // Assert
        Assert.Equal(pwd.Hash, strPwd);
    }

    [Fact]
    public void ShouldMarkPasswordAsExpired()
    {
        // Arrange
        string plainText = "çlkadhfkajhdsfakjsad";

        // Act
        var pwd = Password.ShouldCreate(plainText, expiresAtUtc: DateTime.UtcNow.AddSeconds(-1));

        // Assert
        Assert.True(pwd.IsExpired());
    }

    [Fact]
    public void ShouldFailIfPasswordIsExpired()
    {
        // Arrange
        string plainText = "çlkadhfkajhdsfakjsad";
        var pwd = Password.ShouldCreate(plainText, expiresAtUtc: DateTime.UtcNow.AddSeconds(-1));

        // Act
        var mached = Password.ShouldMatch(pwd.Hash, plainText);

        // Assert
        Assert.False(mached);
    }
    
    [Fact]
    public void ShouldMarkPasswordAsMustChange()
    {
        // Arrange
        string plainText = "çlkadhfkajhdsfakjsad";

        // Act
        var pwd = Password.ShouldCreate(plainText, mustChange: true);

        // Assert
        Assert.True(pwd.MustChange);
    }

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange()
    {
        // Arrange
        string plainText = "çlkadhfkajhdsfakjsad";
        var pwd = Password.ShouldCreate(plainText, mustChange: true);

        // Act
        var mached = Password.ShouldMatch(pwd.Hash, plainText);

        // Assert
        Assert.False(mached);
    }
}