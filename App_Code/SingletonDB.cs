using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

public class SingletonDB
{
    private MySqlConnection connection;
    private MySqlCommand cmd;

    private static SingletonDB instance;

    private SingletonDB() { }

    public static SingletonDB getInstance()
    {
        if (instance == null)
        {
            instance = new SingletonDB();
        }
        return instance;
    }

    //get room info 
    public DataTable getRooms()
    {
        DataTable rooms = new DataTable();
        using (connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT roomid, concat('#',roomno,'  $', price) AS roomInfo FROM room", connection);
                da.Fill(rooms);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while selecting from room: " + ex.Message);
            }
        }
        return rooms;
    }

    //insert new booking
    public void insertBooking(Booking booking, String userID, Label result, Label error)
    {
        Boolean overlap = false;
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT DATE_FORMAT(CheckOut, '%m/%d/%Y') as 'In', DATE_FORMAT(CheckIn, '%m/%d/%Y') as 'Out' FROM booking WHERE roomid = " + booking.roomid, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //check if dates overlap with dates already booked
                    if (booking.checkin <= Convert.ToDateTime(reader["In"]) && Convert.ToDateTime(reader["Out"]) <= booking.checkout)
                    {
                        overlap = true;
                    }
                }
                reader.Close();

                cmd = new MySqlCommand("SELECT userid FROM user WHERE email = @user", connection);
                cmd.Parameters.AddWithValue("@user", userID);
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
                    insertcmd.Parameters.AddWithValue("@checkIn", booking.checkin.ToString("yyyy/MM/dd"));
                    insertcmd.Parameters.AddWithValue("@checkOut", booking.checkout.ToString("yyyy/MM/dd"));
                    insertcmd.Parameters.AddWithValue("@roomid", booking.roomid);
                    insertcmd.Parameters.AddWithValue("@userid", userID);
                    insertcmd.ExecuteNonQuery();

                    result.Text = "Room was booked sucessfully. FROM:" + booking.checkin.ToString("MM/dd/yyyy") + " To:" + booking.checkout.ToString("MM/dd/yyyy");
                }
                else
                {
                    result.Text = "There is a conflict, the room is already booked.";
                }

            }
        }
        catch (Exception ex)
        {
            error.Text = "An error occured while trying to submit: " + ex.Message;
        }
    }

    public void searchRooms(Booking booking, GridView GridView3, Label lblSearch, Label lblError) 
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
            {
                //Fill gridview with rooms that are available for booking based on dates
                MySqlCommand cmd = new MySqlCommand("SELECT room.roomno, room.price"
                    + " FROM room WHERE room.roomid not in"
                    + " (Select roomid From booking where (booking.CheckIn <= @checkOut) AND (booking.CheckOut >= @checkIn))"
                    + " GROUP BY roomid", connection);
                cmd.Parameters.AddWithValue("@checkIn", booking.checkin.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@checkOut", booking.checkout.ToString("yyyyMMdd"));
                DataTable dataTable = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                GridView3.DataSource = dataTable;
                GridView3.DataBind();
                lblSearch.Text = "Rooms available from " + booking.checkin.ToString("MM/dd/yyyy") + " to " + booking.checkout.ToString("MM/dd/yyyy") + ".";
                lblSearch.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "An error occured while trying to search: " + ex.Message;
        }
    }

    public void login(User user, Label lblError, Login Login1)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE email = @user", connection);
                cmd.Parameters.AddWithValue("@user", user.email);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["password"].Equals(user.password))
                    {
                        FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                    }
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "An error occured while trying to submit: " + ex.Message;
        }
    }
}
