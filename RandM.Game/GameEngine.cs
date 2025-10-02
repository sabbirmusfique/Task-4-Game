using RandM.Abstractions;
using Spectre.Console;

public sealed class GameEngine
{
    private readonly IMorty _morty;
    private readonly int _n;
    private readonly Statistics _stats = new();

    public GameEngine(IMorty morty, int n)
    {
        _morty = morty;
        _n = n;
    }

    public void Run()
    {
        ConsoleUI.Markup($"Morty: Oh geez, Rick, I'm gonna hide your portal gun in one of the {_n} boxes, okay?");
        bool again;
        do
        {
            var rng = new FairRandom();

            int gun = _morty.HideGun(_n, rng);

            ConsoleUI.Line($"Morty: Okay, okay, I hid the gun. What’s your guess [0,{_n})?");
            int rickGuess = ConsoleUI.PromptInt("Rick: ", 0, _n);
            int other = _morty.PickOtherToKeep(_n, rickGuess, gun, rng);
            ConsoleUI.Line("Morty: Let’s, uh, generate another value now, I mean, to select a box to keep in the game.");
            ConsoleUI.Line($"Morty: I'm keeping the box you chose, I mean {rickGuess}, and the box {other}.");
            ConsoleUI.Line("Morty: You can switch your box (enter 0), or, you know, stick with it (enter 1).");
            int keepOrSwitch = ConsoleUI.PromptInt("Rick: ", 0, 2);

            bool switched = keepOrSwitch == 0;
            int finalPick = switched ? other : rickGuess;

            bool win = finalPick == gun;
            if (win)
            {
                ConsoleUI.Line("Morty: You portal gun is in the box " + finalPick + ".");
                ConsoleUI.Line("Morty: Aww man, you won, Rick!");
            }
            else
            {
                ConsoleUI.Line("Morty: You portal gun is in the box " + gun + ".");
                ConsoleUI.Line("Morty: Aww man, you lost, Rick.");
            }

            _morty.RevealProofs(rng);

            _stats.AddRound(switched, win);

            again = ConsoleUI.PromptYesNo("Morty: Do you wanna play another round (y/n)?");
            if (!again)
            {
                ConsoleUI.Line("Morty: Okay… uh, bye!");
            }
        } while (again);

        var exact = _morty.ExactProbabilities(_n);
        TablePrinter.PrintSummary(_stats, exact);
    }
}
