using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace Auram
{
    public class Database
    {
        public static Dictionary<string, string> data = new();
        private static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];
            int cnt;
            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
        private static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    CopyTo(msi, gs);
                }
                return mso.ToArray();
            }
        }
        private static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }
                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
        public static void AddToDatabase(string key, string value)
        {
            if (!(key.Contains("|") || key.Contains("»") || value.Contains("|") || value.Contains("»")))
            {
                data[key] = value;
            }
        }
        public static string GetFromDatabase(string key)
        {
            if (!(key.Contains("|") || key.Contains("»")) && data.ContainsKey(key))
            {
                return data[key];
            }
            else
            {
                return null;
            }
        }
        public static void RemoveFromDatabase(string key)
        {
            data.Remove(key);
        }
        public static void ClearDatabase()
        {
            data.Clear();
        }
        public static void SaveDatabase(string file)
        {
            string tmp = string.Empty;
            foreach (string value in data.Keys)
            {
                tmp += value + "»" + data[value] + "|";
            }
            tmp = tmp[0..^1];
            File.WriteAllBytes(file, Zip(tmp));
        }
        public static void LoadDatabase(string file)
        {
            foreach (string value in Unzip(File.ReadAllBytes(file)).Split('|'))
            {
                string[] y = value.Split('»');
                data.Add(y[0], y[1]);
            }
        }
        public static void DeleteDatabase(string file)
        {
            File.Delete(file);
        }
    }
}