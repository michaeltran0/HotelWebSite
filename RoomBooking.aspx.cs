using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class RoomBooking : System.Web.UI.Page
{
    SingletonDB db = SingletonDB.getInstance();  

    protected void Page_Load(object sender, EventArgs e)
    {
        display();
    }

    public void display()
    {
        if (!IsPostBack)
        {
            //display rooms in drop down list
            ddlRooms.DataSource = db.getRooms();
            ddlRooms.DataTextField = "roomInfo";
            ddlRooms.DataValueField = "roomid";
            ddlRooms.DataBind();
        }

        //display bookingRoom and Room columns in table format.  WILL BE REMOVED
        using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT bookingid, userid, roomid, DATE_FORMAT(checkin, '%d/%m/%Y') AS checkin, DATE_FORMAT(checkout, '%d/%m/%Y') AS checkout FROM booking", connection);
                DataTable dataTable = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
                cmd = new MySqlCommand("SELECT * FROM room", connection);
                dataTable = new DataTable();
                da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                GridView2.DataSource = dataTable;
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured while selecting from bookingRoom: " + ex.Message;
            }
        }
        //end of removed section
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblResult.Text = "";
        lblError.Text = "";
        int roomID = Int32.Parse(ddlRooms.SelectedItem.Value);

        DateTime checkIn = Convert.ToDateTime(txtCheckIn.Text);
        DateTime checkOut = Convert.ToDateTime(txtCheckOut.Text);

        //check if date from is before the date to
        if (checkIn < checkOut)
        {
            db.insertBooking(checkIn, checkOut, roomID, HttpContext.Current.User.Identity.Name, lblResult, lblError);
            display();
        }
        else
        {
            lblResult.Text = "\"Check In date\" must be before \"Check Out date\".";
        }        
    }

    //Search for available rooms
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblResult.Text = "";
        lblError.Text = "";
        DateTime checkIn = Convert.ToDateTime(txtCheckIn.Text);
        DateTime checkOut = Convert.ToDateTime(txtCheckOut.Text);

        //check if date from is before the date to
        if (checkIn < checkOut)
        {
            db.searchRooms(checkIn, checkOut, GridView3, lblSearch, lblError);
        }
        else
        {
            lblResult.Text = "\"Date From\" must be before \"Date To\".";
        }

    }
}