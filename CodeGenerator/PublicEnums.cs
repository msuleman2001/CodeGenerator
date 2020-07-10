using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CodeGenerator
{
    public class PublicEnums
    {
        public enum LanguageOption
        {
            CSharp = 1,
            VisualBasic = 2
        }

        public enum DataTypes
        {
            BigInt = 1,
            Binary = 2,
            Bit = 3,
            Char = 4,
            Date = 5,
            DateTime = 6,
            DateTime2 = 7,
            DateTimeOffset = 8,
            Decimal = 9,
            Float = 10,
            Geography = 11,
            Geometry = 12,
            HierarchyID = 13,
            Image = 14,
            Int = 15,
            Money = 16,
            NChar = 17,
            NText = 18,
            Numeric = 19,
            NVarchar = 20,
            Real = 21,
            SmallDateTime = 22,
            SmallInt = 23,
            SmallMoney = 24,
            SQL_Variant = 25,
            Text = 26,
            Time = 27,
            TimeStamp = 28,
            TinyInt = 29,
            UniqueIdentifier = 30,
            Varbinary = 31,
            Varchar = 32,
            XML = 33
        }

        public static string GetPrefix(string strTypeName)
        {
            string strPostfix = string.Empty;
            PublicEnums.DataTypes objDataType = 0;
            objDataType = GetTypeByName((strTypeName));
            switch (objDataType)
            {
                case PublicEnums.DataTypes.BigInt:
                case PublicEnums.DataTypes.Int:
                case PublicEnums.DataTypes.Numeric:
                case PublicEnums.DataTypes.SmallInt:
                case PublicEnums.DataTypes.TinyInt:
                    return "int";
                case PublicEnums.DataTypes.Decimal:
                case PublicEnums.DataTypes.Float:
                case PublicEnums.DataTypes.Money:
                case PublicEnums.DataTypes.Real:
                case PublicEnums.DataTypes.SmallMoney:
                    return "dbl";
                case PublicEnums.DataTypes.Char:
                case PublicEnums.DataTypes.NChar:
                case PublicEnums.DataTypes.Varchar:
                case PublicEnums.DataTypes.NVarchar:
                case PublicEnums.DataTypes.Text:
                case PublicEnums.DataTypes.NText:
                case PublicEnums.DataTypes.XML:
                    return "str";
                case PublicEnums.DataTypes.Bit:
                    return "bln";
                case PublicEnums.DataTypes.Date:
                case PublicEnums.DataTypes.DateTime:
                case PublicEnums.DataTypes.DateTime2:
                case PublicEnums.DataTypes.DateTimeOffset:
                case PublicEnums.DataTypes.SmallDateTime:
                case PublicEnums.DataTypes.Time:
                case PublicEnums.DataTypes.TimeStamp:
                    return "dat";
                case PublicEnums.DataTypes.Geography:
                case PublicEnums.DataTypes.Geometry:
                case PublicEnums.DataTypes.HierarchyID:
                case PublicEnums.DataTypes.Image:
                case PublicEnums.DataTypes.SQL_Variant:
                case PublicEnums.DataTypes.Binary:
                case PublicEnums.DataTypes.Varbinary:
                case PublicEnums.DataTypes.UniqueIdentifier:
                    return "obj";
            }
            return "";
        }

        public static PublicEnums.DataTypes GetTypeByName(string strTypeName)
        {
            switch (strTypeName)
            {
                case "bigint":
                    return PublicEnums.DataTypes.BigInt;
                case "binary":
                    return PublicEnums.DataTypes.Binary;
                case "bit":
                    return PublicEnums.DataTypes.Bit;
                case "char":
                    return PublicEnums.DataTypes.Char;
                case "date":
                    return PublicEnums.DataTypes.Date;
                case "datetime":
                    return PublicEnums.DataTypes.DateTime;
                case "datetime2(7)":
                    return PublicEnums.DataTypes.DateTime2;
                case "datetimeoffset(7)":
                    return PublicEnums.DataTypes.DateTimeOffset;
                case "decimal":
                    return PublicEnums.DataTypes.Decimal;
                case "float":
                    return PublicEnums.DataTypes.Float;
                case "geography":
                    return PublicEnums.DataTypes.Geography;
                case "geometry":
                    return PublicEnums.DataTypes.Geometry;
                case "hierarchyid":
                    return PublicEnums.DataTypes.HierarchyID;
                case "image":
                    return PublicEnums.DataTypes.Image;
                case "int":
                    return PublicEnums.DataTypes.Int;
                case "money":
                    return PublicEnums.DataTypes.Money;
                case "nchar":
                    return PublicEnums.DataTypes.NChar;
                case "ntext":
                    return PublicEnums.DataTypes.NText;
                case "numeric":
                case "numeric() identity":
                    return PublicEnums.DataTypes.Numeric;
                case "nvarchar":
                    return PublicEnums.DataTypes.NVarchar;
                case "real":
                    return PublicEnums.DataTypes.Real;
                case "smalldatetime":
                    return PublicEnums.DataTypes.SmallDateTime;
                case "smallint":
                    return PublicEnums.DataTypes.SmallInt;
                case "smallmoney":
                    return PublicEnums.DataTypes.SmallMoney;
                case "sql_variant":
                    return PublicEnums.DataTypes.SQL_Variant;
                case "text":
                    return PublicEnums.DataTypes.Text;
                case "time":
                    return PublicEnums.DataTypes.Time;
                case "timestamp":
                    return PublicEnums.DataTypes.TimeStamp;
                case "tinyint":
                    return PublicEnums.DataTypes.TinyInt;
                case "uniqueidentifier":
                    return PublicEnums.DataTypes.UniqueIdentifier;
                case "varbinary":
                    return PublicEnums.DataTypes.Varbinary;
                case "varchar":
                    return PublicEnums.DataTypes.Varchar;
                case "xml":
                    return PublicEnums.DataTypes.XML;
                default:
                    return 0;
            }
        }
    }
}
