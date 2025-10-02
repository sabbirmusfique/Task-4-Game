using RandM.Abstractions;

namespace RandM.Morties.Classic;

public sealed class ClassicMorty : IMorty
{
    public string DisplayName => "ClassicMorty";

    public int HideGun(int n, IFairRandom rng)
        => rng.NextFair(n, "HideGun");

    public int PickOtherToKeep(int n, int rickGuess, int gunIndex, IFairRandom rng)
    {
        if (rickGuess == gunIndex)
        {
            var candidates = Enumerable.Range(0, n).Where(i => i != rickGuess).ToArray();
            int k = rng.NextFair(n - 1, "PickOther");
            return candidates[k];
        }
        else
        {
            _ = rng.NextFair(n - 1, "PickOther(ignored)");
            return gunIndex;
        }
    }

    public (double stay, double @switch) ExactProbabilities(int n)
        => (1.0 / n, (n - 1.0) / n);

    public void RevealProofs(IFairRandom rng) => rng.RevealAll("RoundReveal");
}
