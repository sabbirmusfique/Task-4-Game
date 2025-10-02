public sealed class FairRngSession
{
    public int Index { get; }
    public string Purpose { get; }
    public int N { get; }

    public byte[] Key { get; }
    public int MortyValue { get; }
    public string HmacHex { get; }

    public int RickValue { get; private set; } = -1;
    public int FinalValue { get; private set; } = -1;

    public FairRngSession(int index, string purpose, int n, byte[] key, int mortyValue, string hmacHex)
    {
        Index = index; Purpose = purpose; N = n;
        Key = key; MortyValue = mortyValue; HmacHex = hmacHex;
    }

    public void Complete(int rickValue)
    {
        RickValue = rickValue;
        FinalValue = ((MortyValue + RickValue) % N + N) % N;
    }
}
