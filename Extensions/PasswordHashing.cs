using Isopoh.Cryptography.Argon2;

namespace UniVerServer.Extensions;

public static class PasswordHashing
{
    public static string HashPassword(this string password) =>  Argon2.Hash(password);

    public static bool VerifyPassword(this string enteredPassword, string userPassword) =>
        Argon2.Verify(enteredPassword, userPassword);
}