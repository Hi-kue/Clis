using System.Text;
using Spectre.Console;

namespace Passman.Config;

public class PasswordGen
{
    private string PasswordId { get; set; }
    private string UserId { get; set; }
    private string Password { get; set; }
    private string PasswordSalt { get; set; }
    private string PasswordHash { get; set; }
    private Int64 ResetToken { get; set; }
    private DateTime DateCreated { get; set; }

    // Temporary List of Passwords (Local Storage)
    private readonly List<PasswordGen> _passwordsList = new();

    public PasswordGen(string passwordId, string userId, string password)
    {
        PasswordId = passwordId;
        UserId = userId;
        Password = password;
        PasswordSalt = new Hash().GenerateSalt();
        PasswordHash = new Hash().GenerateHash(password, PasswordSalt);
        ResetToken = new Random().Next();
        DateCreated = DateTime.Now;
        
        _passwordsList.Add(this);
    }

    public PasswordGen() { }
    
    public void StorePassword(string passwordId, string userId, string password)
    {
        // Create Password Object / Store Password in Local Storage
        var passwordForStorage = new PasswordGen(passwordId, userId, password);
        
        // Store Password in Database
    }

    // TODO: Edit this Function 
    public string GeneratePassword(PasswordType type) => type switch
    {
        PasswordType.Lowercase => new PasswordGenerator().IncludeLowercase().LengthOf(10).Next(),
        PasswordType.Uppercase => new PasswordGenerator().IncludeUppercase().LengthOf(10).Next(),
        PasswordType.Numeric => new PasswordGenerator().IncludeNumeric().LengthOf(10).Next(),
        PasswordType.Special => new PasswordGenerator().IncludeSpecial().LengthOf(10).Next(),
        PasswordType.All => new PasswordGenerator().IncludeLowercase().IncludeUppercase().IncludeNumeric()
            .IncludeSpecial().LengthOf(10).Next(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
    
    public void RetrievePassword(string findId)
    {
        // Find Password in Local Storage
        var password = _passwordsList.Find(p => p.PasswordId.Equals(findId));
        
        // Display Password
        AnsiConsole.MarkupLine($"[green]Password Found: {password}[/]");
    }

    public override string ToString()
    {
        return $"""
                PasswordId: {PasswordId}
                UserId: {UserId}
                Password: {Password}
                PasswordSalt: {PasswordSalt}
                PasswordHash: {PasswordHash}
                ResetToken: {ResetToken}
                DateCreated: {DateCreated}
                """;
    }
}

// TODO: Update this Class
internal class PasswordGenerator
{
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numeric = "0123456789";
    private const string Special = @"!#$%&*@\";
    private const string All = Lowercase + Uppercase + Numeric + Special;

    private readonly Random _random = new();

    private bool _includeLowercase;
    private bool _includeUppercase;
    private bool _includeNumeric;
    private bool _includeSpecial;
    private int _length;

    public PasswordGenerator IncludeLowercase()
    {
        _includeLowercase = true;
        return this;
    }

    public PasswordGenerator IncludeUppercase()
    {
        _includeUppercase = true;
        return this;
    }

    public PasswordGenerator IncludeNumeric()
    {
        _includeNumeric = true;
        return this;
    }

    public PasswordGenerator IncludeSpecial()
    {
        _includeSpecial = true;
        return this;
    }

    public PasswordGenerator LengthOf(int length)
    {
        _length = length;
        return this;
    }

    public string Next()
    {
        var charSet = All;

        if (!_includeLowercase)
            charSet = charSet.Replace(Lowercase, string.Empty);

        if (!_includeUppercase)
            charSet = charSet.Replace(Uppercase, string.Empty);

        if (!_includeNumeric)
            charSet = charSet.Replace(Numeric, string.Empty);

        if (!_includeSpecial)
            charSet = charSet.Replace(Special, string.Empty);

        var password = new StringBuilder();
        for (var i = 0; i < _length; i++)
        {
            var index = _random.Next(0, charSet.Length);
            password.Append(charSet[index]);
        }

        return password.ToString();
    }
}