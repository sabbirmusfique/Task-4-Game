public sealed class Statistics
{
    public int RoundsSwitched { get; private set; }
    public int RoundsStayed { get; private set; }
    public int WinsSwitched { get; private set; }
    public int WinsStayed { get; private set; }

    public void AddRound(bool switched, bool win)
    {
        if (switched)
        {
            RoundsSwitched++;
            if (win) WinsSwitched++;
        }
        else
        {
            RoundsStayed++;
            if (win) WinsStayed++;
        }
    }

    public (double? stay, double? @switch) Estimates()
    {
        double? stay = RoundsStayed == 0 ? null : WinsStayed / (double)RoundsStayed;
        double? sw   = RoundsSwitched == 0 ? null : WinsSwitched / (double)RoundsSwitched;
        return (stay, sw);
    }
}
