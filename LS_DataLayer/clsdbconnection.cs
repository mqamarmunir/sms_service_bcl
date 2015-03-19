using System;
using System.Data.OleDb;
using System.Configuration;


namespace LS_DataLayer
{
	/// <summary>
	/// Summary description for clsdbconnection.
	/// </summary>
	public class clsdbconnection
	{
		public clsdbconnection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	
		#region Enumerations

		public enum ConnectionType
		{
			ORACLE=1,
			SQLSERVER=2
		}
		#endregion

		#region "Properties"

		/// <summary>
		/// Get SQL Server Connection
		/// </summary>
		public OleDbConnection OleDb_SQL_Connection
		{	
			get { return Dbconnection(clsdbconnection.ConnectionType.ORACLE); }
		}
		#endregion

		#region "Connection String"

		private  OleDbConnection Dbconnection(ConnectionType ConnectToDB) 
		{
			string StrConnection=null;
			
			if (ConnectToDB==ConnectionType.SQLSERVER)
			{
				StrConnection =@"Initial catalog=hrdatabase;data source = (local);user id =sa;password=;";
			}
			else if (ConnectToDB==ConnectionType.ORACLE)
			{
				try
				{
					StrConnection = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
				}
				catch
				{
					StrConnection = "Provider=MSDAORA.1;User ID=whims;Data Source=HIMS;Password=whims";
				}
			
				//StrConnection = "Provider=MSDAORA.1;User ID=whims;Data Source=HIMS;Password=whims";
				//StrConnection = "Provider='OraOLEDB.Oracle.1';User ID=hims;Password=hims;Data Source=hims;Extended Properties=;Persist Security Info=False";
			}
	
			OleDbConnection Conn = new OleDbConnection(StrConnection);
			//Conn.ConnectionString = StrConnection;
            Conn.Open();
			return Conn;

		}
		#endregion

	}
}
