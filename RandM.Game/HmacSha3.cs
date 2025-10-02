using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Digests;
using System.Buffers.Binary;

public static class HmacSha3
{
    public static byte[] ComputeSha3_256(byte[] key, int mortyValue)
    {
        byte[] msg = new byte[4];
        BinaryPrimitives.WriteInt32BigEndian(msg, mortyValue);

        IMac hmac = new HMac(new Sha3Digest(256));
        hmac.Init(new KeyParameter(key));
        hmac.BlockUpdate(msg, 0, msg.Length);
        byte[] output = new byte[hmac.GetMacSize()];
        hmac.DoFinal(output, 0);
        return output;
    }

    public static string ToHex(byte[] data)
    {
        var c = new char[data.Length * 2];
        int idx = 0;
        foreach (var b in data)
        {
            c[idx++] = GetHexNibble(b >> 4);
            c[idx++] = GetHexNibble(b & 0xF);
        }
        return new string(c);
    }

    private static char GetHexNibble(int v) => (char)(v < 10 ? '0' + v : 'A' + (v - 10));
}
