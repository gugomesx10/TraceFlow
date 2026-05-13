using FluentAssertions;
using TraceFlowTraining.Infrastructure.Security;
using Xunit;

namespace TraceFlowTraining.Tests.Services;

public class PasswordServiceTests
{
    private readonly PasswordService _passwordService;

    public PasswordServiceTests()
    {
        _passwordService = new PasswordService();
    }

    [Fact]
    public void HashPassword_ShouldGenerateDifferentHash()
    {
        // Arrange
        var password = "123456";

        // Act
        var hash = _passwordService.HashPassword(password);

        // Assert
        hash.Should().NotBeNullOrEmpty();

        hash.Should().NotBe(password);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        // Arrange
        var password = "123456";

        var hash = _passwordService.HashPassword(password);

        // Act
        var result = _passwordService.VerifyPassword(password, hash);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_WhenPasswordIsInvalid()
    {
        // Arrange
        var password = "123456";

        var hash = _passwordService.HashPassword(password);

        // Act
        var result = _passwordService.VerifyPassword("wrong-password", hash);

        // Assert
        result.Should().BeFalse();
    }
}