using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public class Column
    {

        //properties///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or Set Column Name
        /// </summary>
        public string ColumnName { get; set; } //end ColumnName

        public bool IsPrimaryKey { get; set; }

        public string TypeName { get; set; }//end TypeName

        public int DataType { get; set; }

        public string TableName { get; set; }//end TableName

        public int Precision { get; set; }//end Precision

        public int NumericPercision { get; set; }

        public string NumericScale { get; set; }

        public int Length { get; set; }//end Length

        public int OrdinalPosition { get; set; }

        public int CharacterMaximumLength { get; set; }

        public bool IsNull { get; set; }//end IsNull
    }//end class
}
