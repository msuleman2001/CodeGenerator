using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CodeGenerator.Code;

class VBCodeGenerator
{
    CodeGeneration objCodeGen = new CodeGeneration();
    string strOutput = string.Empty;
    string strTableName = string.Empty;
    string strPrimaryKey = string.Empty;
    DataTable dtColumns;

    public VBCodeGenerator()
    {
        //blank constructor
    }

    public VBCodeGenerator(string strOutputFolderPath, string strTableName, DataTable dtColumnsList, string strPrimarKey)
    {
        dtColumns = new DataTable();
        dtColumns = dtColumnsList;
        strOutput = strOutputFolderPath;
        this.strTableName = strTableName;
        this.strPrimaryKey = strPrimarKey;
    }

    public void GenerateVBTableClass()
    {
        string strTemplate = string.Empty;
        string strTableClass = string.Empty;
        CodeGeneration objTemplate = new CodeGeneration();
        strTemplate = objTemplate.ReadTemplate(PublicEnums.TemplateName.VBTableClass);
        strTableClass = GenerateVBTableClass(strTemplate, strTableName, dtColumns);
        objTemplate.SaveClass(strTableClass, strOutput + @"\CodeClasses\VBClasses\TableClasses\" + strTableName + ".vb");
    }

    public void GenerateVBDataAccessClass()
    {
        string strTemplate = string.Empty;
        string strDataAccessClass = string.Empty;
        CodeGeneration objTemplate = new CodeGeneration();
        strTemplate = objTemplate.ReadTemplate(PublicEnums.TemplateName.VBDataAccessClass);
        strDataAccessClass = GenerateVBDataAccessClass(strTemplate, strTableName, dtColumns);
        objTemplate.SaveClass(strDataAccessClass, strOutput + @"\CodeClasses\VBClasses\DataAccessClasses\" + strTableName + ".vb");
    }

    public void GenerateVBBusinessLogicClass()
    {
        string strTemplate = string.Empty;
        string strDataAccessClass = string.Empty;
        CodeGeneration objTemplate = new CodeGeneration();
        strTemplate = objTemplate.ReadTemplate(PublicEnums.TemplateName.VBBusinessLogicClass);
        strDataAccessClass = GenerateVBBusinessLogicClass(strTemplate, strTableName, dtColumns);
        objTemplate.SaveClass(strDataAccessClass, strOutput + @"\CodeClasses\VBClasses\BusinessLogicClasses\" + strTableName + ".vb");
    }

    private string GenerateVBTableClass(string strTemplate, string strTableName, DataTable dtColumns)
    {
        DataTable dtTargetColumns = new DataTable();
        dtTargetColumns = objCodeGen.GetTargetColumnsList(dtColumns);
        string strVariablesDeclarationList = GenerateVariableDeclarationList(dtTargetColumns);
        strTemplate = strTemplate.Replace("__TableName", strTableName);
        strTemplate = strTemplate.Replace("__PrivateVariablesList", strVariablesDeclarationList);
        string strPropertiesList = GenerateVBPropertyList(dtColumns);
        strTemplate = strTemplate.Replace("__PropertiesList", strPropertiesList);
        return strTemplate;
    }

    private string GenerateVBDataAccessClass(string strTemplate, string strTableName, DataTable dtColumns)
    {
        DataTable dtTargetColumns = new DataTable();
        dtTargetColumns = objCodeGen.GetTargetColumnsList(dtColumns);
        strTemplate = strTemplate.Replace("__TableName", strTableName);
        strTemplate = strTemplate.Replace("__PK", strPrimaryKey);
        string strSPParametersList = GenerateSPParametersList(dtTargetColumns);
        string strFunctionParametersList = GenerateFunctionParametersList(dtTargetColumns);
        strTemplate = strTemplate.Replace("__AttachedParametersList", strSPParametersList);
        strTemplate = strTemplate.Replace("__ParametersList", strFunctionParametersList);
        return strTemplate;
    }

    private string GenerateVBBusinessLogicClass(string strTemplate, string strTableName, DataTable dtColumns)
    {
        DataTable dtTargetColumns = new DataTable();
        CodeGeneration objCodeGen = new CodeGeneration();
        dtTargetColumns = objCodeGen.GetTargetColumnsList(dtColumns);
        strTemplate = strTemplate.Replace("__TableName", strTableName);
        strTemplate = strTemplate.Replace("__PK", strPrimaryKey);
        string strObjectPropertiesList = objCodeGen.GenerateObjectPopertyList(strTableName, dtTargetColumns);
        strTemplate = strTemplate.Replace("__ObjectPropertiesList", strObjectPropertiesList);
        return strTemplate;
    }

    private string GenerateVariableDeclarationList(DataTable dtColumns)
    {
        string strVariablesList = string.Empty;
        CodeGeneration objCodeGen = new CodeGeneration();
        foreach (DataRow dr in dtColumns.Rows)
            strVariablesList += "Dim " + objCodeGen.GetVariableName(dr["DATA_TYPE"].ToString(), dr["COLUMN_NAME"].ToString()) + " As " + GetVBDataType(dr["DATA_TYPE"].ToString()) + "\n";

        return strVariablesList;
    }

    private string GenerateVBPropertyList(DataTable dtColumns)
    {
        string strPropertyList = string.Empty;
        CodeGeneration objCodeGen = new CodeGeneration();
        foreach (DataRow dr in dtColumns.Rows)
        {
            string strVariableName = objCodeGen.GetVariableName(dr["DATA_TYPE"].ToString(), dr["COLUMN_NAME"].ToString());
            string strPropertyTemplate = objCodeGen.ReadTemplate(PublicEnums.TemplateName.VBProperty);
            string strVBDataType = GetVBDataType(dr["DATA_TYPE"].ToString());
            strPropertyTemplate = strPropertyTemplate.Replace("__PropertyName", dr["COLUMN_NAME"].ToString());
            strPropertyTemplate = strPropertyTemplate.Replace("__VariableName", strVariableName);
            strPropertyTemplate = strPropertyTemplate.Replace("__DataType", strVBDataType);
            strPropertyList += strPropertyTemplate;
        }
        return strPropertyList;
    }//end function

    private string GetVBDataType(string strTypeName)
    {
        PublicEnums.DataTypes objDataType = 0;
        objDataType = objCodeGen.GetTypeByName((strTypeName));

        switch (objDataType)
        {
            case PublicEnums.DataTypes.BigInt:
            case PublicEnums.DataTypes.Numeric:
                return "Int64";
            case PublicEnums.DataTypes.SmallInt:
            case PublicEnums.DataTypes.Int:
                return "Int32";
            case PublicEnums.DataTypes.TinyInt:
                return "Int16";
            case PublicEnums.DataTypes.Decimal:
            case PublicEnums.DataTypes.Float:
            case PublicEnums.DataTypes.Money:
            case PublicEnums.DataTypes.Real:
            case PublicEnums.DataTypes.SmallMoney:
                return "Decimal";
            case PublicEnums.DataTypes.Char:
            case PublicEnums.DataTypes.NChar:
            case PublicEnums.DataTypes.NVarchar:
            case PublicEnums.DataTypes.Text:
            case PublicEnums.DataTypes.NText:
            case PublicEnums.DataTypes.XML:
                return "String";
            case PublicEnums.DataTypes.Bit:
                return "Boolean";
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
                return "Object";
        }//end switch
        return "";
    }//end function

    private string GenerateSPParametersList(DataTable dtColumns)
    {
        string strParametersList = string.Empty;
        CodeGeneration objCodeGen = new CodeGeneration();
        foreach (DataRow dr in dtColumns.Rows)
            strParametersList += @"objCommand.Parameters.AddWithValue(""@" + dr["COLUMN_NAME"].ToString() + @""", " + objCodeGen.GetVariableName(dr["DATA_TYPE"].ToString(), dr["COLUMN_NAME"].ToString()) + ")\n";

        return strParametersList;
    }

    private string GenerateFunctionParametersList(DataTable dtColumns)
    {
        string strList = string.Empty;
        CodeGeneration objCodeGen = new CodeGeneration();
        foreach (DataRow dr in dtColumns.Rows)
            strList += "ByVal " + objCodeGen.GetVariableName(dr["DATA_TYPE"].ToString(), dr["COLUMN_NAME"].ToString()) + " As " + GetVBDataType(dr["DATA_TYPE"].ToString()) + ", ";

        strList = strList.Remove(strList.LastIndexOf(","));
        return strList;
    }

}
