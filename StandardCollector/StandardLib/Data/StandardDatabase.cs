using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Standard.Data
{
    public sealed class StandardDatabase : IDisposable
    {
        private DatabaseInfo info;
        private MySqlConnection connection;

        public event StateChangeEventHandler StateChange;

        public StandardDatabase()
        {
        }

        private StandardDatabase(DatabaseInfo info)
        {
            this.connection = new MySqlConnection(info.GetConnectionString());
            this.connection.StateChange += Connection_StateChange;
        }

        public static StandardDatabase Create(DatabaseInfo info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            return new StandardDatabase(info);
        }

        private void Connection_StateChange(object sender, StateChangeEventArgs e)
        {
            this.OnStateChange(e);
        }

        public DatabaseInfo DatabaseInfo
        {
            get
            {
                return this.info;
            }
            set
            {
                if (this.connection != null && this.connection.State == ConnectionState.Open)
                    throw new InvalidOperationException("数据库已打开，无法更改数据库。");

                if (value == null)
                    throw new ArgumentNullException("value");

                if (this.connection != null)
                    this.connection.StateChange -= Connection_StateChange;

                this.info = value;
            }
        }

        public MySqlConnection Connection
        {
            get
            {
                return this.connection;
            }
        }

        public ConnectionState State
        {
            get
            {
                if (this.connection == null)
                    return ConnectionState.Closed;

                return this.connection.State;
            }
        }

        public void Open()
        {
            if (this.connection == null)
            { 
                this.connection = new MySqlConnection(this.info.GetConnectionString());
                this.connection.StateChange += Connection_StateChange;
            }

            if (this.connection.State != ConnectionState.Open)
                this.connection.Open();
        }

        public void Close()
        {
            if (this.connection == null)
                return;

            this.connection.Close();
            this.connection.StateChange -= Connection_StateChange;
            this.connection.Dispose();
            this.connection = null;
        }

        public void Dispose()
        {
            if (this.connection != null)
            {
                this.connection.StateChange -= Connection_StateChange;
                this.connection.Dispose();
            }
        }

        public DataTable GetSchema()
        {
            if (this.connection == null)
                return new DataTable();

            return this.connection.GetSchema();
        }

        private void OnStateChange(StateChangeEventArgs stateChange)
        {
            if (this.StateChange != null)
                this.StateChange(this, stateChange);
        }

        public DataTable GetDatabaseSchema(string catalog, string databaseName, string tableName, string tableType)
        {
            if (this.connection == null)
                return new DataTable();

            string[] restrictions = new string[4];
            restrictions[0] = catalog;                  // 表所在的Catalog
            restrictions[1] = databaseName;             // 表的所有者（数据库）
            restrictions[2] = tableName;                // 表的名字
            restrictions[3] = tableType;                // 表的类型
            DataTable table = this.connection.GetSchema("Tables", restrictions);
            return table;
        }

        public DataTable GetTableSchema(string catalog, string databaseName, string tableName, string columnName)
        {
            if (this.connection == null)
                return new DataTable();

            string[] restrictions = new string[4];
            restrictions[0] = catalog;                  // 列所在的Catalog
            restrictions[1] = databaseName;             // 列的所有者
            restrictions[2] = tableName;                // 列所在的表的名字
            restrictions[3] = columnName;               // 表示列名
            DataTable table = this.connection.GetSchema("Columns", restrictions);
            return table;
        }

        public DataTable RetrieveTable(string tableName, int rowLimit)
        {
            DataTable table = new DataTable();

            if (this.connection == null)
                return table;

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = string.Format("SELECT * FROM {0}{1}", tableName, this.GetRowLimitText(rowLimit));
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }

        public DataTable QueryStandardsInfo(string standardNo, string standardName)
        {
            DataTable table = new DataTable();

            if (this.connection == null)
                return table;


            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = @"SELECT standardNo, standardName, englishName, state FROM standardlib WHERE (position(replace(@std_no, ' ', '') IN replace(standardNo,' ','')) > 0) AND (position(@std_name IN standardName) > 0) 
UNION 
SELECT stdNo, name, englishName, state FROM csres_standard WHERE (position(replace(@std_no, ' ', '') IN replace(stdNo,' ','')) > 0) AND (position(@std_name IN name) > 0)     
ORDER BY 1;";
            command.Parameters.Add(new MySqlParameter("@std_no", standardNo));
            command.Parameters.Add(new MySqlParameter("@std_name", standardName));
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }

        private string GetRowLimitText(int rowLimit)
        {
            if (rowLimit == -1)
                return string.Empty;

            return string.Format(" LIMIT {0}", rowLimit);
        }
    }
}
