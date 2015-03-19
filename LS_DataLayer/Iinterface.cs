using System;
using System.Data.OleDb;


namespace LS_DataLayer
{
	/// <summary>
	/// Summary description for IInterface.
	/// </summary>
	 public interface Iinterface
	{

/*		 string PKeycode
		 {
			 get;
			 set;
		 }
*/

		#region "Methods"

		 OleDbCommand Insert();
		 
		 OleDbCommand Update();

		 OleDbCommand Delete();

		 OleDbCommand Get_All();

		 OleDbCommand Get_Single();

		 OleDbCommand Get_Max();
	
		#endregion
 
	 }
}
