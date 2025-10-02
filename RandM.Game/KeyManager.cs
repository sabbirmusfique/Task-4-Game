using System.Security.Cryptography;

public static class KeyManager
{
    public static byte[] NewKey() => RandomNumberGenerator.GetBytes(32);
}
