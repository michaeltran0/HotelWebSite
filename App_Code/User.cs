using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class User
{
	public User()
	{
	}

    public User(string email, string password)
    {
        this.email = email;
        this.password = password;
    }

    public int userid { get; set; }
    public string email { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string password { get; set; }
    public string phoneNumber { get; set; }
    public string type { get; set; }
}