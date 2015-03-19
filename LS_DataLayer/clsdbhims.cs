using System;
using System.Data;
using System.Data.OleDb;

namespace LS_DataLayer
{
	/// <summary>
	/// Summary description for clsdbhims.
	/// </summary>
	public class clsdbhims : Iinterface
	{
		public clsdbhims()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		# region "Class_Variables"

		private string StrQuery = null;		

		# endregion

		#region "Properties"

		public string Query
		{
			get { return StrQuery; }
			set { StrQuery = value; }
		}

		# endregion

		# region "Data_Methods"

		public OleDbCommand Insert()
		{
			OleDbCommand objCommand = new OleDbCommand();
			
			objCommand.CommandText = this.Query;
			objCommand.CommandType = CommandType.Text;

			return objCommand;
		}

		public OleDbCommand Get_Max()
		{
			OleDbCommand objCommand = new OleDbCommand();
			
			objCommand.CommandText = this.Query;
			objCommand.CommandType = CommandType.Text;

			return objCommand;
		}

		public OleDbCommand Update()
		{
			OleDbCommand objCommand = new OleDbCommand();
			
			objCommand.CommandText = this.Query;
			objCommand.CommandType = CommandType.Text;

			return objCommand;
		}

		public OleDbCommand Delete()
		{
			OleDbCommand objCommand = new OleDbCommand();
			
			objCommand.CommandText = this.Query;
			objCommand.CommandType = CommandType.Text;

			return objCommand;
		}

		public OleDbCommand Get_All()
		{
			OleDbCommand objCommand = new OleDbCommand();

			objCommand.CommandText = this.Query;
			objCommand.CommandType = CommandType.Text;

			return objCommand;
		}

		public OleDbCommand Get_Single()
		{
			OleDbCommand objCommand = new OleDbCommand();

			objCommand.CommandText = this.Query;
			objCommand.CommandType = CommandType.Text;

			return objCommand;
		}

		#endregion
	}
}