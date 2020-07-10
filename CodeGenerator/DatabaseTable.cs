using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public class DatabaseTable
    {
        //local variables//////////////////////////////////////////////////////////////////////////
        string strTableName = string.Empty;
        string strTableType = string.Empty;
        string strPrimaryKey = string.Empty;
        //private List<Column> objColumns = new List<Column>();
        ///////////////////////////////////////////////////////////////////////////////////////////

        //public properties////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or Set Table Name
        /// </summary>
        public string TableName
        {
            get
            {
                return strTableName;
            }
            set
            {
                strTableName = value;
            }
        }

        /// <summary>
        /// Set or get Primary Key of Table. For this system a table only contains one primary key with only numeric data type
        /// </summary>
        public string PrimaryKey
        {
            get { return strPrimaryKey; }
            set { strPrimaryKey = value; }
        }

        /// <summary>
        /// Get or set table type
        /// </summary>
        public string TableType
        {
            get { return strTableType; }
            set { strTableType = value; }
        }

        public List<Column> Columns { get; set; }
    }
}
