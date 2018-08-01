using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Rjachimek_Pharmacy
{
    public abstract class ActiveRecord
    {
	    public abstract void Save();
	    public abstract void Reload();
	    public abstract void Remove();
        

		protected static SqlConnection Open()
	    {
           string connectionString = "Integrated Security=SSPI;" +
                                 "Data Source=.\\SQLEXPRESS01;" +
                                 "Initial Catalog=PHARMACY;";

            SqlConnection connection = new SqlConnection(connectionString);

		    return connection;
		}

	    protected static SqlConnection Close()
	    {
		   
		    SqlConnection connection = new SqlConnection();
		    connection.ConnectionString = "Integrated Security=SSPI;" +
                                 "Data Source=.\\SQLEXPRESS01;" +
                                 "Initial Catalog=PHARMACY;";
            connection.Close();
		    return connection;
	    }
	}
}
