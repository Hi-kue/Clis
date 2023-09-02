namespace Passman.Config;

public class Init
{
    /*
     * Initializer for the Application
     * - Setup the Initial Application;
     * - Database Setup;
     * - Menu Setup;
     * - GUI Setup (Optional);
     * - Encryption Scheme Setup;
     * - Salt & Hash Functions Setup;
     * - Password Generator Setup;
     * - Password Storage Setup;
     * - Password Retrieval Setup;
     * - Password Encryption/Decryption Setup;
     */
    
    private string PasswordId { get; set; }
    private string UserId { get; set; }
    private string Password { get; set; }
    private string PasswordSalt { get; set; }
    private string PasswordHash { get; set; }
    private string ResetToken { get; set; }
    private string DateCreated { get; set; }

    public Init()
    {
        
    }
}