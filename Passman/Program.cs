using Spectre.Console;
using Spectre.Console.Extensions;

internal abstract class Program
{
    /*
     * Purpose of this Application
     * - Store Passwords in a Database;
     * - Store Password Hash in a Database;
     * - Store Password Salt in a Database;
     * - Retrieve Stored Passwords from a Database;
     * - Encrypt/Decrypt Passwords;
     * - Create a GUI for the Application;
     */
    
    /// <nuget_packages>
    /// <nuget id="Spectre.Console" />
    /// <nuget id="ShellProgressBar" />
    /// </nuget_packages>
    
    /// <summary>
    /// Main Entry Point for the Application
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        AnsiConsole.WriteLine("Hello World!");
    }   
}