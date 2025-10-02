namespace RandM.Abstractions;
public interface IFairRandom
{
    int NextFair(int n, string purposeLabel);
    void RevealAll(string headerLabel);
}
