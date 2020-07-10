using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Data.Sql;
using System.Data.SqlClient;

namespace CodeGenerator
{
    public partial class frmCodeGenerator : Form
    {
        private SqlConnection conMaster;
        private SqlCommand comMaster;
        private SqlDataAdapter daMaster;
        private DataTable dtMaster;
        private List<DatabaseTable> table_list = new List<DatabaseTable>();
        private List<DatabaseTable> selected_tables = new List<DatabaseTable>();
        private List<Column> columns = new List<Column>();
        private List<ModelClass> model_classes = new List<ModelClass>();
        private List<ControllerClass> controller_classes = new List<ControllerClass>();
        private List<string> stored_procedures = new List<string>();
        CodeGeneration objCodeGenerator;
        string connection_string = "Data Source=__ServerName;Initial Catalog=__DBName;";
        string sp_options = "SelectAll,SelectByID,InsertUpdate,Truncate,DeleteAll,DeleteByID";

        public frmCodeGenerator()
        {
            InitializeComponent();
        }

        private void frmCodeGenerator_Load(object sender, EventArgs e)
        {
            try
            {
                //////////////Finding SqlServer InstalledOn Network ///////
                SqlDataSourceEnumerator de;
                de = SqlDataSourceEnumerator.Instance;
                DataTable dtServers = new DataTable();
                dtServers = de.GetDataSources();

                foreach (DataRow dr in dtServers.Rows)
                    cmbServerList.Items.Add(dr["ServerName"].ToString());

                if (cmbServerList.Items.Count > 0)
                    cmbServerList.SelectedIndex = 0;
                /////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServerList.Text != string.Empty)
            {
                string connection_string = "Data Source =" + cmbServerList.Text + "; Integrated Security =True;";
                using (SqlConnection sql_connection = new SqlConnection(connection_string))
                {
                    try
                    {
                        sql_connection.Open();
                        DataTable dtTablesList = sql_connection.GetSchema("Databases");
                        sql_connection.Close();
                        foreach (DataRow db_name in dtTablesList.Rows)
                            cmbDatabaseList.Items.Add(db_name["Database_Name"].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                } //end sqlConeection
            } //end if
        }

        private void chkWindowAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.Enabled = !chkWindowAuthentication.Checked;
            txtPassword.Enabled = !chkWindowAuthentication.Checked;
        }

        private void chkSelectAllTable_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstTableList.Items)
                item.Checked = chkSelectAllTable.Checked;
            lstTableList.Enabled = !chkSelectAllTable.Checked;
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdOutputFolder = new FolderBrowserDialog();
            if (fbdOutputFolder.ShowDialog() == DialogResult.OK)
                txtOutputFolder.Text = fbdOutputFolder.SelectedPath;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (chkWindowAuthentication.Checked)
                connection_string += "Integrated Security=True;";
            else
                connection_string += "username=" + txtUsername.Text + ";" + "pwd=" + txtPassword.Text;

            connection_string = connection_string.Replace("__ServerName", cmbServerList.Text);
            connection_string = connection_string.Replace("__DBName", cmbDatabaseList.Text);
            txtConnectionString.Text = connection_string;
            DataTable dtTables = ConnectDB(connection_string, "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'");

            lstTableList.Items.Clear();
            foreach (DataRow rows_table in dtTables.Rows)
            {
                if (rows_table["TABLE_NAME"].ToString() != "sysdiagrams")
                {
                    ListViewItem item_table = new ListViewItem();
                    DatabaseTable db_table = new DatabaseTable();
                    db_table.TableName = rows_table["TABLE_NAME"].ToString();
                    db_table.TableType = "BaseTable";
                    List<Column> columns = GetColumns(db_table);
                    db_table.PrimaryKey = columns[0].ColumnName;
                    db_table.Columns = columns;
                    table_list.Add(db_table);

                    item_table.Text = db_table.TableName;
                    lstTableList.Items.Add(item_table);
                }
            }
            conMaster.Close();
            if (conMaster.State == ConnectionState.Open)
                conMaster.Close();

            GenerateStoredProcedureList();
        }

        private void GenerateStoredProcedureList()
        {
            ConnectDB(connection_string, "SELECT * FROM sys.procedures WHERE IS_MS_SHIPPED = 0");
            foreach (DataRow dr_stored_proc in dtMaster.Rows)
            {
                string sp_name = dr_stored_proc["name"].ToString();

                if (!sp_name.Contains("sp_"))
                {
                    stored_procedures.Add(dr_stored_proc["name"].ToString());
                    lstStoredProcedures.Items.Add(dr_stored_proc["name"].ToString());
                }
            }
        }

        private void lstTableList_MouseClick(object sender, MouseEventArgs e)
        {
            ConnectDB(txtConnectionString.Text, "sp_columns '" + lstTableList.FocusedItem.Text + "'");
            txtColumnsList.Text = "";
            foreach (DataRow row_column_list in dtMaster.Rows)
                txtColumnsList.Text += row_column_list["COLUMN_NAME"].ToString() + ", " + row_column_list["TYPE_NAME"].ToString() + Environment.NewLine;

        }

        private DataTable ConnectDB(string connection_string, string command_text)
        {
            dtMaster = new DataTable();
            conMaster = new SqlConnection(connection_string);
            comMaster = new SqlCommand(command_text);
            daMaster = new SqlDataAdapter();
            if (conMaster.State == ConnectionState.Open)
                conMaster.Close();
            conMaster.Open();
            comMaster.Connection = conMaster;
            daMaster.SelectCommand = comMaster;
            daMaster.Fill(dtMaster);
            return dtMaster;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateSelectedTableList();

            bool is_forward = RequiredFieldsCheck();
            if (!is_forward)
                return;

            objCodeGenerator = new CodeGeneration(selected_tables, PublicEnums.LanguageOption.CSharp, sp_options, stored_procedures);
            GenerateStoredProcedures(); //generate stored procedure
            GenerateModelClasses();
            GenerateControllerClasses();
        }

        private bool RequiredFieldsCheck()
        {
            bool is_forward = true;

            if (selected_tables.Count == 0)
                is_forward = false;

            if (!(chkStoredProcedures.Checked || chkClasses.Checked))
                is_forward = false;

            if (txtOutputFolder.Text == "")
                is_forward = false;

            if (!is_forward)
                MessageBox.Show("Output Path must be given. Atlease one table must be selected. Atleast 1 option must be checked");

            return is_forward;
        }

        private void GenerateSelectedTableList()
        {
            foreach (ListViewItem item in lstTableList.Items)
            {
                if (item.Checked)
                {
                    DatabaseTable table = FindTableByName(item.Text);
                    if (!(table is null))
                        selected_tables.Add(table);
                }
            }
        }

        private DatabaseTable FindTableByName(string table_name)
        {
            foreach (DatabaseTable table in table_list)
                if (table.TableName == table_name)
                    return table;
            return null;
        }

        private void GenerateStoredProcedures()
        {
            string stored_procedures = objCodeGenerator.GenerateProcedures();
            string sp_path = txtOutputFolder.Text + "\\" + cmbDatabaseList.Text + ".sql";
            File.WriteAllText(sp_path, stored_procedures);
            txtGeneratorLog.SelectedText += Environment.NewLine + "Stored Procedures Generated" + Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine;
        }

        private void GenerateModelClasses()
        {
            string model_path = txtOutputFolder.Text + @"\Model\";
            model_classes = objCodeGenerator.GenerateModelClasses();
            Directory.CreateDirectory(model_path);
            foreach (ModelClass model_class in model_classes)
            {
                string model_class_path = model_path + model_class.ClassName + ".cs";
                File.WriteAllText(model_class_path, model_class.ClassCode);
            }
        }

        private void GenerateControllerClasses()
        {
            string controller_path = txtOutputFolder.Text + @"\Controller\";
            controller_classes = objCodeGenerator.GenerateControllerClasses();
            Directory.CreateDirectory(controller_path);
            foreach (ControllerClass controller_class in controller_classes)
            {
                string controller_class_path = controller_path + controller_class.ClassName + ".cs";
                File.WriteAllText(controller_class_path, controller_class.ClassCode);
            }

        }

        private List<Column> GetColumns(DatabaseTable db_table)
        {
            DataTable dtColumns = ConnectDB(connection_string, "SP_COLUMNS " + db_table.TableName);
            List<Column> columns = new List<Column>();
            foreach (DataRow drColumn in dtColumns.Rows)
            {
                Column column = new Column();
                column.ColumnName = drColumn["COLUMN_NAME"].ToString();
                column.TableName = drColumn["TABLE_NAME"].ToString();
                column.DataType = Convert.ToInt32(drColumn["DATA_TYPE"]);
                column.TypeName = drColumn["TYPE_NAME"].ToString();
                column.Precision = Convert.ToInt32(drColumn["PRECISION"]);
                column.Length = Convert.ToInt32(drColumn["LENGTH"]);
                column.NumericScale = Convert.ToString(drColumn["SCALE"]);
                column.OrdinalPosition = Convert.ToInt32(drColumn["ORDINAL_POSITION"]);
                if (drColumn["ORDINAL_POSITION"].ToString() == "1")
                    column.IsPrimaryKey = true;
                columns.Add(column);
            }

            return columns;
        }

        private void txtGeneratorLog_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtUsername.Text = ((int) e.KeyChar).ToString();
        }

        private void btnRemoveProc_Click(object sender, EventArgs e)
        {
            foreach (string proc in stored_procedures)
                ConnectDB(connection_string, "DROP PROCEDURE \"" + proc + " \"");

            lstStoredProcedures.Items.Clear();
            GenerateStoredProcedureList();
        }
    }
}