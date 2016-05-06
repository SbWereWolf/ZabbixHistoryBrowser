namespace ZabbixHistoryBrowser
{
    public class ItemsHistory
    {
        
        // ReSharper disable MemberCanBePrivate.Global
        public System.DateTime Date { get; private set; }
        public string Value { get; private set; }
        // ReSharper restore MemberCanBePrivate.Global

        public ItemsHistory
            (
              System.DateTime date
            , string value
            ) 
        {
            this.Date = date;
            this.Value = value;
        }
    }
}
