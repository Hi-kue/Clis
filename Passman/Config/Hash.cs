using System.Security.Cryptography;

namespace Passman.Config;

/// <summary>
/// Purpose of this Class
/// - Hash Passwords;
/// - Salt Passwords;
/// - Validate Passwords w/ Hash;
/// </summary>
public class Hash
{
    /// <summary>
    /// Hash and Salt Size
    /// </summary>
    private const int SaltSize = 16; // 128 Bits
    private const int HashSize = 32; // 256 Bits
    
    /// <summary>
    /// Pre Defined Iteration Count
    /// </summary>
    private const int Iterations = 10000;
    
    /// <summary>
    /// Pre Defined Hash Algorithm Name
    /// </summary>
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA256;
    private const char SegmentDelimiter = ':';

    /// <summary>
    /// Generates a Salt for the Password
    /// </summary>
    /// <returns></returns>
    public string GenerateSalt()
    {
        var rng = RandomNumberGenerator.GetBytes(SaltSize); 
        return Convert.ToHexString(rng);
    }

    /// <summary>
    /// Returns a Hashed Password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public string GenerateHash(string password, string salt)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            Convert.FromHexString(salt),
            Iterations,
            HashAlgorithmName,
            HashSize
        );

        return string.Join(
            "$SHA256$V1$",
            SegmentDelimiter,
            Convert.ToHexString(hash),
            salt,
            Iterations,
            HashAlgorithmName
        );
    }

    /// <summary>
    /// Generates a Single Hash of a Password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string GenerateSingleHash(string password)
    {
        var salt = GenerateSalt();
        var hash = GenerateHash(password, salt);
        return hash;
    }

    /// <summary>
    /// Check if Hash is Supported
    /// </summary>
    /// <param name="hash"></param>
    /// <returns></returns>
    public bool IsHashSupported(string hash)
    {
        return hash.Contains("$SHA256$V1$"); // $MYHASH$V1$
    }
    
    /// <summary>
    /// Validates the Password Hash w/ the Password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public bool Validate(string password, string hashedPassword)
    {
        if (!IsHashSupported(hashedPassword)) throw new NotSupportedException("The Hash Type is not Supported");

        var segments = hashedPassword.Split(SegmentDelimiter);
        var hash = Convert.FromHexString(segments[0]);
        var salt = Convert.FromHexString(segments[1]);
        var iterations = int.Parse(segments[3]);
        var hashAlgorithmName = new HashAlgorithmName(segments[4]);

        var newHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            hashAlgorithmName,
            hash.Length
        );

        return CryptographicOperations.FixedTimeEquals(newHash, hash);
    }
}