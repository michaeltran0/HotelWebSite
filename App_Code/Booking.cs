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

    public int bookingid { get; set; }
    public int userid { get; set; }
    public int roomid { get; set; }
    public DateTime checkin { get; set; }
    public DateTime checkout { get; set; }
}