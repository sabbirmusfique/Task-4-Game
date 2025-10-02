using Spectre.Console;

public sealed class UserFriendlyException : Exception
{
    public string Hint { get; }
    public UserFriendlyException(string message, string hint = "") : base(message) => Hint = hint;
}

public static class ArgsParser
{
    public static (int n, string mortyPath, string mortyClass) ParseOrShowHelp(string[] argv)
    {

        if (argv.Length != 4)
        {
            ShowUsage();
            throw new UserFriendlyException(
                "You must pass exactly 3 arguments: <boxes> <morty-assembly-path> <morty-class>",
                "Example: dotnet run --project RandM.Game 3 ./RandM.Morties.Classic/bin/Debug/net9.0/RandM.Morties.Classic.dll RandM.Morties.Classic.ClassicMorty");
        }

        if (!int.TryParse(argv[1], out var n) || n <= 2)
            throw new UserFriendlyException("Boxes must be an integer greater than 2.",
                "Try: 3, 4, 5, ...");

        var path = argv[2];
        var cls = argv[3];

        return (n, path, cls);
    }

    private static void ShowUsage()
    {
        var rule = new Rule("[bold]Rick & Morty — Usage[/]");
        AnsiConsole.Write(rule);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("Run:");
        AnsiConsole.MarkupLine("[cyan]dotnet run --project RandM.Game -- <boxes> <morty-assembly-path> <morty-class>[/]");
        AnsiConsole.MarkupLine("Example:");
        AnsiConsole.MarkupLine("[green]dotnet run --project RandM.Game -- 3 ./RandM.Morties.Classic/bin/Debug/net9.0/RandM.Morties.Classic.dll RandM.Morties.Classic.ClassicMorty[/]");
        AnsiConsole.WriteLine();
    }
}
