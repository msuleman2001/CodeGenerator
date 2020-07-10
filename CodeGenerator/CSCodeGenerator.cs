using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CodeGenerator
{
    public class CSCodeGenerator
    {
        string cs_template_path = string.Empty;
        DatabaseTable table = new DatabaseTable();
        string model_template = string.Empty;
        string controller_template = string.Empty;
        string cs_model_class = string.Empty;
        string cs_controller_class = string.Empty;

        public CSCodeGenerator(string template_path, DatabaseTable db_table)
        {
            cs_template_path = template_path;
            model_template = File.ReadAllText(cs_template_path + "ModelClass.tem");
            controller_template = File.ReadAllText(cs_template_path + "ControllerClass.tem");
            table = db_table;
        }//end constructor

        public string GenerateModelClass()
        {
            cs_model_class = model_template;
            string private_variables = GeneratePrivateVariables();
            cs_model_class = cs_model_class.Replace("__PrivateVariablesList", private_variables);

            string public_properties = GeneratePublicProperties();
            cs_model_class = cs_model_class.Replace("__PropertiesList", public_properties);

            string attached_parameters = GenerateAttachedParameters();
            cs_model_class = cs_model_class.Replace("__AttachedParametersList", attached_parameters);

            string db_to_model_list = GenerateDatabaseToModel();
            cs_model_class = cs_model_class.Replace("__DatabaseToPropertiesList", db_to_model_list);

            cs_model_class = cs_model_class.Replace("__TableName", table.TableName);
            cs_model_class = cs_model_class.Replace("__PK", table.PrimaryKey);

            return cs_model_class;
        }

        public string GenerateControllerClass()
        {
            cs_controller_class = controller_template;
            cs_controller_class = cs_controller_class.Replace("__TableName", table.TableName);
            cs_controller_class = cs_controller_class.Replace("__PK", table.PrimaryKey);
            return cs_controller_class;   
        }

        private string GeneratePrivateVariables()
        {
            string variable_list = string.Empty; ;
            foreach (Column column in table.Columns)
            {
                string variable_name = string.Empty;
                variable_name = GetCSDataType(column.TypeName);
                variable_name += " " + PublicEnums.GetPrefix(column.TypeName);
                variable_name += column.ColumnName;
                variable_name += ";";
                variable_list += variable_name + Environment.NewLine;
            }
            return variable_list;
        }

        private string GeneratePublicProperties()
        {
            string property_list = string.Empty;
            string property_template = File.ReadAllText(cs_template_path + "Property.tem");
            
            foreach (Column column in table.Columns)
            {
                string public_property = property_template;
                string data_type = GetCSDataType(column.TypeName);
                string property_name = column.ColumnName;
                string variable_name = PublicEnums.GetPrefix(column.TypeName) + column.ColumnName;

                public_property = public_property.Replace("__DataType", data_type);
                public_property = public_property.Replace("__PropertyName", property_name);
                public_property = public_property.Replace("__VariableName", variable_name);
                public_property += Environment.NewLine;
                property_list += public_property;
            }
            return property_list;
        }

        private string GenerateAttachedParameters()
        {
            string attached_parameters = string.Empty;

            foreach (Column column in table.Columns)
            {
                string variable_name = PublicEnums.GetPrefix(column.TypeName) + column.ColumnName;
                attached_parameters += "com" + column.TableName + ".Parameters.AddWithValue" + "(\"" + column.ColumnName + "\", " + variable_name + ");" + Environment.NewLine;
            }
            return attached_parameters;
        }

        private string GenerateDatabaseToModel()
        {
            string db_to_property_list = string.Empty;
            foreach (Column column in table.Columns)
            {
                string db_to_property = "obj" + column.TableName + "." + column.ColumnName + " = Convert.To" + GetConvertorName(column.TypeName) + "(dr__TableName[\"" + column.ColumnName + "\"]);" + Environment.NewLine;
                db_to_property_list += db_to_property;
            }
            return db_to_property_list;
        }

        private string GetCSDataType(string strTypeName)
        {
            PublicEnums.DataTypes objDataType = 0;
            objDataType = PublicEnums.GetTypeByName((strTypeName));

            switch (objDataType)
            {
                case PublicEnums.DataTypes.BigInt:
                case PublicEnums.DataTypes.Numeric:
                    return "Int64";
                case PublicEnums.DataTypes.SmallInt:
                case PublicEnums.DataTypes.Int:
                    return "int";
                case PublicEnums.DataTypes.TinyInt:
                    return "Int16";
                case PublicEnums.DataTypes.Decimal:
                case PublicEnums.DataTypes.Float:
                case PublicEnums.DataTypes.Money:
                case PublicEnums.DataTypes.Real:
                case PublicEnums.DataTypes.SmallMoney:
                    return "float";
                case PublicEnums.DataTypes.Char:
                case PublicEnums.DataTypes.NChar:
                case PublicEnums.DataTypes.NVarchar:
                case PublicEnums.DataTypes.Varchar:
                case PublicEnums.DataTypes.Text:
                case PublicEnums.DataTypes.NText:
                case PublicEnums.DataTypes.XML:
                    return "string";
                case PublicEnums.DataTypes.Bit:
                    return "bool";
                case PublicEnums.DataTypes.Date:
                case PublicEnums.DataTypes.DateTime:
                case PublicEnums.DataTypes.DateTime2:
                case PublicEnums.DataTypes.DateTimeOffset:
                case PublicEnums.DataTypes.SmallDateTime:
                case PublicEnums.DataTypes.Time:
                case PublicEnums.DataTypes.TimeStamp:
                    return "DateTime";
                case PublicEnums.DataTypes.Geography:
                case PublicEnums.DataTypes.Geometry:
                case PublicEnums.DataTypes.HierarchyID:
                case PublicEnums.DataTypes.Image:
                case PublicEnums.DataTypes.SQL_Variant:
                case PublicEnums.DataTypes.Binary:
                case PublicEnums.DataTypes.Varbinary:
                case PublicEnums.DataTypes.UniqueIdentifier:
                    return "object";
            }//end switch
            return "";
        }

        private string GetConvertorName(string type_name)
        {
            switch (type_name)
            {
                case "string":
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                case "text":
                case "ntext":
                case "xml":
                    return "String";
                case "bit":
                    return "Boolean";
                case "int":
                case "smallint":
                    return "Int32";
                case "tinyint":
                    return "Int16";
                case "decimal":
                case "real":
                case "money":
                case "smallmoney":
                    return "Decimal";
                case "float":
                    return "Single";
                case "numeric() identity":
                case "numeric":
                case "bigint":
                    return "Int64";
                case "datetime":
                case "date":
                case "datetime2":
                case "datetimeoffset":
                case "time":
                case "timestamp":
                    return "DateTime";
            }
            return "";
        }

        //depricated
        private string GenerateCSDataAccessClass(string strTemplate, string strTableName, DataTable dtColumns)
        {
            //DataTable dtTargetColumns = new DataTable();
            //dtTargetColumns = objCodeGen.GetTargetColumnsList(dtColumns);
            //strTemplate = strTemplate.Replace("__TableName", strTableName);
            //strTemplate = strTemplate.Replace("__PK", strPrimaryKey);
            //string strSPParametersList = GenerateSPParametersList(dtTargetColumns, strTableName);
            //string strFunctionParametersList = GenerateFunctionParametersList(dtTargetColumns);
            //strTemplate = strTemplate.Replace("__AttachedParametersList", strSPParametersList);
            //strTemplate = strTemplate.Replace("__ParametersList", strFunctionParametersList);
            return strTemplate;
        }

        //depricated
        private string GenerateCSTableClass(string strTemplate, string strTableName, DataTable dtColumns)
        {
            //DataTable dtTargetColumns = new DataTable();
            //dtTargetColumns = objCodeGen.GetTargetColumnsList(dtColumns);
            //string strVariablesDeclarationList = GenerateVariableDeclarationList(dtTargetColumns);
            //strTemplate = strTemplate.Replace("__TableName", strTableName);
            //strTemplate = strTemplate.Replace("__PrivateVariablesList", strVariablesDeclarationList);
            //string strPropertiesList = GenerateCSPropertyList(dtColumns);
            //strTemplate = strTemplate.Replace("__PropertiesList", strPropertiesList);
            return strTemplate;
        }

        //depricated
        private string GenerateCSBusinessLogicClasses(string strTemplate, string strTableName, DataTable dtColumns)
        {
            //DataTable dtTargetColumns = new DataTable();
            //dtTargetColumns = objCodeGen.GetTargetColumnsList(dtColumns);
            //strTemplate = strTemplate.Replace("__TableName", strTableName);
            //strTemplate = strTemplate.Replace("__PK", strPrimaryKey);
            //string strObjectPropertiesList = objCodeGen.GenerateObjectPopertyList(strTableName, dtTargetColumns);
            //strTemplate = strTemplate.Replace("__ObjectPropertiesList", strObjectPropertiesList);
            return strTemplate;
        }

        //depricated
        private string GenerateVariableDeclarationList(DataTable dtColumns)
        {
            string strVariablesList = string.Empty;
            //CodeGeneration objCodeGen = new CodeGeneration();

            //foreach (DataRow dr in dtColumns.Rows)
            //    strVariablesList += GetCSDataType(dr["DATA_TYPE"].ToString()) + " " + objCodeGen.GetVariableName(dr["DATA_TYPE"].ToString(), dr["COLUMN_NAME"].ToString()) + ";\n";

            return strVariablesList;
        }

        //depricated
        private string GenerateCSPropertyList(DataTable dtColumns)
        {
            string strPropertyList = string.Empty;
            //CodeGeneration objCodeGen = new CodeGeneration();
            //foreach (DataRow dr in dtColumns.Rows)
            //{
            //    string strVariableName = objCodeGen.GetVariableName(dr["DATA_TYPE"].ToString(), dr["COLUMN_NAME"].ToString());
            //    string strPropertyTemplate = objCodeGen.ReadTemplate(PublicEnums.TemplateName.CSProperty);
            //    string strVBDataType = GetCSDataType(dr["DATA_TYPE"].ToString());
            //    strPropertyTemplate = strPropertyTemplate.Replace("__PropertyName", dr["COLUMN_NAME"].ToString());
            //    strPropertyTemplate = strPropertyTemplate.Replace("__VariableName", strVariableName);
            //    strPropertyTemplate = strPropertyTemplate.Replace("__DataType", strVBDataType);
            //    strPropertyList += "\n" + strPropertyTemplate;
            //}
            return strPropertyList;
        }

        //depricated
        private string GenerateSPParametersList(DataTable dtColumns, string strTableName)
        {
            string strParametersList = string.Empty;

            //CodeGeneration objCodeGen = new CodeGeneration();
            //foreach (DataRow dr in dtColumns.Rows)
            //    strParametersList += @"com" + strTableName + ".Parameters.AddWithValue(\"" + dr["COLUMN_NAME"].ToString() + @""", " + objCodeGen.GetVariableName(dr["DATA_TYPE"].ToString(), dr["COLUMN_NAME"].ToString()) + ");\n";

            return strParametersList;
        }

        //depricated
        private string GenerateFunctionParametersList(DataTable dtColumns)
        {
            string strList = string.Empty;
            //CodeGeneration objCodeGen = new CodeGeneration();
            //foreach (DataRow dr in dtColumns.Rows)
            //    strList += GetCSDataType(dr["DATA_TYPE"].ToString()) + " " + objCodeGen.GetVariableName(dr["DATA_TYPE"].ToString(), dr["COLUMN_NAME"].ToString()) + ", ";

            strList = strList.Remove(strList.LastIndexOf(","));
            return strList;
        }
    }
}