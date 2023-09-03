using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spectre.Console;

using Sharprompt;

namespace Passman.Config;

public class Menu
{
    public void DisplayMenu()
    {
        var selection = Prompt.Select<MenuOptions>("Select an option:");
        DetermineUserSelection(selection);
    }

    private void DetermineUserSelection(MenuOptions option)
    {
        var optionDisplay = option switch
        {
            MenuOptions.StorePassword => StorePasswordDetails(),
            MenuOptions.RetrievePassword => RetrievePasswordDetails(),
            MenuOptions.EncryptPassword => EncryptPassword(),
            MenuOptions.DisplayPasswords => DisplayPasswords(),
            MenuOptions.GeneratePassword => GenerateNewPassword(),
            MenuOptions.Exit => Environment.Exit(1),
            _ => NoValidOptionsSelected()
        };

        optionDisplay();
    }

    // TODO: Edit this Function
    private Action StorePasswordDetails()
    {
        return () =>
        {
            try
            {
                var passwordId = Prompt.Input<string>("Enter Password Id: ");
                var userId = Prompt.Input<string>("Enter User Id: ");
                var password = Prompt.Input<string>("Enter Password: ");

                if (passwordId.Equals(String.IsNullOrEmpty) || userId.Equals(String.IsNullOrEmpty) ||
                    password.Equals(String.IsNullOrEmpty))
                {
                    AnsiConsole.MarkupLine("[red]Invalid Input Provided[/]");
                    StorePasswordDetails();
                }

                var passwordForStorage = new PasswordGen(passwordId, userId, password);

                // TODO: Store Password in Database
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenTypes);
            }
        };
    }

    // TODO: Edit this Function
    private Action RetrievePasswordDetails()
    {
        return () =>
        {
            var passwordId = Prompt.Input<string>("Enter Password Id to Retrieve Password: ");
            var userId = Prompt.Input<string>("Enter User Id to Retrieve Password: ");

            var localOrDBPassword = Prompt.Select("Where would you like to search for the password? ", new[]
            {
                "Local",
                "Database"
            });

            if (!passwordId.Equals(String.Empty) || !userId.Equals(String.Empty))
            {
                switch (localOrDBPassword)
                {
                    case "Local":
                        new PasswordGen().RetrievePassword(passwordId);
                        break;
                    case "Database":
                        break;
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid Input Provided[/]");
                RetrievePasswordDetails();
            }
        };
    }

    // TODO: Edit this Function
    private Action EncryptPassword()
    {
        return () =>
        {
            var password = Prompt.Input<string>("Please enter a password to encrypt: ");

            if (password.Equals(String.Empty)) return;

            var encryptedPassword = new Hash().GenerateSingleHash(password);
            AnsiConsole.MarkupLine($"[green]Encrypted Password: {encryptedPassword}[/]");

            var response = Prompt.Input<string>("Would you like to continue? (Y/N/R): ");
            if (response.Equals("Y"))
            {
                DisplayMenu();
            }
            else
            {
                EncryptPassword();
            }
        };
    }

    // TODO: Edit this Function
    private Action DisplayPasswords()
    {
        return () =>
        {
            // TODO: Implement Password Display using Tables
        };
    }

    // TODO: Edit this Function
    private Action GenerateNewPassword()
    {
        return () =>
        {
            var passwordLength = Prompt.Input<int>("Enter Password Length: ");
            var password = new PasswordGen().GeneratePassword(PasswordType.All);
            AnsiConsole.MarkupLine($"[green]Generated Password: {password}[/]");

            var response = Prompt.Input<string>("Would you like to continue? (Y/N/R): ");
            if (response.Equals("Y"))
            {
                DisplayMenu();
            }
            else
            {
                GenerateNewPassword();
            }
        };
    }

private Action NoValidOptionsSelected()
    {
        return () =>
        {
            AnsiConsole.MarkupLine("[red]No Valid Options Selected[/]");
            DisplayMenu();
        };
    }
}


public enum MenuOptions
{
    [Display(Name = "Store Password")]
    StorePassword,
    [Display(Name = "Retrieve Password")]
    RetrievePassword,
    [Display(Name = "Encrypt Password")]
    EncryptPassword,
    [Display(Name = "Display Passwords")]
    DisplayPasswords,
    [Display(Name = "Generate Password")]
    GeneratePassword,
    [Display(Name = "Exit")]
    Exit,
}