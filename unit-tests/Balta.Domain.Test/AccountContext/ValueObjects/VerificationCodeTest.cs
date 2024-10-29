using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.Test.Mocks;
using System.Net;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class VerificationCodeTest
{
    [Fact]
    public void ShouldGenerateVerificationCode()
    {
        // Act
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());
        
        // Assert
        Assert.NotNull(generated);
    }

    [Fact]
    public void ShouldGenerateExpiresAtInFuture()
    {
        // Act
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());

        // Assert
        Assert.True(generated.ExpiresAtUtc > DateTime.UtcNow);
    }

    [Fact]
    public void ShouldGenerateVerifiedAtAsNull()
    {
        // Act
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());

        // Assert
        Assert.Null(generated.VerifiedAtUtc);
    }

    [Fact]
    public void ShouldBeActiveWhenCreated()
    {
        // Act
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());

        // Assert
        Assert.True(generated.IsActive);
    }

    [Fact]
    public void ShouldFailIfExpired()
    {
        // Arrange
        var generated = VerificationCode.ShouldCreate(new MockYesterdayDateTimeProvider());

        // Act / Assert
        Assert.Throws<InvalidVerificationCodeException>(() =>
            generated.ShouldVerify(generated.Code));
    }

    [Fact]
    public void ShouldFailIfCodeIsInvalid()
    {
        // Arrange
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());

        // Act / Assert
        Assert.Throws<InvalidVerificationCodeException>(() =>
            generated.ShouldVerify("outroCodigo"));
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    public void ShouldFailIfCodeIsLessThanSixChars(string code)
    {
        // Arrange
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());

        // Act / Assert
        Assert.Throws<InvalidVerificationCodeException>(() =>
            generated.ShouldVerify(code));
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("12345678")]
    [InlineData("123456789")]
    public void ShouldFailIfCodeIsGreaterThanSixChars(string code)
    {
        // Arrange
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());

        // Act / Assert
        Assert.Throws<InvalidVerificationCodeException>(() =>
            generated.ShouldVerify(code));
    }

    [Fact]
    public void ShouldFailIfIsNotActive()
    {
        // Arrange
        var generated = VerificationCode.ShouldCreate(new MockYesterdayDateTimeProvider());

        // Act / Assert
        Assert.False(generated.IsActive);
        Assert.Throws<InvalidVerificationCodeException>(() =>
            generated.ShouldVerify(generated.Code));
    }

    [Fact]
    public void ShouldFailIfIsAlreadyVerified()
    {
        // Arrange
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());
        generated.ShouldVerify(generated.Code);

        // Act / Assert
        Assert.Throws<InvalidVerificationCodeException>(() =>
            generated.ShouldVerify(generated.Code));
    }

    [Fact]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch()
    {
        // Arrange
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());

        // Act / Assert
        Assert.Throws<InvalidVerificationCodeException>(() =>
            generated.ShouldVerify("outroCodigo"));
    }

    [Fact]
    public void ShouldVerify()
    {
        // Arrange
        var generated = VerificationCode.ShouldCreate(new MockDateTimeProvider());

        // Act / Assert
        generated.ShouldVerify(generated.Code);
        
        Assert.True(generated.IsVerified);
    }
}