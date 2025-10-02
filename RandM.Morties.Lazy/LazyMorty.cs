using RandM.Abstractions;

namespace RandM.Morties.Lazy;

public sealed class LazyMorty : IMorty
{
    public string DisplayName => "LazyMorty";

    public int HideGun(int n, IFairRandom rng)
        => rng.NextFair(n, "HideGun");

    public int PickOtherToKeep(int n, int rickGuess, int gunIndex, IFairRandom rng)
    {
        var alive = new HashSet<int>(Enumerable.Range(0, n));
        int toRemove = n - 2;

        for (int i = 0; i < n && toRemove > 0; i++)
        {
            if (i == rickGuess || i == gunIndex) continue;
            alive.Remove(i);
            toRemove--;
        }
        int other = alive.First(x => x != rickGuess);
        return other;
    }

    public (double stay, double @switch) ExactProbabilities(int n)
        => (1.0 / n, (n - 1.0) / n);

    public void RevealProofs(IFairRandom rng) => rng.RevealAll("RoundReveal");
}
