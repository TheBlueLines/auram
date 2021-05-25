using System;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace Auram
{
    class Info
    {
        public string title;
        public DateTime createdOn;
        public DateTime lastModified;
        public Dictionary<string, string> data;
    }
    public class Database
    {
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
        private static string Base64Encode(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }
        private static string Base64Decode(string base64EncodedData)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
        }
        private static string title = "Auram Database";
        private static Dictionary<string, string> data = new();
        private static DateTime createdOn;
        private static DateTime lastModified;
        public static void SetTitle(string text)
        {
            title = text;
        }
        public static void AddToDatabase(string key, string value)
        {
            data.Add(key, value);
        }
        public static string GetFromDatabase(string key)
        {
            return data[key];
        }
        public static void RemoveToDatabase(string key)
        {
            data.Remove(key);
        }
        public static void ClearDatabase()
        {
            data.Clear();
        }
        public static void SaveDatabase(string file)
        {
            Info info = new();
            if (!File.Exists(file))
            {
                info.createdOn = DateTime.Now;
            }
            else
            {
                string json = Base64Decode(Unzip(File.ReadAllBytes(file)));
                Info x = JsonConvert.DeserializeObject<Info>(json);
                info.createdOn = x.createdOn;
            }
            info.lastModified = DateTime.Now;
            info.data = data;
            info.title = title;
            File.WriteAllBytes(file, Zip(Base64Encode(JsonConvert.SerializeObject(info))));
        }
        public static DateTime Since()
        {
            return createdOn;
        }
        public static DateTime LastEdit()
        {
            return lastModified;
        }
        public static void LoadDatabase(string file)
        {
            string json = Base64Decode(Unzip(File.ReadAllBytes(file)));
            Info info = JsonConvert.DeserializeObject<Info>(json);
            data = info.data;
            title = info.title;
            createdOn = info.createdOn;
            lastModified = info.lastModified;
        }
        public static void DeleteDatabase(string file)
        {
            File.Delete(file);
        }
    }
}