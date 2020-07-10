using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CodeGenerator
{
    class StoredProcedureGenerator
    {
        const string SP_TEMPLATE_PATH = @"Templates\StoredProcedures\";

        public string GenerateStoredProcedure(DatabaseTable db_table, string sp_options, List<string> stored_procs)
        {
            string sp_table_list = "";
            foreach (string sp_option in sp_options.Split(','))
            {
                string sp_name = db_table.TableName + sp_option;
                
                string sp_template = File.ReadAllText(SP_TEMPLATE_PATH + sp_option + ".tem");
                string stored_procedure = "";
                
                stored_procedure = sp_template.Replace("__TableName", db_table.TableName); //setting table name in stored procedure template
                stored_procedure = stored_procedure.Replace("__PK", db_table.PrimaryKey);  //setting primary key in stored procedure

                if (stored_procs.Contains(sp_name))
                    stored_procedure = stored_procedure.Replace("__CREATEORALTER", "ALTER");
                else
                    stored_procedure = stored_procedure.Replace("__CREATEORALTER", "CREATE");

                if (sp_option.Contains("Insert"))
                {
                    string strSignatureColumnList = GetSignatureList(db_table);
                    string strKeyValueColumns = GetKeyValueList(db_table);
                    string strColumnValueList = GetColumnValueList(db_table);
                    stored_procedure = stored_procedure.Replace("__SignatureColumnList", strSignatureColumnList);
                    stored_procedure = stored_procedure.Replace("__KeyValueColumns", strKeyValueColumns);
                    stored_procedure = stored_procedure.Replace("__ValueList", strColumnValueList);
                }
                sp_table_list += Environment.NewLine + stored_procedure;
            }

            return sp_table_list;
        }

        private string GetSignatureList(DatabaseTable dt_table)
        {
            string strSignatureList = string.Empty;
            foreach (Column col in dt_table.Columns)
            {
                if (col.OrdinalPosition == 1)
                    continue;

                PublicEnums.DataTypes objSignatureColumn = 0;
                objSignatureColumn = GetTypeByName((string)col.TypeName);
                string strSignatureColumn = string.Empty;
                switch (objSignatureColumn)
                {
                    case PublicEnums.DataTypes.BigInt:
                    case PublicEnums.DataTypes.Bit:
                    case PublicEnums.DataTypes.Date:
                    case PublicEnums.DataTypes.DateTime:
                    case PublicEnums.DataTypes.Float:
                    case PublicEnums.DataTypes.Geography:
                    case PublicEnums.DataTypes.Geometry:
                    case PublicEnums.DataTypes.HierarchyID:
                    case PublicEnums.DataTypes.Image:
                    case PublicEnums.DataTypes.Int:
                    case PublicEnums.DataTypes.Money:
                    case PublicEnums.DataTypes.Real:
                    case PublicEnums.DataTypes.NText:
                    case PublicEnums.DataTypes.SmallDateTime:
                    case PublicEnums.DataTypes.SmallInt:
                    case PublicEnums.DataTypes.SmallMoney:
                    case PublicEnums.DataTypes.SQL_Variant:
                    case PublicEnums.DataTypes.Text:
                    case PublicEnums.DataTypes.TimeStamp:
                    case PublicEnums.DataTypes.TinyInt:
                    case PublicEnums.DataTypes.UniqueIdentifier:
                    case PublicEnums.DataTypes.XML:
                        strSignatureColumn = "@" + col.ColumnName + " " + col.TypeName.ToUpper();
                        break;

                    case PublicEnums.DataTypes.Binary:
                    case PublicEnums.DataTypes.Char:
                    case PublicEnums.DataTypes.NChar:
                        strSignatureColumn = "@" + col.ColumnName + " " + col.TypeName.ToUpper() + "(" + col.Precision.ToString() + ")";
                        break;

                    case PublicEnums.DataTypes.DateTime2:
                    case PublicEnums.DataTypes.DateTimeOffset:
                    case PublicEnums.DataTypes.Time:
                        strSignatureColumn = "@" + col.ColumnName + " " + col.TypeName.ToUpper() + "(" + col.NumericPercision.ToString() + ")";
                        break;

                    case PublicEnums.DataTypes.Decimal:
                    case PublicEnums.DataTypes.Numeric:
                        strSignatureColumn = "@" + col.ColumnName + " " + col.TypeName.ToUpper() + "(" + col.Precision + ", " + col.NumericScale.ToString() + ")";
                        break;

                    case PublicEnums.DataTypes.NVarchar:
                    case PublicEnums.DataTypes.Varbinary:
                    case PublicEnums.DataTypes.Varchar:
                        if (col.CharacterMaximumLength == -1)
                            strSignatureColumn = "@" + col.ColumnName + " " + col.TypeName.ToUpper() + "(MAX)";
                        else
                            strSignatureColumn = "@" + col.ColumnName + " " + col.TypeName.ToUpper() + "(" + col.Precision + ")";
                        break;

                    default:
                        strSignatureColumn = string.Empty;
                        break;
                }//end switch
                if (strSignatureColumn == string.Empty)
                    return string.Empty;

                strSignatureList += strSignatureColumn + ", ";
            }//end for
            strSignatureList = strSignatureList.Remove(strSignatureList.LastIndexOf(","));
            return strSignatureList;
        }

        private string GetKeyValueList(DatabaseTable dt_table)
        {
            string strKeyValueList = string.Empty;
            foreach (Column col in dt_table.Columns)
            {
                if (col.OrdinalPosition == 1)
                    continue;

                strKeyValueList += col.ColumnName + " = @" + col.ColumnName + ", ";
            }
            strKeyValueList = strKeyValueList.Remove(strKeyValueList.LastIndexOf(","));
            return strKeyValueList;
        }

        private string GetColumnValueList(DatabaseTable dt_table)
        {
            string strValueColumnList = string.Empty;
            foreach (Column col in dt_table.Columns)
            {
                if (col.OrdinalPosition == 1)
                    continue;

                strValueColumnList += "@" + col.ColumnName + ", ";
            }
            strValueColumnList = strValueColumnList.Remove(strValueColumnList.LastIndexOf(","));
            return strValueColumnList;
        }

        public PublicEnums.DataTypes GetTypeByName(string strTypeName)
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

        //depricated
        private string GetSimpleColumnList(DatabaseTable dtColumns)
        {
            string strSimpleColumnList = string.Empty;
            //foreach (DataRow dr in dtColumns.Rows)
            //{
            //    if (dr["ORDINAL_POSITION"].ToString() == "1")
            //        continue;

            //    strSimpleColumnList += (string)dr["COLUMN_NAME"] + ", ";
            //}
            //strSimpleColumnList = strSimpleColumnList.Remove(strSimpleColumnList.LastIndexOf(","));
            return strSimpleColumnList;
        }

    }
}
