using Spectre.Console;

public static class ConsoleUI
{
    public static int PromptInt(string message, int minInclusive, int maxExclusive)
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>(message.EscapeMarkup());
            if (int.TryParse(input, out var v) && v >= minInclusive && v < maxExclusive)
                return v;

            AnsiConsole.MarkupLine(
                $"[yellow]Please enter an integer in range [[{minInclusive},{maxExclusive}]].[/]");
        }
    }

    public static bool PromptYesNo(string message)
    {
        var c = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(message.EscapeMarkup())
                .AddChoices("y", "n"));
        return c == "y";
    }

    public static void Line(string text) => AnsiConsole.WriteLine(text);
    public static void Markup(string text) => AnsiConsole.MarkupLine(text);
}
