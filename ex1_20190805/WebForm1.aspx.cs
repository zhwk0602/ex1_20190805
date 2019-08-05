using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace ex1_20190805
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region dt
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Addr");
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("birthday", typeof(DateTime));
            dt.TableName = "dt1";
            DataRow workRow = dt.NewRow();
            workRow["Name"] = "Ken";
            workRow["Age"] = 10;
            workRow["Addr"] = "Taipei";
            workRow["Mobile"] = "0912345678";
            workRow["Birthday"] = DateTime.Now;
            dt.Rows.Add(workRow);
            dt.AcceptChanges(); 
            #endregion
            #region ds
            DataSet ds = new DataSet();//多個DataTabel
            ds.Tables.Add(dt); 
            #endregion
            List<DataTable> Ldt = new List<DataTable>();
            Ldt.Add(dt);
            //合併dt.Merge
            #region SQL ADO
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NorthwindConnectionString1"].ConnectionString);
            SqlDataReader dr = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 300;
            cmd.Connection = conn;
            #region Query
            cmd.CommandText = "SELECT * FROM [Suppliers] WHERE [City]=@City or [Country]=@Country ORDER BY [CompanyName]";


            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 15);

            cmd.Parameters["@City"].Value = "London";
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 15);

            cmd.Parameters["@Country"].Value = "UK";
            #endregion
            #region UPD
            //cmd.CommandText = "UPDATE [Suppliers] SET Fax=@Fax  WHERE [City]=@City and  [Country]=@Country ";

            //cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 24);

            //cmd.Parameters["@Fax"].Value = "0312345678";
            //cmd.Parameters.Add("@City", SqlDbType.NVarChar, 15);

            //cmd.Parameters["@City"].Value = "London";
            //cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 15);

            //cmd.Parameters["@Country"].Value = "UK"; 
            #endregion
            try
            {
                conn.Open();
                 dr = cmd.ExecuteReader(); //query

                //cmd.ExecuteNonQuery();//way1 不太好
                // int rc = cmd.ExecuteNonQuery();//way2
                //資料繫結或做其他處理
                GridView1.DataSource = dr;
                
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally//關閉資源
            {
                if (dr != null)
                {
                    cmd.Cancel();
                    dr.Close();
                }
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();

                }

            } 
            #endregion
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            


        }

        
    }
}