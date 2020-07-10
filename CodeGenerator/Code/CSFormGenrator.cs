using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;


class CSFormGenrator
{
    DataTable objTablesList;
    DataTable objColumnsList;

    string strFormDesignerTempPath = @"..\..\Templates\FormDesigner\WinForms\CS\";

    public void GenerateFormDesignerClass(string strFormType, string strNameSpace, string strConnectionString, string strFormClassOutputPath, string[] arrayMasterFields, string[] arrayDetailFields)
    {
          
        int strLableLocX = 25;
        int strLableTextboxLocY = 25;
        int strTabIndex = 0;
        int strTextboxLocX = 110;

        string strDesignerFormName = string.Empty;

        string strLableInitializer = string.Empty;
        string strLableProperties = string.Empty;
        string strAddLableControl = string.Empty;
        string strLableController = string.Empty;
        string strTextBoxInitializer = string.Empty;
        string strTextBoxProperties = string.Empty;
        string strAddTextBoxControl = string.Empty;
        string strTextBoxController = string.Empty;
        string strFormDesignerClass = string.Empty;

        if (strFormType == "DataEntry")
        {

            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            DataTable objTablesList = new DataTable();
            objTablesList = objDBOperation.SelectTables();
            DataTable objColumnsList = new DataTable();
            ///////////////// Select tables in Db //////////////////////
            foreach (DataRow dr in objTablesList.Rows)
            {
                ///////////////"TABLE_NAME" are tables name in DB//////////
                objColumnsList = new DataTable();
                objColumnsList = objDBOperation.SelectColumns(dr["TABLE_NAME"].ToString());
                string strColumnsList = string.Empty;

                if ((string)dr["TABLE_OWNER"] == "dbo")
                {
                    strDesignerFormName = dr["TABLE_NAME"].ToString();

                    foreach (DataRow dc in objColumnsList.Rows)
                    {
                        if ((string)dc["TABLE_SCHEMA"] == "dbo")
                        {
                            strLableInitializer += " this.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + " = new System.Windows.Forms.Label();\n";
                            strLableProperties += "//lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + "//" + "\n this.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".AutoSize = true;\n this.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Location = new System.Drawing.Point(" + strLableLocX + " , " + strLableTextboxLocY + "); \n this.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Name =" + '"' + "lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + '"' + ";\n this.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Size = new System.Drawing.Size(47, 13);\n this.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".TabIndex = " + strTabIndex + ";\n this.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Text =" + '"' + dc["COLUMN_NAME"] + '"' + ";\n\n";
                            strAddLableControl += "this.Controls.Add(this.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ");\n";
                            strLableController += " private System.Windows.Forms.Label  lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ";\n";
                            strTabIndex = strTabIndex + 1;

                            strTextBoxInitializer += " this.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + " = new System.Windows.Forms.TextBox();\n";
                            strTextBoxProperties += "//txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + "//" + "\n this.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".AutoSize = true;\n this.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Location = new System.Drawing.Point(" + strTextboxLocX + " , " + strLableTextboxLocY + "); \n this.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Name =" + '"' + "txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + '"' + ";\n this.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Size = new System.Drawing.Size(100, 13);\n this.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".TabIndex = " + strTabIndex + ";\n this.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Text =" + '"' + dc["COLUMN_NAME"] + '"' + ";\n\n";
                            strAddTextBoxControl += "this.Controls.Add(this.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ");\n";
                            strTextBoxController += " private System.Windows.Forms.TextBox  txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ";\n";

                            strLableTextboxLocY = strLableTextboxLocY + 25;
                            strTabIndex = strTabIndex + 1;


                        }

                    } //end for LOOP FOR columns list

                    strLableTextboxLocY = 25;

                    strFormDesignerClass = File.ReadAllText(strFormDesignerTempPath + "FormDesigner.tem");

                    strFormDesignerClass = strFormDesignerClass.Replace("__SolutionName", strNameSpace);
                    strFormDesignerClass = strFormDesignerClass.Replace("__FormName", strDesignerFormName);
                    strFormDesignerClass = strFormDesignerClass.Replace("__FormText", strDesignerFormName);
                    strFormDesignerClass = strFormDesignerClass.Replace("__LableInitializer", strLableInitializer);
                    strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxInitializer", strTextBoxInitializer);
                    strFormDesignerClass = strFormDesignerClass.Replace("__LableProperties", strLableProperties);
                    strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxProperties", strTextBoxProperties);
                    strFormDesignerClass = strFormDesignerClass.Replace("__AddLableControl", strAddLableControl);
                    strFormDesignerClass = strFormDesignerClass.Replace("__AddTextBoxControl", strAddTextBoxControl);
                    strFormDesignerClass = strFormDesignerClass.Replace("__LableController", strLableController);
                    strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxController", strTextBoxController);

                    File.WriteAllText(strFormClassOutputPath + strDesignerFormName + ".Designer.cs", strFormDesignerClass);

                    strLableInitializer = string.Empty;
                    strLableProperties = string.Empty;
                    strAddLableControl = string.Empty;
                    strLableController = string.Empty;
                    strTextBoxInitializer = string.Empty;
                    strTextBoxProperties = string.Empty;
                    strAddTextBoxControl = string.Empty;
                    strTextBoxController = string.Empty;
                    strFormDesignerClass = string.Empty;

                    //
                } //end for LOOP FOR table names in DB

            }
        }
//////////////////////////////////////////////GeneratingMasterDetailForm////////////////////////
        if (strFormType == "MasterDetail")
        {
            strDesignerFormName = "MasterDetail";
            for (int i = 0; i <= arrayMasterFields.Length - 1; i++)
            {
                if (arrayMasterFields[i] == "stop")
                {
                    break;

                }
                    strLableInitializer += " this.lbl" + arrayMasterFields[i] + " = new System.Windows.Forms.Label();\n";
                    strLableProperties += "//lbl" + arrayMasterFields[i] + "//" + "\n this.lbl" + arrayMasterFields[i] + ".AutoSize = true;\n this.lbl" + arrayMasterFields[i] + ".Location = new System.Drawing.Point(" + strLableLocX + " , " + strLableTextboxLocY + "); \n this.lbl" + arrayMasterFields[i] + ".Name =" + '"' + "lbl" + arrayMasterFields[i] + '"' + ";\n this.lbl" + arrayMasterFields[i] + ".Size = new System.Drawing.Size(47, 13);\n this.lbl" + arrayMasterFields[i] + ".TabIndex = " + strTabIndex + ";\n this.lbl" + arrayMasterFields[i] + ".Text =" + '"' + arrayMasterFields[i] + '"' + ";\n\n";
                    strAddLableControl += "this.gbxMasterFields.Controls.Add(this.lbl" + arrayMasterFields[i] + ");\n";
                    strLableController += " private System.Windows.Forms.Label  lbl" + arrayMasterFields[i] + ";\n";
                    strTabIndex = strTabIndex + 1;

                    strTextBoxInitializer += " this.txt" + arrayMasterFields[i] + " = new System.Windows.Forms.TextBox();\n";
                    strTextBoxProperties += "//txt" + arrayMasterFields[i] + "//" + "\n this.txt" + arrayMasterFields[i] + ".AutoSize = true;\n this.txt" + arrayMasterFields[i] + ".Location = new System.Drawing.Point(" + strTextboxLocX + " , " + strLableTextboxLocY + "); \n this.txt" + arrayMasterFields[i] + ".Name =" + '"' + "txt" + arrayMasterFields[i] + '"' + ";\n this.txt" + arrayMasterFields[i] + ".Size = new System.Drawing.Size(100, 13);\n this.txt" + arrayMasterFields[i] + ".TabIndex = " + strTabIndex + ";\n this.txt" + arrayMasterFields[i] + ".Text =" + '"' + arrayMasterFields[i] + '"' + ";\n\n";
                    strAddTextBoxControl += "this.gbxMasterFields.Controls.Add(this.txt" + arrayMasterFields[i] + ");\n";
                    strTextBoxController += " private System.Windows.Forms.TextBox  txt" + arrayMasterFields[i] + ";\n";

                    strLableTextboxLocY = strLableTextboxLocY + 25;
                    strTabIndex = strTabIndex + 1;
                       
            }
                   

            strFormDesignerClass = File.ReadAllText(strFormDesignerTempPath + "CSMasterDetailFormDesigner.tem");

            strFormDesignerClass = strFormDesignerClass.Replace("__SolutionName", strNameSpace);
            strFormDesignerClass = strFormDesignerClass.Replace("__FormName", strDesignerFormName);
            strFormDesignerClass = strFormDesignerClass.Replace("__FormText", strDesignerFormName);
            strFormDesignerClass = strFormDesignerClass.Replace("__LableInitializer", strLableInitializer);
            strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxInitializer", strTextBoxInitializer);
            strFormDesignerClass = strFormDesignerClass.Replace("__LableProperties", strLableProperties);
            strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxProperties", strTextBoxProperties);
            strFormDesignerClass = strFormDesignerClass.Replace("__AddLableControl", strAddLableControl);
            strFormDesignerClass = strFormDesignerClass.Replace("__AddTextBoxControl", strAddTextBoxControl);
            strFormDesignerClass = strFormDesignerClass.Replace("__LableController", strLableController);
            strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxController", strTextBoxController);

            File.WriteAllText(strFormClassOutputPath + strDesignerFormName + ".Designer.cs", strFormDesignerClass);

        }//end if
           
        //////////////EndMasterDetailFormGenerating////////////
        if (strFormType == "ViewForm")
        {
 
        }
        ///////////////////////////////////////////////////////////////
    }
    public void GenerateFormClass(string strFormType, string strNameSpace, string strFormClassOutputPath, string strConnectionString, string strServerName)
    {
        string strFormClass = string.Empty;
        string strFormClassName = string.Empty;
        string strViewFormDesigner = string.Empty;
        string strSavebtn = string.Empty;
             
        if (strFormType == "DataEntry")
        {
            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            DataTable objTablesList = new DataTable();
            objTablesList = objDBOperation.SelectTables();

            foreach (DataRow dcr in objTablesList.Rows)
            {
                objColumnsList = new DataTable();
                objColumnsList = objDBOperation.SelectColumns(dcr["TABLE_NAME"].ToString());
                if ((string)dcr["TABLE_OWNER"] == "dbo")
                {
                    strFormClassName = dcr["TABLE_NAME"].ToString();
                    //////////////////////
                    strFormClass = File.ReadAllText(strFormDesignerTempPath + "FormClass.tem");
                    foreach (DataRow dc in objColumnsList.Rows)
                    {
                        if ((string)dc["TABLE_SCHEMA"] == "dbo")
                        {
                            strSavebtn += "\nobj" + dcr["TABLE_NAME"] + "." + dc["COLUMN_NAME"] + "=" + dcr["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Text;";

                        }
                    }
                    ///////////////////////////////////

                // strFormClass = File.ReadAllText(strFormDesignerTempPath + "FormClass.tem");
                    strFormClass = strFormClass.Replace("__ColumnsList", strSavebtn);

                    strFormClass = strFormClass.Replace("__SolutionName", strNameSpace);
                    strFormClass = strFormClass.Replace("__TableName", strFormClassName);

                    strFormClass = strFormClass.Replace("__FormName", strFormClassName);

                    File.WriteAllText(strFormClassOutputPath + strFormClassName + ".cs", strFormClass);

                }
                strFormClassName = string.Empty;
                strFormClass = string.Empty;
                strSavebtn = string.Empty;

            } //end for
        }
        //////////////////////////////////VIEW FORMS////////////////////////
        if (strFormType == "ViewForm")
        {
            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            DataTable objTablesList = new DataTable();
            objTablesList = objDBOperation.SelectTables();

            foreach (DataRow dcr in objTablesList.Rows)
            {
                if ((string)dcr["TABLE_OWNER"] == "dbo")
                {
                    strFormClassName = dcr["TABLE_NAME"].ToString();

                    strFormClass = File.ReadAllText(strFormDesignerTempPath + "View.tem");
                    strViewFormDesigner = File.ReadAllText(strFormDesignerTempPath + "ViewDesigner.tem");

                    strFormClass = strFormClass.Replace("__ServerName", strServerName);
                    strFormClass = strFormClass.Replace("__SolutionName", strNameSpace);
                    strFormClass = strFormClass.Replace("__ConnectionString", strConnectionString);
                    strViewFormDesigner = strViewFormDesigner.Replace("__SolutionName", strNameSpace);

                    strFormClass = strFormClass.Replace("__FormName", strFormClassName);
                    strViewFormDesigner = strViewFormDesigner.Replace("__FormName", strFormClassName);

                    File.WriteAllText(strFormClassOutputPath +  "frmView" + strFormClassName + ".cs", strFormClass);
                    File.WriteAllText(strFormClassOutputPath + "frmView" + strFormClassName + ".Designer.cs", strViewFormDesigner );
                }
            }
        }

        //////////////////////////////////////////////////////////////masterDetialClass//////
        if (strFormType == "MasterDetail")
        {
            strFormClassName = "MasterDetail";

            strFormClass = File.ReadAllText(strFormDesignerTempPath + "CSMasterDetailForm Class.tem");

            strFormClass = strFormClass.Replace("__NameSpace", strNameSpace);

            strFormClass = strFormClass.Replace("__FormName", "MasterDetai");

            File.WriteAllText(strFormClassOutputPath + strFormClassName + ".cs", strFormClass);


            strFormClassName = string.Empty;
            strFormClass = string.Empty;
        }
    }
}
