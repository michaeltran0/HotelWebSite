using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Room
/// </summary>
public class Booking
{
	public Booking()
	{
	}

    public Booking(DateTime checkin, DateTime checkout)
    {
        this.checkin = checkin;
        this.checkout = checkout;
    }

    public Booking(DateTime checkin, DateTime checkout, int roomid)
    {
        this.checkin = checkin;
        this.checkout = checkout;
        this.roomid = roomid;
    }

    public int bookingid { get; set; }
    public int userid { get; set; }
    public int roomid { get; set; }
    public DateTime checkin { get; set; }
    public DateTime checkout { get; set; }
}