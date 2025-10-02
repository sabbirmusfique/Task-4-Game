using Spectre.Console;

public static class TablePrinter
{
    public static void PrintSummary(Statistics stats, (double stay, double sw) exact)
    {
        var t = new Table().Caption("[grey]GAME STATS[/]");
        t.AddColumn("Game results");
        t.AddColumn("Rick switched");
        t.AddColumn("Rick stayed");

        t.AddRow("Rounds", stats.RoundsSwitched.ToString(), stats.RoundsStayed.ToString());
        t.AddRow("Wins", stats.WinsSwitched.ToString(), stats.WinsStayed.ToString());

        var (estStay, estSw) = stats.Estimates();
        t.AddRow("P (estimate)",
            estSw is null ? "?" : $"{estSw:0.000}",
            estStay is null ? "?" : $"{estStay:0.000}");

        t.AddRow("P (exact)", $"{exact.sw:0.000}", $"{exact.stay:0.000}");

        AnsiConsole.Write(t);
    }
}
