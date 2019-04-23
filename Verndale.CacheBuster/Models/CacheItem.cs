namespace Verndale.CacheBuster.Models
{
    public class CacheItem
    {
        public CacheItem(string key, string type, string value)
        {
            Key = key;
            Type = type;
            Value = value;
        }

        public string Key { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}