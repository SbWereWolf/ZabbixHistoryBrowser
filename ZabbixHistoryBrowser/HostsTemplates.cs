namespace ZabbixHistoryBrowser
{
    public class HostsTemplates
    {
        // ReSharper disable MemberCanBePrivate.Global
        public string Template { get; private set; }
        public string Host { get; private set; }
        public long Hostid { get; private set; }
        // ReSharper restore MemberCanBePrivate.Global

        public HostsTemplates
            (
                string template
                , string host
                , long hostid
            )
        {
            this.Template = template ;
            this.Host = host ;
            this.Hostid = hostid ;
        }
    }
}
