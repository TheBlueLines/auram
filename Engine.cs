using System.IO.Compression;
using System.Text;

namespace Auram
{
    internal class Engine
    {
        public static byte[] SerializeStringArray(string[] array)
        {
            List<byte> list = new();
            foreach (string str in array)
            {
                list.AddRange(VarintBitConverter.GetVarintBytes(Encoding.UTF8.GetBytes(str).Length));
                list.AddRange(Encoding.UTF8.GetBytes(str));
            }
            return Combine(list.ToArray());
        }
        public static string[] DeserializeStringArray(byte[] array)
        {
            List<byte> temp = new();
            List<string> words = new();
            temp.AddRange(array);
            while (temp.Count != 0)
            {
                int length = VarintBitConverter.ToInt32(temp.ToArray());
                temp.RemoveRange(0, VarintBitConverter.GetVarintBytes(length).Length);
                words.Add(Encoding.UTF8.GetString(temp.GetRange(0, length).ToArray()));
                temp.RemoveRange(0, length);
            }
            return words.ToArray();
        }
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];
            int cnt;
            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
        public static byte[] Zip(byte[] data)
        {
            using (var msi = new MemoryStream(data))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    CopyTo(msi, gs);
                }
                return mso.ToArray();
            }
        }
        public static byte[] Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }
                return mso.ToArray();
            }
        }
    }
}
