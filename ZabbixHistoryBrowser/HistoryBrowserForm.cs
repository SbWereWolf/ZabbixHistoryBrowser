using System;

namespace ZabbixHistoryBrowser
{
    public partial class ZabbixHistoryBrowserForm : System.Windows.Forms.Form
    {
        private const string C_TemplateColumn = "Template";
        private const string C_HostColumn = "Host";
        const int C_TimeoutConverRatio = 60;

        private const string C_NameColumn = "Name";
        private const string C_KeyColumn = "Key";
        private const string C_UnitsColumn = "Units";
        private const string C_DescriptionColumn = "Description";
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
            System.Windows.Forms.Control serverAddressTextBox ,
            System.Windows.Forms.Control connectionPortTextBox ,
            System.Windows.Forms.Control databaseNameTextBox ,
            System.Windows.Forms.Control userIdTextBox ,
            System.Windows.Forms.Control userPasswordTextBox )
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
                    else
                    {
                        System.Windows.Forms.MessageBox.Show
                            (
                                Properties.Resources.Connection_fail);
                    }

                    MySql.Data.MySqlClient.MySqlDataReader dataSetReader = null ;

                    if ( databaseCommand != null )
                    {

                        double reqestTimeoutDouble;
                        var isSuccessParse = double.TryParse(reqestTimeoutString, out reqestTimeoutDouble);
                        if (isSuccessParse )
                        {
                            var reqestTimeout = Convert.ToInt32(reqestTimeoutDouble) * ZabbixHistoryBrowserForm.C_TimeoutConverRatio;
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
";

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

                    var isEmpty = true ;

                    if ( hasRows )
                    {
                        var readerFieldCount = dataSetReader.FieldCount ;

                        templateIndex = dataSetReader.GetOrdinal
                            (
                                ZabbixHistoryBrowserForm.C_TemplateColumn ) ;
                        hostIndex = dataSetReader.GetOrdinal
                            (
                                ZabbixHistoryBrowserForm.C_HostColumn ) ;

                        var isValidRecordSet =
                            ( ( readerFieldCount > templateIndex )
                              && ( readerFieldCount > hostIndex )
                            )
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

                        var hostsTemplates = new HostsTemplates
                            (
                            template ,
                            host 
                             ) ;
                        hostsTemplatesList.Add ( hostsTemplates );
                    }

                    dataSetReader?.Close();

                    databaseConnection?.Close();

                    var hostsDataGridView = this.HostsDataGridView ;
                    if ( hostsDataGridView != null )
                    {
                        hostsDataGridView.DataSource = null ;
                        hostsDataGridView.Rows.Clear (  );
                        hostsDataGridView.MultiSelect = false ;
                        hostsDataGridView.AutoGenerateColumns = true ;
                        hostsDataGridView.DataSource = hostsTemplatesList;
                    }
                }

            }
        }

        private void ChooseHostButton_Click(object sender, EventArgs e)
        {
            var hostsDataGridView = this.HostsDataGridView ;
            var selectedRowsCount = hostsDataGridView?.SelectedRows.Count ;
            var selectedCellsCount = hostsDataGridView?.SelectedCells.Count;

            var host = string.Empty;
            var selectedRow = new System.Windows.Forms.DataGridViewRow();

            if ( ( selectedRowsCount > 0 ) 
                || (selectedCellsCount > 0))
            {
                
                const int C_FirastIndex = 0;

                if (selectedCellsCount > 0 )
                {
                    var gridViewCell = hostsDataGridView.SelectedCells[C_FirastIndex] ;
                    if ( gridViewCell != null )
                    {
                        selectedRow = gridViewCell.OwningRow;
                    }
                }
                else
                {
                    selectedRow = hostsDataGridView.SelectedRows[C_FirastIndex];
                }
            }

            var dataGridViewCell = selectedRow?.Cells[ZabbixHistoryBrowserForm.C_HostColumn];
            if (dataGridViewCell?.Value != null)
            {
                host = (string)dataGridViewCell.Value;
            }

            var hostFilterTextBox = this.HostFilterTextBox ;
            if ( hostFilterTextBox != null )
            {
                hostFilterTextBox.Text = host ;
            }
        }

        private void ChooseItemButton_Click(object sender, EventArgs e)
        {

        }

        private void RequestItemsButton_Click(object sender, EventArgs e)
        {

            var serverAddressTextBox = this.ServerAddressTextBox;
            var connectionPortTextBox = this.ConnectionsPortTextBox;
            var databaseNameTextBox = this.DatabaseNameTextBox;
            var userIdTextBox = this.UserIdTextBox;
            var userPasswordTextBox = this.UserPasswordTextBox;
            var reqestTimeoutTextBox = this.ReqestTimeoutTextBox;

            var hostParameterTextBox = this.HostFilterTextBox;
            var itemParameterTextBox = this.ItemNameFilterTextBox;

            if ((serverAddressTextBox != null)
                 && (connectionPortTextBox != null)
                 && (databaseNameTextBox != null)
                 && (userIdTextBox != null)
                 && (userPasswordTextBox != null)
                 && (reqestTimeoutTextBox != null)
                 && (hostParameterTextBox != null)
                 && (itemParameterTextBox != null))
            {
                var serverAddress = serverAddressTextBox.Text;
                var connectionPort = connectionPortTextBox.Text;
                var databaseName = databaseNameTextBox.Text;
                var userId = userIdTextBox.Text;
                var userPassword = userPasswordTextBox.Text;
                var reqestTimeoutString = reqestTimeoutTextBox.Text;


                using (var databaseConnection = ZabbixHistoryBrowserForm.GetDatabaseConnection
                    (
                        serverAddress,
                        connectionPort,
                        databaseName,
                        userId,
                        userPassword))
                {
                    var isConnectionOpen = false;

                    if (databaseConnection != null)
                    {
                        var isSuccessConnection = databaseConnection.State == System.Data.ConnectionState.Open;
                        if (isSuccessConnection)
                        {
                            isConnectionOpen = true;
                        }

                    }

                    var databaseCommand = new MySql.Data.MySqlClient.MySqlCommand();

                    if (isConnectionOpen)
                    {
                        databaseCommand = databaseConnection.CreateCommand();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show
                            (
                                Properties.Resources.Connection_fail ) ;
                    }

                    MySql.Data.MySqlClient.MySqlDataReader dataSetReader = null;

                    if (databaseCommand != null)
                    {

                        double reqestTimeoutDouble;
                        var isSuccessParse = double.TryParse(reqestTimeoutString, out reqestTimeoutDouble);
                        if (isSuccessParse)
                        {
                            var reqestTimeout = Convert.ToInt32(reqestTimeoutDouble) * ZabbixHistoryBrowserForm.C_TimeoutConverRatio;
                            databaseCommand.CommandTimeout = reqestTimeout;
                        }

                        databaseCommand.CommandText = @"
SELECT
    ( SELECT host FROM `hosts` HOS WHERE HOS.hostid = IT.hostid ) AS 'Host'
    , IT.name AS 'Name'
    , IT.key_ AS 'Key'
    , IT.units AS 'Units'
    , IT.description AS 'Description'

FROM
    items IT
WHERE
        (
               @HostMask IS NULL
            OR IT.hostid IN ( SELECT hostid FROM `hosts` HOW WHERE HOW.name LIKE @HostMask)
        )
    AND (
               @ItemMask IS NULL
            OR IT.itemid IN ( SELECT itemid FROM `items` ITW WHERE ITW.name LIKE @ItemMask)
        )
    AND
        (
            EXISTS
            (
                SELECT
                    NULL
                FROM
                    history HD
                WHERE
                    HD.itemid = IT.itemid
            )
            OR EXISTS
            (
                SELECT
                    NULL
                FROM
                    history_str HSE
                WHERE
                    HSE.itemid = IT.itemid
            )
            OR EXISTS
            (
                SELECT
                    NULL
                FROM
                    history_text HT
                WHERE
                    HT.itemid = IT.itemid
            )
            OR EXISTS
            (
                SELECT
                    NULL
                FROM
                    history_uint HU
                WHERE
                    HU.itemid = IT.itemid
            )         
        )
;
";
                        var hostParameter = hostParameterTextBox.Text;
                        if (string.IsNullOrWhiteSpace(hostParameter))
                        {
                            hostParameter = null;
                        }
                        databaseCommand.Parameters?.AddWithValue("@HostMask", hostParameter);

                        var itemParameter = itemParameterTextBox.Text;
                        if (string.IsNullOrWhiteSpace(itemParameter))
                        {
                            itemParameter = null;
                        }
                        databaseCommand.Parameters?.AddWithValue("@ItemMask", itemParameter);



                        databaseCommand.Prepare();
                        dataSetReader = databaseCommand.ExecuteReader();
                    }

                    var hasRows = false;

                    if (dataSetReader != null)
                    {
                        hasRows = dataSetReader.HasRows;
                    }

                    var hostIndex = 0;
                    var nameIndex = 0;
                    var keyIndex = 0;
                    var unitsIndex = 0;
                    var descriptionIndex = 0;

                    var isEmpty = true;

                    if (hasRows)
                    {
                        var readerFieldCount = dataSetReader.FieldCount;

                        hostIndex = dataSetReader.GetOrdinal
                            (
                                ZabbixHistoryBrowserForm.C_HostColumn ) ;
                        nameIndex = dataSetReader.GetOrdinal
                            (
                                ZabbixHistoryBrowserForm.C_NameColumn);
                        keyIndex = dataSetReader.GetOrdinal
                            (
                                ZabbixHistoryBrowserForm.C_KeyColumn);
                        unitsIndex = dataSetReader.GetOrdinal
                            (
                                ZabbixHistoryBrowserForm.C_UnitsColumn);
                        descriptionIndex = dataSetReader.GetOrdinal
                            (
                                ZabbixHistoryBrowserForm.C_DescriptionColumn);

                        var isValidRecordSet =
                            ( ( readerFieldCount > hostIndex )
                              && ( readerFieldCount > nameIndex )
                              && ( readerFieldCount > keyIndex )
                              && ( readerFieldCount > unitsIndex )
                              && ( readerFieldCount > descriptionIndex )
                            )
                            ;

                        isEmpty = !isValidRecordSet;

                    }

                    var hostsItemsList = new System.Collections.Generic.List<HostsItems>();

                    while (!isEmpty)
                    {

                        isEmpty = !dataSetReader.Read();

                        var host = dataSetReader.GetString
                            (
                                hostIndex);
                        var name = dataSetReader.GetString
                            (
                                nameIndex);
                        var key = dataSetReader.GetString
                                                    (
                                                        keyIndex);
                        var units = dataSetReader.GetString
                                                    (
                                                        unitsIndex);
                        var description = dataSetReader.GetString
                                                    (
                                                        descriptionIndex);

                        var hostsItem = new HostsItems
                            (
                            host
                            , name
                            , key
                            , units
                            , description
                            ) ;
                        hostsItemsList.Add(hostsItem);
                    }

                    dataSetReader?.Close();

                    databaseConnection?.Close();

                    var itemsListDataGridView = this.ItemsListDataGridView;
                    if (itemsListDataGridView != null)
                    {
                        itemsListDataGridView.DataSource = null;
                        itemsListDataGridView.Rows.Clear();
                        itemsListDataGridView.MultiSelect = false;
                        itemsListDataGridView.AutoGenerateColumns = true;
                        itemsListDataGridView.DataSource = hostsItemsList;
                    }
                }

            }

        }
    }
}