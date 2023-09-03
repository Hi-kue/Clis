using Spectre.Console;
using Spectre.Console.Extensions;
using Sharprompt;
using ShellProgressBar;
using FigletFontArt;
using Passman.Config;

internal abstract class Program
{
    /// <nuget_packages>
    /// <nuget id="Spectre.Console">
    ///   <version>0.40.0</version>
    ///   <install>dotnet add package Spectre.Console</install>
    /// </nuget>
    /// <nuget id="ShellProgressBar">
    ///  <version>5.0.0</version>
    ///  <install>dotnet add package ShellProgressBar</install>
    /// </nuget>
    /// <nuget id="SharPrompt">
    ///  <version>1.0.0</version>
    ///  <install>dotnet add package SharPrompt</install>
    /// </nuget>
    /// <nuget id="FigletFontArt">
    ///  <version>1.0.0</version>
    ///  <install>dotnet add package FigletFontArt  --version 1.0.0</install>
    /// </nuget>
    /// </nuget_packages>
    
    /// <summary>
    /// Main Entry Point for the Application
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        // Title
        Console.WriteLine(FiggleFonts.Larry3d.Render("Passman"));

        var init = new Init();
    }

    private void Render()
    {
        
    }
}