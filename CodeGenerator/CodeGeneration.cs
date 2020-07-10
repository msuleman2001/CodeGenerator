using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace CodeGenerator
{
    public class CodeGeneration
    {
        
        const string VB_CLASS_PATH = @"Templates\Classes\VB\";
        const string CS_CLASS_PATH = @"Templates\Classes\CS\";

        private string strDBName;
        private string strOutputPath;
        private List<DatabaseTable> objTableList;
        private PublicEnums.LanguageOption enSelectedLanguage;
        private List<string> stored_procedures = new List<string>();
        private string strSelectedSP;
        private string strMVCOPtions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="table_list">List of database tables</param>
        /// <param name="language">Programming language. C# or VB</param>
        /// <param name="sp_options">List of stored procedures such as SelectAll, SelecByID etc. This list is comma seperated</param>
        public CodeGeneration(List<DatabaseTable> table_list, PublicEnums.LanguageOption language, string sp_options, List<string> sp_list)
        {
            objTableList = table_list;
            enSelectedLanguage = language;
            strSelectedSP = sp_options;
            stored_procedures = sp_list;
        }

        /// <summary>
        /// Generate stored procedures and combine them into single string
        /// </summary>
        /// <returns></returns>
        public string GenerateProcedures()
        {
            string strStoredProcedureList = string.Empty;
            StoredProcedureGenerator objSPGen = new StoredProcedureGenerator();
            foreach (DatabaseTable db_table in objTableList)
                strStoredProcedureList += objSPGen.GenerateStoredProcedure(db_table, strSelectedSP, stored_procedures);

            return strStoredProcedureList;
        }

        public List<ModelClass> GenerateModelClasses()
        {
            List<ModelClass> model_classes = new List<ModelClass>();
            
            foreach (DatabaseTable db_table in objTableList)
            {
                ModelClass model_class = new ModelClass();

                switch (enSelectedLanguage)
                {
                    case PublicEnums.LanguageOption.CSharp:
                        CSCodeGenerator objCSCodeGen = new CSCodeGenerator(CS_CLASS_PATH, db_table);
                        string class_code = objCSCodeGen.GenerateModelClass();
                        string class_name = db_table.TableName;
                        model_class.ClassName = class_name;
                        model_class.ClassCode = class_code;
                        break;
                    case PublicEnums.LanguageOption.VisualBasic:
                        
                        break;
                }

                model_classes.Add(model_class);
            }

            return model_classes;
        }

        public List<ControllerClass> GenerateControllerClasses()
        {
            List<ControllerClass> contoller_classes = new List<ControllerClass>();

            foreach (DatabaseTable db_table in objTableList)
            {
                ControllerClass controller_class = new ControllerClass();

                switch (enSelectedLanguage)
                {
                    case PublicEnums.LanguageOption.CSharp:
                        CSCodeGenerator objCSCodeGen = new CSCodeGenerator(CS_CLASS_PATH, db_table);
                        string class_code = objCSCodeGen.GenerateControllerClass();
                        string class_name = db_table.TableName;
                        controller_class.ClassName = class_name;
                        controller_class.ClassCode = class_code;
                        break;
                    case PublicEnums.LanguageOption.VisualBasic:

                        break;
                }
                contoller_classes.Add(controller_class);
            }
            return contoller_classes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strClass"></param>
        /// <param name="strPath"></param>
        public void SaveClass(string strClass, string strPath)
        {
            File.WriteAllText(strPath, strClass);
        }

        /*public string GetPostfix(string strTypeName)
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
        }*/

        public string GetVariableName(string strDataType, string strVariableName)
        {
            string strPostfix = "";// GetPostfix(strDataType);
            return strPostfix + strVariableName;
        }

        public string GenerateObjectPopertyList(string strTableName, DataTable dtColumns)
        {
            string strList = string.Empty;
            foreach (DataRow dr in dtColumns.Rows)
                strList += "obj" + strTableName + "." + dr["COLUMN_NAME"].ToString() + ",";
            strList = strList.Remove(strList.LastIndexOf(","));
            return strList;
        }
    }
}
