using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEmail.Text = "";
        txtFirst.Text = "";
        txtLast.Text = "";
        txtPassword.Text = "";
        txtPassword2.Text = "";
        txtPhone.Text = "";
    }
}