using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


class DatabaseOperations
{
    DataTable objData;
    SqlCommand objCommand;
    SqlConnection objConnection;
    SqlDataAdapter objDataAdapter;
    string strConnectionString;

    public DatabaseOperations(string strConnectionString)
    {
        objData = new DataTable();
        objCommand = new SqlCommand();
        objConnection = new SqlConnection();
        objDataAdapter = new SqlDataAdapter();
        this.strConnectionString = strConnectionString;
    }//constructor

    public DataTable SelectTables()
    {
        try
        {
            objConnection.ConnectionString = strConnectionString;
            objConnection.Open();
            objCommand.CommandText = "SP_TABLES";
            objCommand.Connection = objConnection;
            objDataAdapter.SelectCommand = objCommand;
            objDataAdapter.Fill(objData);
            objConnection.Close();
            return objData;
                
               
        }//end try
        catch (Exception Ex)
        {
              
            if (objConnection.State == ConnectionState.Open)
                objConnection.Close();

            return null;
        }//end catch            
    }//end function

    public DataTable SelectColumns(string strTableName)
    {
        try
        {
            objConnection.ConnectionString = strConnectionString;
            objConnection.Open();
            objData = new DataTable();
            objCommand.CommandText = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + strTableName + "'";
            objCommand.Connection = objConnection;
            objDataAdapter.SelectCommand = objCommand;
            objDataAdapter.Fill(objData);
            objConnection.Close();
            return objData;
        }//end try
        catch (Exception ex)
        {
            return null;
        }//end catch
    }//end function

    public static String GenerateCSVColumnsList(DataTable dtColumns)
    {
        try
        {
            string strColumnsList = string.Empty;
            foreach (DataRow dr in dtColumns.Rows)
            {
                if ((string)dr["TABLE_SCHEMA"] == "dbo")
                {
                    strColumnsList += dr["TABLE_NAME"] + "." + dr["COLUMN_NAME"] + ", ";
                }
            } //end for
            return strColumnsList.Remove(strColumnsList.LastIndexOf(","));
        }
        catch (Exception ex)
        {
            return null;
        }//end catch
    }
}