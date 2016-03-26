using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated) 
        {
            lblError.Text = "You are already logged in.";
            Login1.Visible = false;
        }
        else
        {
            lblError.Text = "";
            Login1.Visible = true;
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
        {
            String user = Login1.UserName;
            String pass = Login1.Password;

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE email = @user", connection);
                cmd.Parameters.AddWithValue("@user", user);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["password"].Equals(pass))
                    {
                        FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured while trying to submit: " + ex.Message;
            }
        }
    }
}