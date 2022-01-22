using Extensions;
using Models;
using Xunit;

public class PasswordHasherTests
{
    [Fact]
    public void HashPassword_ValidateSamePassword_ShouldReturnTrue()
    {
        //Arrange data
        User user = new User();
        string passOne = "nickIsCool";
        string passTwo = "nickIsCool";

        //Act
        PasswordHasher pH = new PasswordHasher();
        string hashedPassOne = pH.HashPassword(passOne);
        string hashedPassTwo = pH.HashPassword(passTwo);
        var result = pH.VerifyHashedPassword(user, hashedPassTwo, passOne);

        //Assert
        Assert.True(result == PasswordHasher.PasswordVerificationResult.Success);
    }
}