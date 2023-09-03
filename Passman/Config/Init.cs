using Spectre.Console;
using Spectre.Console.Extensions;
using Sharprompt;
using ShellProgressBar;


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

    private const int Height = 20;
    private const int Width = 20;
    private const double MaxValueExtent = 2.0;

    public Init()
    {
        // Configure and Setup the Application
        Configure();
    }
    
    private readonly struct ComplexNumber
    {
        private double Real { get; }
        private double Imaginary { get; }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static ComplexNumber operator +(ComplexNumber x, ComplexNumber y)
        {
            return new ComplexNumber(x.Real + y.Real, x.Imaginary + y.Imaginary);
        }

        public static ComplexNumber operator *(ComplexNumber x, ComplexNumber y)
        {
            return new ComplexNumber(x.Real * y.Real - x.Imaginary * y.Imaginary,
                x.Real * y.Imaginary + x.Imaginary * y.Real);
        }

        public double Abs()
        {
            return Real * Real + Imaginary * Imaginary;
        }
    }

    private void Configure()
    {
        // Title
        AnsiConsole.Write(new FigletText("Center").LeftJustified().Color(Color.DarkKhaki));
        var userName = Prompt.Input<string>("Welcome to Passman, please enter your name:");
        

        // Setup Database
        SetupDatabase();

        // Setup Menu System
        var menu = new Menu();
        menu.DisplayMenu();
    }

    private void SetupDatabase()
    {
        
    }
}