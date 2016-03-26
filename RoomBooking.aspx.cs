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
    protected void Page_Load(object sender, EventArgs e)
    {
        display();
    }

    public void display()
    {
        if (!IsPostBack)
        {
            //display rooms in drop down list
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
            {
                try
                {
                    DataTable rooms = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT roomid, concat('#',roomno,'  $', price) AS roomInfo FROM room", connection);
                    da.Fill(rooms);
                    ddlRooms.DataSource = rooms;
                    ddlRooms.DataTextField = "roomInfo";
                    ddlRooms.DataValueField = "roomid";
                    ddlRooms.DataBind();
                }
                catch (Exception ex)
                {
                    lblError.Text = "An error occured while selecting from room: " + ex.Message;
                }
            }
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
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblResult.Text = "";
        lblError.Text = "";
        Boolean overlap = false;
        int roomid = Int32.Parse(ddlRooms.SelectedItem.Value);
        String userID = "";

        using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
        {
            DateTime checkIn = Convert.ToDateTime(txtCheckIn.Text);
            DateTime checkOut = Convert.ToDateTime(txtCheckOut.Text);

            //check if date from is before the date to
            if (checkIn < checkOut)
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT DATE_FORMAT(CheckOut, '%d/%m/%Y') as 'In', DATE_FORMAT(CheckIn, '%d/%m/%Y') as 'Out' FROM booking WHERE roomid = " + roomid, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //check if dates overlap with dates already booked
                        if (checkIn <= Convert.ToDateTime(reader["In"]) && Convert.ToDateTime(reader["Out"]) <= checkOut)
                        {
                            overlap = true;
                        }
                    }
                    reader.Close();

                    cmd = new MySqlCommand("SELECT userid FROM user WHERE email = @user", connection);
                    cmd.Parameters.AddWithValue("@user", HttpContext.Current.User.Identity.Name);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userID = reader["userid"].ToString();
                    }
                    reader.Close();

                    //insert booking if no overlap
                    if (overlap == false)
                    {
                        MySqlCommand insertcmd = connection.CreateCommand();
                        insertcmd.CommandText = "INSERT INTO booking(CheckIn, CheckOut, roomid, userid) Values(@checkIn, @checkOut, @roomid, @userid)";
                        insertcmd.Parameters.AddWithValue("@checkIn", checkIn.ToString("yyyyMMdd"));
                        insertcmd.Parameters.AddWithValue("@checkOut", checkOut.ToString("yyyyMMdd"));
                        insertcmd.Parameters.AddWithValue("@roomid", roomid);
                        insertcmd.Parameters.AddWithValue("@userid", userID);
                        insertcmd.ExecuteNonQuery();
                        lblResult.Text = "Room was booked sucessfully. FROM:" + checkIn.ToString("yyyy/MM/dd") + " To:" + checkOut.ToString("yyyy/MM/dd");
                        display();
                    }
                    else
                    {
                        lblResult.Text = "There is a conflict, the room is already booked.";
                    }

                }
                catch (Exception ex)
                {
                    lblError.Text = "An error occured while trying to submit: " + ex.Message;
                }
            }
            else
            {
                lblResult.Text = "\"Check In date\" must be before \"Check Out date\".";
            }
           
        }
    }

    //Search for available rooms
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblResult.Text = "";
        lblError.Text = "";

        using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
        {
            DateTime checkIn = Convert.ToDateTime(txtCheckIn.Text);
            DateTime checkOut = Convert.ToDateTime(txtCheckOut.Text);

            //check if date from is before the date to
            if (checkIn < checkOut)
            {
                try
                {
                    //Fill gridview with rooms that are available for booking based on dates
                    MySqlCommand cmd = new MySqlCommand("SELECT room.roomno, room.price"
                        + " FROM room WHERE room.roomid not in" 
                        + " (Select roomid From booking where (booking.CheckIn <= @checkOut) AND (booking.CheckOut >= @checkIn))"
                        + " GROUP BY roomid", connection);
                    cmd.Parameters.AddWithValue("@checkIn", checkIn.ToString("yyyyMMdd"));
                    cmd.Parameters.AddWithValue("@checkOut", checkOut.ToString("yyyyMMdd"));
                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    GridView3.DataSource = dataTable;
                    GridView3.DataBind();
                    lblSearch.Text = "Rooms available from " + checkIn.ToString("dd/MM/yyyy") + " to " + checkOut.ToString("dd/MM/yyyy") + ".";
                    lblSearch.Visible = true;
                }
                catch (Exception ex)
                {
                    lblError.Text = "An error occured while trying to search: " + ex.Message;
                }
            }
            else
            {
                lblResult.Text = "\"Date From\" must be before \"Date To\".";
            }

        }
    }
}