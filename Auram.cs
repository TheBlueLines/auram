namespace Auram
{
	public class Database
	{
		public static Dictionary<string, string> data = new();
		public static bool Add(string key, string value)
		{
			return data.TryAdd(key, value);
		}
		public static string? Get(string key)
		{
			return data.ContainsKey(key) ? data[key] : null;
		}
		public static bool Remove(string key)
		{
			return data.Remove(key);
		}
		public static void Clear()
		{
			data.Clear();
		}
		public static void Save(string path)
		{
			List<string> tmp = new();
			foreach (string value in data.Keys)
			{
				tmp.Add(value);
				tmp.Add(data[value]);
			}
			File.WriteAllBytes(path, Engine.Zip(Engine.SerializeStringArray(tmp.ToArray())));
		}
		public static void Load(string path)
		{
			string? tmp = null;
			foreach (string item in Engine.DeserializeStringArray(Engine.Unzip(File.ReadAllBytes(path))))
			{
				if (tmp == null)
				{
					tmp = item;
				}
				else
				{
					data.Add(tmp, item);
					tmp = null;
				}
			}
		}
		public static void Delete(string path)
		{
			if (File.Exists(path))
            {
				File.Delete(path);
			}
		}
	}
}