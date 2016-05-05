using System;

namespace ZabbixHistoryBrowser
{
    public partial class ZabbixHistoryBrowserForm : System.Windows.Forms.Form
    {
        public ZabbixHistoryBrowserForm()
        {
            this.InitializeComponent();
        }

        private void TestConnectionButton_Click(object sender, EventArgs e)
        {
            var serverAddressTextBox = this.ServerAddressTextBox ;
            var connectionPortTextBox = this.ConnectionsPortTextBox ;
            var databaseNameTextBox = this.DatabaseNameTextBox;
            var userIdTextBox = this.UserIdTextBox ;
            var userPasswordTextBox = this.UserPasswordTextBox ;

            ZabbixHistoryBrowserForm.TestConnection (
                serverAddressTextBox ,
                connectionPortTextBox ,
                databaseNameTextBox ,
                userIdTextBox ,
                userPasswordTextBox ) ;
        }

        private static void TestConnection
            (
            System.Windows.Forms.TextBox serverAddressTextBox ,
            System.Windows.Forms.TextBox connectionPortTextBox ,
            System.Windows.Forms.TextBox databaseNameTextBox ,
            System.Windows.Forms.TextBox userIdTextBox ,
            System.Windows.Forms.TextBox userPasswordTextBox )
        {
            if ( ( serverAddressTextBox != null )
                 && ( connectionPortTextBox != null )
                 && ( databaseNameTextBox != null )
                 && ( userIdTextBox != null )
                 && ( userPasswordTextBox != null ) )
            {
                var serverAddress = serverAddressTextBox.Text ;
                var connectionPort = connectionPortTextBox.Text ;
                var databaseName = databaseNameTextBox.Text ;
                var userId = userIdTextBox.Text ;
                var userPassword = userPasswordTextBox.Text ;


                var databaseConnection = ZabbixHistoryBrowserForm.GetDatabaseConnection
                    (
                        serverAddress ,
                        connectionPort ,
                        databaseName ,
                        userId ,
                        userPassword ) ;

                if ( databaseConnection != null )
                {

                    var connectionState = databaseConnection.State.ToString();
                    System.Windows.Forms.MessageBox.Show
                        (
                            connectionState);

                    databaseConnection.Close();
                }
            }
        }

        private static MySql.Data.MySqlClient.MySqlConnection GetDatabaseConnection
            (
            string serverAddress ,
            string connectionPort ,
            string databaseName ,
            string userId ,
            string userPassword )
        {
            var connectionString =
                $"Server={serverAddress};"
                + $"Port={connectionPort};"
                + $"Database={databaseName};"
                + $"Uid={userId};"
                + $"Pwd={userPassword};" ;

            var databaseConnection = new MySql.Data.MySqlClient.MySqlConnection ( connectionString ) ;
            try
            {
                databaseConnection.Open ( ) ;
            }
            catch ( Exception )
            {
                databaseConnection = null ;
            }
            return databaseConnection ;
        }

        private void GetHostsListButton_Click(object sender, EventArgs e)
        {
            var serverAddressTextBox = this.ServerAddressTextBox;
            var connectionPortTextBox = this.ConnectionsPortTextBox;
            var databaseNameTextBox = this.DatabaseNameTextBox;
            var userIdTextBox = this.UserIdTextBox;
            var userPasswordTextBox = this.UserPasswordTextBox;
            var reqestTimeoutTextBox = this.ReqestTimeoutTextBox ;

            if ((serverAddressTextBox != null)
                 && (connectionPortTextBox != null)
                 && (databaseNameTextBox != null)
                 && (userIdTextBox != null)
                 && (userPasswordTextBox != null)
                 && (reqestTimeoutTextBox !=null ))
            {
                var serverAddress = serverAddressTextBox.Text;
                var connectionPort = connectionPortTextBox.Text;
                var databaseName = databaseNameTextBox.Text;
                var userId = userIdTextBox.Text;
                var userPassword = userPasswordTextBox.Text;
                var reqestTimeoutString = reqestTimeoutTextBox.Text ;


                using ( var databaseConnection = ZabbixHistoryBrowserForm.GetDatabaseConnection
                    (
                        serverAddress ,
                        connectionPort ,
                        databaseName ,
                        userId ,
                        userPassword ) )
                {
                    var isConnectionOpen = false ;

                    if ( databaseConnection != null )
                    {
                        var isSuccessConnection = databaseConnection.State == System.Data.ConnectionState.Open ;
                        if ( isSuccessConnection )
                        {
                            isConnectionOpen = true ;
                        }

                    }

                    var databaseCommand = new MySql.Data.MySqlClient.MySqlCommand ( ) ;

                    if ( isConnectionOpen )
                    {
                        databaseCommand = databaseConnection.CreateCommand ( ) ;
                    }

                    MySql.Data.MySqlClient.MySqlDataReader dataSetReader = null ;

                    if ( databaseCommand != null )
                    {

                        double reqestTimeoutDouble;
                        var isSuccessParse = double.TryParse(reqestTimeoutString, out reqestTimeoutDouble);
                        if (isSuccessParse )
                        {
                            const int C_TimeoutConverRatio = 60 ;
                            var reqestTimeout = Convert.ToInt32(reqestTimeoutDouble) * C_TimeoutConverRatio;
                            databaseCommand.CommandTimeout = reqestTimeout;
                        }

                        databaseCommand.CommandText = @"
SELECT
      ( SELECT NAME FROM `hosts` HOS WHERE HOS.Hostid = HP.templateid ) AS 'Template'
    , ( SELECT NAME FROM `hosts` HOS WHERE HOS.Hostid = HP.Hostid ) AS 'Host'
	, HP.Hostid AS 'Hostid'
FROM
        hosts_templates HP
WHERE
       EXISTS
        (
            SELECT
                NULL
            FROM
                `hosts` HOE
                JOIN items ITE
                ON ITE.Hostid = HOE.Hostid
                JOIN history HD
                ON HD.itemid = ITE.itemid
                WHERE
                    HOE.Hostid = HP.Hostid
        )
    OR EXISTS
        (
            SELECT
                NULL
            FROM
                `hosts` HOE
                JOIN items ITE
                ON ITE.Hostid = HOE.Hostid
                JOIN history_str HSE
                ON HSE.itemid = ITE.itemid
                WHERE
                    HOE.Hostid = HP.Hostid
        )
    OR EXISTS
        (
            SELECT
                NULL
            FROM
                `hosts` HOE
                JOIN items ITE
                ON ITE.Hostid = HOE.Hostid
                JOIN history_text HT
                ON HT.itemid = ITE.itemid
                WHERE
                    HOE.Hostid = HP.Hostid
        )
    OR EXISTS
        (
            SELECT
                NULL
            FROM
                `hosts` HOE
                JOIN items ITE
                ON ITE.Hostid = HOE.Hostid
                JOIN history_uint HU
                ON HU.itemid = ITE.itemid
                WHERE
                    HOE.Hostid = HP.Hostid
        )        
ORDER BY 
      HP.templateid
    , HP.Hostid
;
" ;
                        
                        databaseCommand.Prepare ( ) ;
                        dataSetReader = databaseCommand.ExecuteReader ( ) ;
                    }

                    var hasRows = false ;

                    if ( dataSetReader != null )
                    {
                        hasRows = dataSetReader.HasRows ;
                    }

                    var templateIndex = 0 ;
                    var hostIndex = 0 ;
                    var hostidIndex = 0 ;

                    var isEmpty = true ;

                    if ( hasRows )
                    {
                        var readerFieldCount = dataSetReader.FieldCount ;

                        const string C_TemplateColumn = "Template" ;
                        const string C_HostColumn = "Host" ;
                        const string C_HostidColumn = "Hostid" ;

                        templateIndex = dataSetReader.GetOrdinal
                            (
                                C_TemplateColumn ) ;
                        hostIndex = dataSetReader.GetOrdinal
                            (
                                C_HostColumn ) ;
                        hostidIndex = dataSetReader.GetOrdinal
                            (
                                C_HostidColumn ) ;

                        var isValidRecordSet =
                            ( readerFieldCount > templateIndex )
                            && ( readerFieldCount > hostIndex )
                            && ( readerFieldCount > hostidIndex )
                            ;

                        isEmpty = !isValidRecordSet ;

                    }

                    var hostsTemplatesList = new System.Collections.Generic.List <HostsTemplates> (  );

                    while ( !isEmpty )
                    {

                        isEmpty = !dataSetReader.Read ( ) ;
                        
                        var template = dataSetReader.GetString
                            (
                                templateIndex ) ;
                        var host = dataSetReader.GetString
                            (
                                hostIndex ) ;
                        var hostId = dataSetReader.GetInt64
                            (
                                hostidIndex ) ;

                        var hostsTemplates = new HostsTemplates
                            (
                            template ,
                            host ,
                            hostId ) ;
                        hostsTemplatesList.Add ( hostsTemplates );
                    }

                    dataSetReader?.Close();

                    databaseConnection?.Close();

                    var hostsDataGridView = this.HostsDataGridView ;
                    if ( hostsDataGridView != null )
                    {
                        hostsDataGridView.DataSource = null ;
                        hostsDataGridView.Rows.Clear (  );
                        hostsDataGridView.AutoGenerateColumns = true ;
                        hostsDataGridView.DataSource = hostsTemplatesList;
                    }
                }

            }
        }
    }
}