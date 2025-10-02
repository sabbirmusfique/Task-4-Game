using RandM.Abstractions;
using Spectre.Console;

try
{
    var (n, mortyPath, mortyClass) = ArgsParser.ParseOrShowHelp(Environment.GetCommandLineArgs());
    var morty = MortyLoader.Load(mortyPath, mortyClass);

    var engine = new GameEngine(morty, n);
    engine.Run();
}
catch (UserFriendlyException ex)
{
    AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
    if (!string.IsNullOrWhiteSpace(ex.Hint))
        AnsiConsole.MarkupLine($"[yellow]Hint:[/] {ex.Hint}");
}
catch (Exception)
{
    AnsiConsole.MarkupLine("[red]Unexpected error. Please check your parameters or try again.[/]");
}
