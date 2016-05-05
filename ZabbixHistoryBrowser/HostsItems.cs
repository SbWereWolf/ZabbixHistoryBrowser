namespace ZabbixHistoryBrowser
{
    public class HostsItems
    {
        
        // ReSharper disable MemberCanBePrivate.Global
        public string Host { get; private set; }
        public string Name { get; private set; }
        public string Key { get; private set; }
        public string Units { get; private set; }
        public string Description { get; private set; }
        // ReSharper restore MemberCanBePrivate.Global

        public HostsItems
            (
              string host
            , string name
            , string key
            , string units
            , string description
            ) 
        {
            this.Host = host;
            this.Name = name;
            this.Key = key;
            this.Units = units;
            this.Description = description;
        }
    }
}
