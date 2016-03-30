using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Room
/// </summary>
public class Room
{
	public Room()
	{
	}

    public int roomid { get; set; }
    public int roomno { get; set; }
    public int floorno { get; set; }
    public char room_type { get; set; }
    public int price { get; set; }
    public char bathroom { get; set; }
    public string notes{ get; set; }
}