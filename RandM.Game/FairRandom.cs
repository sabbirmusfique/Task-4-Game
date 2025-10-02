using System.Security.Cryptography;
using RandM.Abstractions;
using Spectre.Console;

public sealed class FairRandom : IFairRandom
{
    private readonly List<FairRngSession> _sessions = new();
    private int _counter = 0;

    public int NextFair(int n, string purposeLabel)
    {
        if (n <= 0) throw new UserFriendlyException("Fair Game called with non-positive range.");

        var key = KeyManager.NewKey();
        int mortyValue = RandomNumberGenerator.GetInt32(n);
        var hmac = HmacSha3.ComputeSha3_256(key, mortyValue);
        var hmacHex = HmacSha3.ToHex(hmac);
        var sessionNum = _counter + 1;
        var session = new FairRngSession(++_counter, purposeLabel, n, key, mortyValue, hmacHex);
        _sessions.Add(session);

        string hmacLabel = sessionNum == 1 ? "1st" : sessionNum == 2 ? "2nd" : $"{sessionNum}th";
        ConsoleUI.Line($"Morty: HMAC{hmacLabel}={hmacHex}");
        ConsoleUI.Line($"Morty: Rick, enter your number [0,{n}), and, uh, don’t say I didn’t play fair, okay?");
        int rick = ConsoleUI.PromptInt("Rick: ", 0, n);

        session.Complete(rick);

        return session.FinalValue;
    }

    public void RevealAll(string headerLabel)
    {
        if (_sessions.Count == 0) return;

        ConsoleUI.Line("Morty: Aww man, time to reveal my secrets!");
        int i = 1;
        foreach (var s in _sessions)
        {
            string label = i == 1 ? "1st" : i == 2 ? "2nd" : $"{i}th";
            ConsoleUI.Line($"Morty: Aww man, my {label} random value is {s.MortyValue}.");
            ConsoleUI.Line($"Morty: KEY{label}={HmacSha3.ToHex(s.Key)}");
            ConsoleUI.Line($"Morty: So the {label} fair number is ({s.MortyValue} + {s.RickValue}) % {s.N} = {s.FinalValue}.");
            i++;
        }
        _sessions.Clear();
    }
}
