using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated) // if the user is already logged in
        {
            lblName.Text = "Logged in as: ";
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT firstname FROM user WHERE email = @user", connection);
                    cmd.Parameters.AddWithValue("@user", HttpContext.Current.User.Identity.Name);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lblUser.Text = reader["firstname"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            //hypLogin.Visible = false;
            //hypLogout.Visible = true;
        }
        else
        {
            lblName.Text = "";
            lblUser.Text = "";
            //hypLogin.Visible = true;
            //hypLogout.Visible = false;
        }
    }
}
