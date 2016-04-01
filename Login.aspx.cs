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
    SingletonDB db = SingletonDB.getInstance();  

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
        User user = new User(Login1.UserName, Login1.Password);

        db.login(user, lblError, Login1);
    }
}