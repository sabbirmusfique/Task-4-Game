namespace RandM.Abstractions;

public interface IMorty
{
    string DisplayName { get; }
    int HideGun(int n, IFairRandom rng);
    int PickOtherToKeep(int n, int rickGuess, int gunIndex, IFairRandom rng);
    (double stay, double @switch) ExactProbabilities(int n);
    void RevealProofs(IFairRandom rng);
}
