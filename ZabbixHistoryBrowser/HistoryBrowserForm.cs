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

        private const string C_DateColumn = "Date";
        private const string C_ValueColumn = "value";

        const int C_FirastIndex = 0;
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

                    MySql.Data.MySqlClient.MySqlCommand databaseCommand = null ;

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

                        databaseCommand.CommandText = $@"
SELECT
      ( SELECT NAME FROM `hosts` HOS WHERE HOS.Hostid = HP.templateid ) AS '{ZabbixHistoryBrowserForm.C_TemplateColumn}' -- 'Template'
    , ( SELECT NAME FROM `hosts` HOS WHERE HOS.Hostid = HP.Hostid ) AS '{ZabbixHistoryBrowserForm.C_HostColumn}' -- 'Host'
	-- , HP.Hostid AS 'Hostid'
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
LIMIT 1
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
            System.Windows.Forms.DataGridViewRow selectedRow = null ;

            if ( ( selectedRowsCount > 0 ) 
                || (selectedCellsCount > 0))
            {
                if (selectedCellsCount > 0 )
                {
                    var gridViewCell = hostsDataGridView.SelectedCells[ZabbixHistoryBrowserForm.C_FirastIndex] ;
                    if ( gridViewCell != null )
                    {
                        selectedRow = gridViewCell.OwningRow;
                    }
                }
                else
                {
                    selectedRow = hostsDataGridView.SelectedRows[ZabbixHistoryBrowserForm.C_FirastIndex];
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
            var host = string.Empty;
            var key = string.Empty;
            System.Windows.Forms.DataGridViewRow selectedRow = null ;

            var itemsListDataGridView = this.ItemsListDataGridView;
            var selectedRowsCount = itemsListDataGridView?.SelectedRows.Count;
            var selectedCellsCount = itemsListDataGridView?.SelectedCells.Count;

            if ((selectedRowsCount > 0)
                || (selectedCellsCount > 0))
            {

                if (selectedCellsCount > 0)
                {
                    var gridViewCell = itemsListDataGridView.SelectedCells[ZabbixHistoryBrowserForm.C_FirastIndex];
                    if (gridViewCell != null)
                    {
                        selectedRow = gridViewCell.OwningRow;
                    }
                }
                else
                {
                    selectedRow = itemsListDataGridView.SelectedRows[ZabbixHistoryBrowserForm.C_FirastIndex];
                }
            }

            var dataGridViewCell = selectedRow?.Cells[ZabbixHistoryBrowserForm.C_HostColumn];
            if (dataGridViewCell?.Value != null)
            {
                host = (string)dataGridViewCell.Value;
            }
            var historyHostFilterTextBox = this.HistoryHostFilterTextBox;
            if (historyHostFilterTextBox != null)
            {
                historyHostFilterTextBox.Text = host;
            }

            dataGridViewCell = selectedRow?.Cells[ZabbixHistoryBrowserForm.C_KeyColumn];
            if (dataGridViewCell?.Value != null)
            {
                key = (string)dataGridViewCell.Value;
            }
            var historyItemKeyFilterTextBox = this.HistoryItemKeyFilterTextBox;
            if (historyItemKeyFilterTextBox != null)
            {
                historyItemKeyFilterTextBox.Text = key;
            }


        }

        private void RequestItemsButton_Click(object sender, EventArgs e)
        {

            var hostsItemsList = new System.Collections.Generic.List<HostsItems>();

            var serverAddress = string.Empty;
            var connectionPort = string.Empty;
            var databaseName = string.Empty;
            var userId = string.Empty;
            var userPassword = string.Empty;


            var serverAddressTextBox = this.ServerAddressTextBox;
            var connectionPortTextBox = this.ConnectionsPortTextBox;
            var databaseNameTextBox = this.DatabaseNameTextBox;
            var userIdTextBox = this.UserIdTextBox;
            var userPasswordTextBox = this.UserPasswordTextBox;
            var reqestTimeoutTextBox = this.ReqestTimeoutTextBox;

            var hostParameterTextBox = this.HostFilterTextBox;
            var itemParameterTextBox = this.ItemKeyFilterTextBox;

            if ((serverAddressTextBox != null)
                 && (connectionPortTextBox != null)
                 && (databaseNameTextBox != null)
                 && (userIdTextBox != null)
                 && (userPasswordTextBox != null)
                  )
            {
                serverAddress = serverAddressTextBox.Text;
                connectionPort = connectionPortTextBox.Text;
                databaseName = databaseNameTextBox.Text;
                userId = userIdTextBox.Text;
                userPassword = userPasswordTextBox.Text;
            }


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

                MySql.Data.MySqlClient.MySqlCommand databaseCommand = null ;

                if (isConnectionOpen)
                {
                    databaseCommand = databaseConnection.CreateCommand();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show
                        (
                            Properties.Resources.Connection_fail);
                }

                MySql.Data.MySqlClient.MySqlDataReader dataSetReader = null;

                if (databaseCommand != null)
                {

                    var reqestTimeoutString = string.Empty;
                    if (reqestTimeoutTextBox != null)
                    {
                        reqestTimeoutString = reqestTimeoutTextBox.Text;
                    }

                    double requestTimeoutDouble;
                    var isSuccessParse = double.TryParse
                        (
                            reqestTimeoutString,
                            out requestTimeoutDouble);
                    if (isSuccessParse)
                    {
                        var reqestTimeout = Convert.ToInt32
                                                (
                                                    requestTimeoutDouble)
                                            * ZabbixHistoryBrowserForm.C_TimeoutConverRatio;
                        databaseCommand.CommandTimeout = reqestTimeout;
                    }

                    const string C_HostMask = "@HostMask";
                    const string C_ItemKeyMask = "@ItemKeyMask";

                    databaseCommand.CommandText = $@"
SELECT
    ( SELECT host FROM `hosts` HOS WHERE HOS.hostid = IT.hostid ) AS '{ZabbixHistoryBrowserForm.C_HostColumn}'
    , IT.name AS '{ZabbixHistoryBrowserForm.C_NameColumn}'
    , IT.key_ AS '{ZabbixHistoryBrowserForm.C_KeyColumn}' 
    , IT.units AS '{ZabbixHistoryBrowserForm.C_UnitsColumn}'
    , IT.description AS '{ZabbixHistoryBrowserForm.C_DescriptionColumn}'

FROM
    items IT
WHERE
        (
               {C_HostMask} IS NULL
            OR  EXISTS( SELECT NULL FROM `hosts` HOW WHERE HOW.hostid = IT.hostid AND HOW.host LIKE {C_HostMask} )
        )
    AND (
               {C_ItemKeyMask} IS NULL
            OR  IT.key_ LIKE {C_ItemKeyMask}
        )
    AND
        (
            EXISTS
            (
                SELECT
                    NULL
                FROM
                    history HDE
                WHERE
                    HDE.itemid = IT.itemid
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
                    history_text HTE
                WHERE
                    HTE.itemid = IT.itemid
            )
            OR EXISTS
            (
                SELECT
                    NULL
                FROM
                    history_uint HUE
                WHERE
                    HUE.itemid = IT.itemid
            )         
        )
;
";

                    var hostParameter = string.Empty;
                    if (
                        (hostParameterTextBox != null)
                        )
                    {
                        hostParameter = hostParameterTextBox.Text;
                    }
                    if (string.IsNullOrWhiteSpace
                        (
                            hostParameter))
                    {
                        hostParameter = null;
                    }

                    databaseCommand.Parameters?.AddWithValue
                        (
                            C_HostMask,
                            hostParameter);

                    var itemParameter = string.Empty;
                    if (
                        (itemParameterTextBox != null)
                        )
                    {
                        itemParameter = itemParameterTextBox.Text;
                    }
                    if (string.IsNullOrWhiteSpace
                        (
                            itemParameter))
                    {
                        itemParameter = null;
                    }
                    databaseCommand.Parameters?.AddWithValue
                        (
                            C_ItemKeyMask,
                            itemParameter);

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
                            ZabbixHistoryBrowserForm.C_HostColumn);
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
                        ((readerFieldCount > hostIndex)
                          && (readerFieldCount > nameIndex)
                          && (readerFieldCount > keyIndex)
                          && (readerFieldCount > unitsIndex)
                          && (readerFieldCount > descriptionIndex)
                        )
                        ;

                    isEmpty = !isValidRecordSet;

                }

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
                        );
                    hostsItemsList.Add(hostsItem);
                }

                dataSetReader?.Close();

                databaseConnection?.Close();
            }

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

        private void RequestHistoryButton_Click(object sender, EventArgs e)
        {

            var itemsHistoriesList = new System.Collections.Generic.List<ItemsHistory>();

            var serverAddress = string.Empty;
            var connectionPort = string.Empty;
            var databaseName = string.Empty;
            var userId = string.Empty;
            var userPassword = string.Empty;
            

            var serverAddressTextBox = this.ServerAddressTextBox;
            var connectionPortTextBox = this.ConnectionsPortTextBox;
            var databaseNameTextBox = this.DatabaseNameTextBox;
            var userIdTextBox = this.UserIdTextBox;
            var userPasswordTextBox = this.UserPasswordTextBox;
            var reqestTimeoutTextBox = this.ReqestTimeoutTextBox;

            var historyHostFilterTextBox = this.HistoryHostFilterTextBox;
            var historyItemKeyFilterTextBox = this.HistoryItemKeyFilterTextBox;
            var historyFirstDayDateTimePicker = this.HistoryFirstDayDateTimePicker;
            var historyLastDayDateTimePicker = this.HistoryLastDayDateTimePicker;

            if ( ( serverAddressTextBox != null )
                 && ( connectionPortTextBox != null )
                 && ( databaseNameTextBox != null )
                 && ( userIdTextBox != null )
                 && ( userPasswordTextBox != null )
                  )
            {
                serverAddress = serverAddressTextBox.Text ;
                connectionPort = connectionPortTextBox.Text ;
                databaseName = databaseNameTextBox.Text ;
                userId = userIdTextBox.Text ;
                userPassword = userPasswordTextBox.Text ;
            }


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

                MySql.Data.MySqlClient.MySqlCommand databaseCommand = null ;

                if ( isConnectionOpen )
                {
                    databaseCommand = databaseConnection.CreateCommand ( ) ;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show
                        (
                            Properties.Resources.Connection_fail ) ;
                }

                MySql.Data.MySqlClient.MySqlDataReader dataSetReader = null ;

                if ( databaseCommand != null )
                {

                    var reqestTimeoutString = string.Empty ;
                    if ( reqestTimeoutTextBox != null )
                    {
                        reqestTimeoutString = reqestTimeoutTextBox.Text ;
                    }

                    double reqestTimeoutDouble ;
                    var isSuccessParse = double.TryParse
                        (
                            reqestTimeoutString ,
                            out reqestTimeoutDouble ) ;
                    if ( isSuccessParse )
                    {
                        var reqestTimeout = Convert.ToInt32
                                                (
                                                    reqestTimeoutDouble )
                                            * ZabbixHistoryBrowserForm.C_TimeoutConverRatio ;
                        databaseCommand.CommandTimeout = reqestTimeout ;
                    }

                    const string C_HostMask = "@HostMask" ;
                    const string C_ItemKeyMask = "@ItemKeyMask" ;
                    const string C_HistoryFirstDay = "@HistoryFirstDay" ;
                    const string C_HistoryLastDay = "@HistoryLastDay" ;

                    databaseCommand.CommandText =
                        $@"
SELECT
      FROM_UNIXTIME(HD.clock) AS '{ZabbixHistoryBrowserForm.C_DateColumn}'
    , HD.value AS '{ZabbixHistoryBrowserForm.C_ValueColumn}'
FROM
    history HD
WHERE
        EXISTS
        (
            SELECT
                NULL
            FROM
                items ITS
            WHERE
                HD.itemid = ITS.itemid
                AND ITS.key_ LIKE {C_ItemKeyMask}
                AND EXISTS(SELECT NULL FROM `hosts` HOS WHERE ITS.hostid = HOS.hostid AND HOS.host LIKE {C_HostMask} )
        )
    AND (
            {C_HistoryFirstDay} IS NULL
            OR {C_HistoryLastDay} IS NULL
            OR HD.clock BETWEEN UNIX_TIMESTAMP({C_HistoryFirstDay}) AND UNIX_TIMESTAMP({C_HistoryLastDay})
        )
UNION
SELECT
      FROM_UNIXTIME(HS.clock)
    , HS.value
FROM
    history_str HS
WHERE
        EXISTS
        (
            SELECT
                NULL
            FROM
                items ITS
            WHERE
                HS.itemid = ITS.itemid
                AND ITS.key_ LIKE {C_ItemKeyMask}
                AND EXISTS(SELECT NULL FROM `hosts` HOS WHERE ITS.hostid = HOS.hostid AND HOS.host LIKE {C_HostMask} )
        )
    AND 
        (
            {C_HistoryFirstDay} IS NULL
            OR {C_HistoryLastDay} IS NULL
            OR HS.clock BETWEEN UNIX_TIMESTAMP({C_HistoryFirstDay}) AND UNIX_TIMESTAMP({C_HistoryLastDay})
        )
UNION
SELECT
      FROM_UNIXTIME(HT.clock) AS DATETIME
    , HT.value
FROM
    history_text HT
WHERE
        EXISTS
        (
            SELECT
                NULL
            FROM
                items ITS
            WHERE
                HT.itemid = ITS.itemid
                AND ITS.key_ LIKE {C_ItemKeyMask}
                AND EXISTS(SELECT NULL FROM `hosts` HOS WHERE ITS.hostid = HOS.hostid AND HOS.host LIKE {C_HostMask} )
        )
    AND 
        (
            {C_HistoryFirstDay} IS NULL
            OR {C_HistoryLastDay} IS NULL
            OR HT.clock BETWEEN UNIX_TIMESTAMP({C_HistoryFirstDay}) AND UNIX_TIMESTAMP({C_HistoryLastDay})
        )
UNION
SELECT
      FROM_UNIXTIME(HU.clock) AS DATETIME
    , HU.value
FROM
    history_uint HU
WHERE
        EXISTS
        (
            SELECT
                NULL
            FROM
                items ITS
            WHERE
                HU.itemid = ITS.itemid
                AND ITS.key_ LIKE {C_ItemKeyMask}
                AND EXISTS(SELECT NULL FROM `hosts` HOS WHERE ITS.hostid = HOS.hostid AND HOS.host LIKE {C_HostMask} )
        )        
    AND 
        (
            {C_HistoryFirstDay} IS NULL
            OR {C_HistoryLastDay} IS NULL
            OR HU.clock BETWEEN UNIX_TIMESTAMP({C_HistoryFirstDay}) AND UNIX_TIMESTAMP({C_HistoryLastDay})
        )
LIMIT 1
;
" ;

                    var hostParameter = string.Empty ;
                    var itemParameter = string.Empty ;
                    DateTime? firstDayParameter = null ;
                    DateTime? lastDayParameter = null ;

                    if ( historyHostFilterTextBox != null )
                    {
                        hostParameter = historyHostFilterTextBox.Text ;
                    }
                    if ( historyItemKeyFilterTextBox != null )
                    {
                        itemParameter = historyItemKeyFilterTextBox.Text ;
                    }

                    if ( historyFirstDayDateTimePicker != null )
                    {
                        firstDayParameter = historyFirstDayDateTimePicker.Value.Date ;
                    }
                    if ( historyLastDayDateTimePicker != null )
                    {
                        lastDayParameter = historyLastDayDateTimePicker.Value.Date ;
                    }


                    if ( string.IsNullOrWhiteSpace
                        (
                            hostParameter ) )
                    {
                        hostParameter = null ;
                    }
                    databaseCommand.Parameters?.AddWithValue
                        (
                            C_HostMask ,
                            hostParameter ) ;

                    if ( string.IsNullOrWhiteSpace
                        (
                            itemParameter ) )
                    {
                        itemParameter = null ;
                    }
                    databaseCommand.Parameters?.AddWithValue
                        (
                            C_ItemKeyMask ,
                            itemParameter ) ;

                    databaseCommand.Parameters?.AddWithValue
                        (
                            C_HistoryFirstDay ,
                            firstDayParameter ) ;

                    databaseCommand.Parameters?.AddWithValue
                        (
                            C_HistoryLastDay ,
                            lastDayParameter ) ;

                    databaseCommand.Prepare ( ) ;
                    dataSetReader = databaseCommand.ExecuteReader ( ) ;
                }

                var hasRows = false ;

                if ( dataSetReader != null )
                {
                    hasRows = dataSetReader.HasRows ;
                }

                var dateIndex = 0 ;
                var valueIndex = 0 ;

                var isEmpty = true ;

                if ( hasRows )
                {
                    var readerFieldCount = dataSetReader.FieldCount ;

                    dateIndex = dataSetReader.GetOrdinal
                        (
                            ZabbixHistoryBrowserForm.C_DateColumn ) ;
                    valueIndex = dataSetReader.GetOrdinal
                        (
                            ZabbixHistoryBrowserForm.C_ValueColumn ) ;

                    var isValidRecordSet =
                        ( ( readerFieldCount > dateIndex)
                          && ( readerFieldCount > valueIndex)
                        )
                        ;

                    isEmpty = !isValidRecordSet ;

                }

                while ( !isEmpty )
                {

                    isEmpty = !dataSetReader.Read ( ) ;

                    var date = dataSetReader.GetDateTime 
                        (
                            dateIndex) ;
                    var value = dataSetReader.GetString
                        (
                            valueIndex) ;

                    var hostsItem = new ItemsHistory
                        (
                        date
                        , value
                        ) ;
                    itemsHistoriesList.Add
                        (
                            hostsItem ) ;
                }

                dataSetReader?.Close ( ) ;

                databaseConnection?.Close ( ) ;
            }

            var historyListDataGridView = this.HistoryListDataGridView;
            if ( historyListDataGridView != null )
            {
                historyListDataGridView.DataSource = null ;
                historyListDataGridView.Rows.Clear ( ) ;
                historyListDataGridView.MultiSelect = false ;
                historyListDataGridView.AutoGenerateColumns = true ;
                historyListDataGridView.DataSource = itemsHistoriesList ;
            }
        }
    }
}