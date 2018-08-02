using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Data
{
    public class DatabaseInfo
    {
        private string datasource;
        private string port;
        private string database;
        private string user;
        private string password;

        public DatabaseInfo(string datasource, string port, string database, string user, string password)
        {
            this.datasource = datasource;
            this.port = port;
            this.database = database;
            this.user = user;
            this.password = password;
        }

        public string DataSource
        {
            get { return this.datasource; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    this.datasource = value;
            }
        }

        public string Port
        {
            get { return this.port; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                int portvalue = int.Parse(value);
                if (portvalue < 0 || portvalue > 65535)
                    return;

                this.port = value;
            }
        }

        public string Database
        {
            get { return this.database; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                this.database = value;
            }
        }

        public string User
        {
            get { return this.user; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                this.user = value;
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                this.password = value;
            }
        }

        public string GetConnectionString()
        {
            return string.Format("Data Source={0};Port={1};Database={2};User Id={3};Password={4};Charset=utf8", this.datasource, this.port, this.database, this.user, this.password);
        }
    }
}
