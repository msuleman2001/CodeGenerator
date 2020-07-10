using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

class VBFormsGenerator
{

    DataTable objTablesList;
    DataTable objColumnsList;

    string strVbFormDesignerTemp = @"..\..\Templates\FormDesigner\Winforms\VB\";

    public void VBFormDesinerClass(string strConnectionString, string strVBFormClassesOutputPath, string strFormType, string[] arrayMasterTableFields, string[] arrayDetailTableFields)
    {
        int strLableLocX = 25;
        int strLableTextboxLocY = 25;
        int strTabIndex = 0;
        int strTextboxLocX = 110;
        int FormSizeY = 200;
        int FormSizeX = 140;

        string strLableInitializer = string.Empty;
        string strTextBoxInitializer = string.Empty;

        string strLableProperties = string.Empty;
        string strTextBoxProperties = string.Empty;

        string strAddLableControl = string.Empty;
        string strAddTextBoxControl = string.Empty;

        string strLableController = string.Empty;
        string strTextBoxController = string.Empty;

        string strFormDesignerClass = string.Empty;
        string strDesignerFormName = string.Empty;

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


                            strLableInitializer += "Me.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + "= New System.Windows.Forms.Label \n";
                            strLableProperties += " ' \n ' lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + "\n '" + "\n Me.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".AutoSize = true\n Me.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Location = new System.Drawing.Point(" + strLableLocX + " , " + strLableTextboxLocY + ") \n Me.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Name =" + '"' + "lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + '"' + "\n Me.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Size = new System.Drawing.Size(47, 13)\n Me.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".TabIndex = " + strTabIndex + " \n Me.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Text =" + '"' + dc["COLUMN_NAME"] + '"' + " \n\n";
                            strAddLableControl += "Me.Controls.Add(Me.lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ") \n";
                            strLableController += " Friend WithEvents lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + " As System.Windows.Forms.Label \n";
                            strTabIndex = strTabIndex + 1;

                            strTextBoxInitializer += "Me.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + " = new System.Windows.Forms.TextBox\n";
                            strTextBoxProperties += " ' \n ' txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + "\n ' " + "\n Me.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".AutoSize = true \n Me.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Location = new System.Drawing.Point(" + strTextboxLocX + " , " + strLableTextboxLocY + ") \n Me.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Name =" + '"' + "txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + '"' + " \n Me.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Size = new System.Drawing.Size(100, 13)\n Me.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".TabIndex = " + strTabIndex + " \n Me.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Text =" + '"' + dc["COLUMN_NAME"] + '"' + " \n\n";
                            strAddTextBoxControl += "Me.Controls.Add(Me.txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ") \n";
                            strTextBoxController += " Friend WithEvents txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + " As System.Windows.Forms.TextBox \n";

                            strTabIndex = strTabIndex + 1;

                            strLableTextboxLocY = strLableTextboxLocY + 25;
                        }

                    } //end for LOOP FOR columns list

                    strFormDesignerClass = File.ReadAllText(strVbFormDesignerTemp + "VBFormDesigner.tem");

                    strFormDesignerClass = strFormDesignerClass.Replace("__FormName", strDesignerFormName);
                    strFormDesignerClass = strFormDesignerClass.Replace("__LableInitializer", strLableInitializer);
                    strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxInitializer", strTextBoxInitializer);
                    strFormDesignerClass = strFormDesignerClass.Replace("__LableProperties", strLableProperties);
                    strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxProperties", strTextBoxProperties);
                    strFormDesignerClass = strFormDesignerClass.Replace("__AddLableControl", strAddLableControl);
                    strFormDesignerClass = strFormDesignerClass.Replace("__AddTextBoxControl", strAddTextBoxControl);
                    strFormDesignerClass = strFormDesignerClass.Replace("__LableController", strLableController);
                    strFormDesignerClass = strFormDesignerClass.Replace("__TextBoxController", strTextBoxController);

                    File.WriteAllText(strVBFormClassesOutputPath + strDesignerFormName + ".Designer.vb", strFormDesignerClass);

                    strLableInitializer = string.Empty;
                    strLableProperties = string.Empty;
                    strAddLableControl = string.Empty;
                    strLableController = string.Empty;
                    strTextBoxInitializer = string.Empty;
                    strTextBoxProperties = string.Empty;
                    strAddTextBoxControl = string.Empty;
                    strTextBoxController = string.Empty;
                    strFormDesignerClass = string.Empty;

                    strTabIndex = 0;

                    strLableTextboxLocY = 25;

                }
            }//end foreach
        }//endif
        /////masterDetail forms
        if (strFormType == "MasterDetail")
        {
            strDesignerFormName = "MasterDetail";
            for (int i = 0; i <= arrayMasterTableFields.Length - 1; i++)
            {
                if (arrayMasterTableFields[i] == "stop")
                {
                    break;
                }
                strLableInitializer += "Me.lbl" + arrayMasterTableFields[i] + "= New System.Windows.Forms.Label \n";
                strLableProperties += " ' \n ' lbl" + arrayMasterTableFields[i] + "\n '" + "\n Me.lbl" + arrayMasterTableFields[i] + ".AutoSize = true\n Me.lbl" + arrayMasterTableFields[i] + ".Location = new System.Drawing.Point(" + strLableLocX + " , " + strLableTextboxLocY + ") \n Me.lbl" + arrayMasterTableFields[i] + ".Name =" + '"' + "lbl" + arrayMasterTableFields[i] + '"' + "\n Me.lbl" + arrayMasterTableFields[i] + ".Size = new System.Drawing.Size(47, 13)\n Me.lbl" + arrayMasterTableFields[i] + ".TabIndex = " + strTabIndex + " \n Me.lbl" + arrayMasterTableFields[i] + ".Text =" + '"' + arrayMasterTableFields[i] + '"' + " \n\n";
                strAddLableControl += "Me.Controls.Add(Me.lbl" + arrayMasterTableFields[i] + ") \n";
                strLableController += " Friend WithEvents lbl" + arrayMasterTableFields[i] + " As System.Windows.Forms.Label \n";
                strTabIndex = strTabIndex + 1;

                strTextBoxInitializer += "Me.txt" + arrayMasterTableFields[i] + " = new System.Windows.Forms.TextBox\n";
                strTextBoxProperties += " ' \n ' txt" + arrayMasterTableFields[i] + "\n ' " + "\n Me.txt" + arrayMasterTableFields[i] + ".AutoSize = true \n Me.txt" + arrayMasterTableFields[i] + ".Location = new System.Drawing.Point(" + strTextboxLocX + " , " + strLableTextboxLocY + ") \n Me.txt" + arrayMasterTableFields[i] + ".Name =" + '"' + "txt" + arrayMasterTableFields[i] + '"' + " \n Me.txt" + arrayMasterTableFields[i] + ".Size = new System.Drawing.Size(100, 13)\n Me.txt" + arrayMasterTableFields[i] + ".TabIndex = " + strTabIndex + " \n Me.txt" + arrayMasterTableFields[i] + ".Text =" + '"' + arrayMasterTableFields[i] + '"' + " \n\n";
                strAddTextBoxControl += "Me.Controls.Add(Me.txt" + arrayMasterTableFields[i] + ") \n";
                strTextBoxController += " Friend WithEvents txt" + arrayMasterTableFields[i] + " As System.Windows.Forms.TextBox \n";

                strTabIndex = strTabIndex + 1;

                strLableTextboxLocY = strLableTextboxLocY + 25;
            }
            strFormDesignerClass = File.ReadAllText(strVbFormDesignerTemp + "VBMasterDetailFormDesigner.tem");

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

            File.WriteAllText(strVBFormClassesOutputPath + strDesignerFormName + ".Designer.vb", strFormDesignerClass);

            strFormDesignerClass = string.Empty;
        }
    }//////////////////end Function FormDesigner /////////////

    public void GenerateVBFormClass(string strConnectionString, string strVBFormClassOutputPath, string strFormType, string strServerName)
    {

        string strFormClass = string.Empty;
        string strFormClassName = string.Empty;
        string strFormDesignerClass = string.Empty;
        string strSavebtn= string.Empty;

        if (strFormType == "DataEntry")
        {
            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            DataTable objTablesList = new DataTable();
            objTablesList = objDBOperation.SelectTables();
            DataTable objColumnsList = new DataTable();
            ///////////////// Select tables in Db //////////////////////                string strColumnsList = string.Empty;

            foreach (DataRow dcr in objTablesList.Rows)
            {
                objColumnsList = new DataTable();
                objColumnsList = objDBOperation.SelectColumns(dcr["TABLE_NAME"].ToString());
                   
                if ((string)dcr["TABLE_OWNER"] == "dbo")
                {
          
                    strFormClassName = dcr["TABLE_NAME"].ToString();
                        
                    strFormClass = File.ReadAllText(strVbFormDesignerTemp + "VBFormClass.tem");
                    //////////////////////
                    foreach (DataRow dc in objColumnsList.Rows)
                    {
                        if ((string)dc["TABLE_SCHEMA"] == "dbo")
                        {
                                
                            //strSavebtn += "\nobj" + dcr["TABLE_NAME"] + "." + dc["COLUMN_NAME"] + " = 0";
                            strSavebtn += "\nobj" + dcr["TABLE_NAME"] + "." + dc["COLUMN_NAME"] + "=" + dcr["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + ".Text";
                                
                        }
                    }
                    ///////////////////////////////////
                    strFormClass = strFormClass.Replace("__ColumnsList", strSavebtn);

                    strFormClass = strFormClass.Replace("__FormName", strFormClassName);
                    strFormClass = strFormClass.Replace("__TableName", strFormClassName);

                    strFormClass = strFormClass.Replace("__SolutionName", strFormClassName);

                    strFormClass = strFormClass.Replace("__ServerName", strFormClassName);

                    File.WriteAllText(strVBFormClassOutputPath + strFormClassName + ".vb", strFormClass);
                       
                }
                strFormClassName = string.Empty;
                strFormClass = string.Empty;
                strFormDesignerClass = string.Empty;
                strSavebtn = string.Empty;

            } //end for
        }///////////////end DataEntry FormClass////
        ///////////////////////////////////View Form Generator /////////////////////
        if (strFormType == "ViewForm")
        {
            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            DataTable objTablesList = new DataTable();
            objTablesList = objDBOperation.SelectTables();
            ///////////////// Select tables in Db //////////////////////
            foreach (DataRow dcr in objTablesList.Rows)
            {
                if ((string)dcr["TABLE_OWNER"] == "dbo")
                {
                    strFormClassName = dcr["TABLE_NAME"].ToString();

                    strFormClass = File.ReadAllText(strVbFormDesignerTemp + "View.tem");
                    strFormDesignerClass = File.ReadAllText(strVbFormDesignerTemp + "ViewDesigner.tem");

                    strFormClass = strFormClass.Replace("__FormName", strFormClassName);
                    strFormClass = strFormClass.Replace("__ServerName", strServerName);
                    strFormDesignerClass = strFormDesignerClass.Replace("__FormName", strFormClassName);

                    File.WriteAllText(strVBFormClassOutputPath + "frmView"+strFormClassName + ".vb", strFormClass);
                    File.WriteAllText(strVBFormClassOutputPath + "frmView" +strFormClassName+ ".Designer.vb", strFormDesignerClass);

                }
                strFormClassName = string.Empty;
                strFormClass = string.Empty;
            }
        }

        ////////////////////////////////////////////// end view FormGenerator////////

        ////////////////////////GenertingMasterDetailformClass////////////////////
        if (strFormType == "MasterDetail")
        {
            strFormClassName = "MasterDetail";

            strFormClass = File.ReadAllText(strVbFormDesignerTemp + "VBMasterDetailFormClass.tem");

            strFormClass = strFormClass.Replace("__FormName", strFormClassName);

            File.WriteAllText(strVBFormClassOutputPath + strFormClassName + ".vb", strFormClass);

            strFormClassName = string.Empty;
            strFormClass = string.Empty;
        }

        //////////////////////////////////////////////////////////////////////////
    }//////end function
} ////////EndClass