using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

class WebFormsGenerator
{
    DataTable objTablesList;
    DataTable objColumnsList;

    string strFormDesignerTempPath = @"..\..\Templates\FormDesigner\WebForms\VB\";

    public void GenerateWebForms(string strConnectionString, string strWebFormOutputPath, string strFormType,string[] arrayMasterTableFields,string[] arrayDetailTableFields,string strServerName,string  strNameSpace)
    {
        string strDesignerFormName = string.Empty;
        string strTableContainer = string.Empty;
        string strFormDesignerClass = string.Empty;
        string strCodeBehindClass = string.Empty;

        if (strFormType == "DataEntry")
        {
            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            DataTable objTablesList; objTablesList = new DataTable();
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

                            strTableContainer += "\n<tr> <td><asp:Label ID=" + '"' + "lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + '"' + " runat=" + '"' + "server" + '"' + " Text=" + '"' + dc["COLUMN_NAME"] + '"' + "></asp:Label> </td> <td><asp:TextBox ID=" + '"' + "txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + '"' + " runat=" + '"' + "server" + '"' + " Text=" + '"' + dc["COLUMN_NAME"] + '"' + " ></asp:TextBox> </td> </tr> ";

                        }

                    } //end for LOOP FOR columns list

                    strFormDesignerClass = File.ReadAllText(strFormDesignerTempPath + "WebForms.tem");

                    strFormDesignerClass = strFormDesignerClass.Replace("__FormName", strDesignerFormName);


                    strFormDesignerClass = strFormDesignerClass.Replace("__TableContainer", strTableContainer);

                    File.WriteAllText(strWebFormOutputPath + strDesignerFormName + ".aspx", strFormDesignerClass);


                    strFormDesignerClass = string.Empty;
                    strDesignerFormName = string.Empty;
                    strTableContainer = string.Empty;
                    strCodeBehindClass = string.Empty;

                    //
                } //end for LOOP FOR table names in DB

            }//endforeach
        }
        ///////////////////////////////////////////////////end DataEntry //////

        ////////////////////////////View Form////////////////////
        if (strFormType == "ViewForm")
        {
            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            DataTable objTablesList; objTablesList = new DataTable();
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


                    strCodeBehindClass = File.ReadAllText(strFormDesignerTempPath + "View.aspx.tem");
                    strFormDesignerClass = File.ReadAllText(strFormDesignerTempPath + "View.tem");

                    strCodeBehindClass = strCodeBehindClass.Replace("__FormName", strDesignerFormName);
                    strFormDesignerClass = strFormDesignerClass.Replace("__FormName", strDesignerFormName);

                    strCodeBehindClass = strCodeBehindClass.Replace("__SolutionName", strNameSpace);
                    strFormDesignerClass = strFormDesignerClass.Replace("__SolutionName",strNameSpace);

                    strCodeBehindClass = strCodeBehindClass.Replace("__ServerName", strServerName);

                    File.WriteAllText(strWebFormOutputPath + strDesignerFormName + "_View.aspx", strFormDesignerClass);
                    File.WriteAllText(strWebFormOutputPath + strDesignerFormName  + "_View.aspx.vb", strCodeBehindClass);


                    strFormDesignerClass = string.Empty;
                    strDesignerFormName = string.Empty;
                    strCodeBehindClass = string.Empty;
                }
            }
        }

        ///////////////////////////////////////////////////////////
        if (strFormType == "MasterDetail")
        {
            strDesignerFormName = "MasterDetail";
            for (int i = 0; i <= arrayMasterTableFields.Length - 1; i++)
            {
                if (arrayMasterTableFields[i] == "stop")
                {
                    break;
                }
                strTableContainer += "\n<tr> <td><asp:Label ID=" + '"' + "lbl" + arrayMasterTableFields[i] + '"' + " runat=" + '"' + "server" + '"' + " Text=" + '"' + arrayMasterTableFields[i] + '"' + "></asp:Label> </td> <td><asp:TextBox ID=" + '"' + "txt" + arrayMasterTableFields[i] + '"' + " runat=" + '"' + "server" + '"' + " Text=" + '"' + arrayMasterTableFields[i] + '"' + " ></asp:TextBox> </td> </tr> ";
            }
            strFormDesignerClass = File.ReadAllText(strFormDesignerTempPath + "VBWebMasterDetailFormDesigner.tem");
            strCodeBehindClass = File.ReadAllText(strFormDesignerTempPath + "VBWebMasterDetailCodeBehind.tem");

            strFormDesignerClass = strFormDesignerClass.Replace("__FormName", strDesignerFormName);
            strCodeBehindClass = strCodeBehindClass.Replace("__FormName", strDesignerFormName);
            strFormDesignerClass = strFormDesignerClass.Replace("__TableContainer", strTableContainer);

            File.WriteAllText(strWebFormOutputPath + strDesignerFormName + ".aspx", strFormDesignerClass);
            File.WriteAllText(strWebFormOutputPath + strDesignerFormName + ".aspx.vb", strCodeBehindClass);

            strFormDesignerClass = string.Empty;
            strDesignerFormName = string.Empty;
            strTableContainer = string.Empty;
            strCodeBehindClass = string.Empty;
        }


    }

    public void CodeBehindFileGenerator(string strConnectionString,string strCodeBehindClassOutputPath)
    {
        string strCodeBehindClass = string.Empty;
        string strCodeBehindViewFormClass = string.Empty;
        string strCodeBehindClassName = string.Empty;

        DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

        objTablesList = new DataTable();
        objTablesList = objDBOperation.SelectTables();

        foreach (DataRow dcr in objTablesList.Rows)
        {
            if ((string)dcr["TABLE_OWNER"] == "dbo")
            {
                strCodeBehindClassName = dcr["TABLE_NAME"].ToString();

                strCodeBehindClass = File.ReadAllText(strFormDesignerTempPath + "CodeBehindClass.tem");
                //strCodeBehindViewFormClass = File.ReadAllText(strFormDesignerTempPath + "");

                strCodeBehindClass = strCodeBehindClass.Replace("__FormName", strCodeBehindClassName);

                File.WriteAllText(strCodeBehindClassOutputPath + strCodeBehindClassName + ".aspx.vb", strCodeBehindClass);

            }

            strCodeBehindClass = string.Empty;
            strCodeBehindClassName = string.Empty;

        } //end for
    }

    /////////////////////// CSharp web FormS genreting ///////////
    public void CSWebFormGenerator(string strConnectionString, string strCSFormClassOutputPath, string strNameSpace, string strFormType, string[] arrayMasterTableFields, string[] arrayDetailTableFields, string strServerName)
    {
        string strCSFormDesignerTempPath = @"..\..\Templates\FormDesigner\WebForms\CS\";


        string strCSDesignerFormName = string.Empty;
            
        string strCSTableContainer = string.Empty;

        string strCSFormDesignerClass = string.Empty;
        string strCSViewFormDesignerClass = string.Empty;
        string strCSCodeBehindViewFormClass = string.Empty;
        string strCSCodeBehindClass = string.Empty;

        if (strFormType == "DataEntry")
        {
            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            objTablesList = new DataTable();
            objTablesList = objDBOperation.SelectTables();
            objColumnsList = new DataTable();
            ///////////////// Select tables in Db //////////////////////
            foreach (DataRow dr in objTablesList.Rows)
            {
                ///////////////"TABLE_NAME" are tables name in DB//////////
                objColumnsList = new DataTable();
                objColumnsList = objDBOperation.SelectColumns(dr["TABLE_NAME"].ToString());
                string strColumnsList = string.Empty;

                if ((string)dr["TABLE_OWNER"] == "dbo")
                {
                    strCSDesignerFormName = dr["TABLE_NAME"].ToString();

                    foreach (DataRow dc in objColumnsList.Rows)
                    {
                        if ((string)dc["TABLE_SCHEMA"] == "dbo")
                        {

                            strCSTableContainer += "\n<tr> <td><asp:Label ID=" + '"' + "lbl" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + '"' + " runat=" + '"' + "server" + '"' + " Text=" + '"' + dc["COLUMN_NAME"] + '"' + "></asp:Label> </td> <td><asp:TextBox ID=" + '"' + "txt" + dc["TABLE_NAME"] + "_" + dc["COLUMN_NAME"] + '"' + " runat=" + '"' + "server" + '"' + " Text=" + '"' + dc["COLUMN_NAME"] + '"' + " ></asp:TextBox> </td> </tr> ";

                        }

                    } //end for LOOP FOR columns list

                    strCSFormDesignerClass = File.ReadAllText(strCSFormDesignerTempPath + "CSWebForms.tem");
                    strCSCodeBehindClass = File.ReadAllText(strCSFormDesignerTempPath + "CSCodeBehindClass.tem");
                        
                    strCSFormDesignerClass = strCSFormDesignerClass.Replace("__FormName", strCSDesignerFormName);
                    strCSCodeBehindClass = strCSCodeBehindClass.Replace("__FormName", strCSDesignerFormName);
                    strCSCodeBehindClass = strCSCodeBehindClass.Replace("__NameSpace", strNameSpace);
                        
                    strCSFormDesignerClass = strCSFormDesignerClass.Replace("__TableContainer", strCSTableContainer);

                    File.WriteAllText(strCSFormClassOutputPath + strCSDesignerFormName + ".aspx", strCSFormDesignerClass);
                    File.WriteAllText(strCSFormClassOutputPath + strCSDesignerFormName + ".aspx.cs", strCSCodeBehindClass);
                       
                    strCSFormDesignerClass = string.Empty;
                    strCSViewFormDesignerClass = string.Empty;
                    strCSCodeBehindClass = string.Empty;
                    strCSCodeBehindViewFormClass = string.Empty;

                    //
                } //end for LOOP FOR table names in DB

            }//end forEach
        }
        ///////////////////dataEntry//////////////////////////////
        if (strFormType == "ViewForm")
        {
            DatabaseOperations objDBOperation = new DatabaseOperations(strConnectionString);

            objTablesList = new DataTable();
            objTablesList = objDBOperation.SelectTables();
            objColumnsList = new DataTable();
            ///////////////// Select tables in Db //////////////////////
            foreach (DataRow dr in objTablesList.Rows)
            {
                ///////////////"TABLE_NAME" are tables name in DB//////////
                objColumnsList = new DataTable();
                objColumnsList = objDBOperation.SelectColumns(dr["TABLE_NAME"].ToString());
                string strColumnsList = string.Empty;

                if ((string)dr["TABLE_OWNER"] == "dbo")
                {
                    strCSDesignerFormName = dr["TABLE_NAME"].ToString();

                    strCSViewFormDesignerClass = File.ReadAllText(strCSFormDesignerTempPath + "CSWebViewForm.tem");
                    strCSCodeBehindViewFormClass = File.ReadAllText(strCSFormDesignerTempPath + "CSCodeBehindClassViewForm.tem");

                    strCSViewFormDesignerClass = strCSViewFormDesignerClass.Replace("__FormName", strCSDesignerFormName);
                    strCSCodeBehindViewFormClass = strCSCodeBehindViewFormClass.Replace("__FormName", strCSDesignerFormName);

                    strCSViewFormDesignerClass = strCSViewFormDesignerClass.Replace("__SolutionName", strNameSpace);
                    strCSCodeBehindViewFormClass = strCSCodeBehindViewFormClass.Replace("__SolutionName", strNameSpace);

                    strCSCodeBehindViewFormClass = strCSCodeBehindViewFormClass.Replace("__ServerName", strServerName);

                    File.WriteAllText(strCSFormClassOutputPath + strCSDesignerFormName + "_View.aspx", strCSViewFormDesignerClass);
                    File.WriteAllText(strCSFormClassOutputPath + strCSDesignerFormName + "_View.aspx.cs", strCSCodeBehindViewFormClass);

                    strCSViewFormDesignerClass = string.Empty;
                    strCSCodeBehindViewFormClass = string.Empty;
                }
            }//////end foreach
        }
        //////master DetailForms//////////////////////////////////
        if (strFormType == "MasterDetail")
        {
            strCSDesignerFormName = "MasterDetail";
            for (int i = 0; i <= arrayMasterTableFields.Length - 1; i++)
            {
                if (arrayMasterTableFields[i] == "stop")
                {
                    break;
                }

                strCSTableContainer += "\n<tr> <td><asp:Label ID=" + '"' + "lbl" + arrayMasterTableFields[i] + '"' +
                                       " runat=" + '"' + "server" + '"' + " Text=" + '"' + arrayMasterTableFields[i] +
                                       '"' + "></asp:Label> </td> <td><asp:TextBox ID=" + '"' + "txt" +
                                       arrayMasterTableFields[i] + '"' + " runat=" + '"' + "server" + '"' + " Text=" +
                                       '"' + arrayMasterTableFields[i] + '"' + " ></asp:TextBox> </td> </tr> ";
            }
            strCSFormDesignerClass = File.ReadAllText(strCSFormDesignerTempPath + "CSWebMasterDetailFormDesigner.tem");
            strCSCodeBehindClass = File.ReadAllText(strCSFormDesignerTempPath + "CSWebMasterDetailCodeBehind.tem");

            strCSFormDesignerClass = strCSFormDesignerClass.Replace("__FormName", strCSDesignerFormName);
            strCSCodeBehindClass = strCSCodeBehindClass.Replace("__FormName", strCSDesignerFormName);

            strCSCodeBehindClass = strCSCodeBehindClass.Replace("__NameSpace", strNameSpace);
            strCSCodeBehindViewFormClass = strCSCodeBehindViewFormClass.Replace("__NameSpace", strNameSpace);
            strCSFormDesignerClass = strCSFormDesignerClass.Replace("__TableContainer", strCSTableContainer);

            File.WriteAllText(strCSFormClassOutputPath + strCSDesignerFormName + ".aspx", strCSFormDesignerClass);
            File.WriteAllText(strCSFormClassOutputPath + strCSDesignerFormName + ".aspx.cs", strCSCodeBehindClass);

        }
    }

}

