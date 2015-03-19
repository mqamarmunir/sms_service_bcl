using System;
using System.Data;  
using DataLayer;

namespace LS_BusinessLayer
{
	/// <summary>
	///		Application	:	Hospital Information & Management System (HIMS)
	///		Class for	:	"Organizations" Table
	///		Developer	:	Trees Software (Pvt) Ltd.
	///		Date		:	April 2005
	/// 	Type		:	Business Layer Class
	/// </summary>
	
	public class clsBLUMatrix
	{
		public clsBLUMatrix()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region "Class Variables"
		
		private const string Default = "~!@";
		private const string TableName = "um_tprofile";
		private string StrErrorMessage = "";		
		private string StrPersonID = Default;	

		private string StrApplicationID =  Default;		
		private string StrFormID =  Default;
        private string StrVersion = Default;

		#endregion

		#region "Properties"	
		
		public string ErrorMessage
		{			
			get{	return StrErrorMessage;	}
		}

		public string PersonID
		{
			get{	return StrPersonID;	}
			set{	StrPersonID = value;	}
		}	

		public string ApplicationID
		{
			get{	return StrApplicationID;	}
			set{	StrApplicationID = value;	}
		}			
		
		public string FormID
		{
			get{	return StrFormID;	}
			set{	StrFormID = value;	}
		}
        public string Version
        {
            get { return StrVersion; }
            set { StrVersion = value; }
        }

		#endregion
		
		clsoperation objTrans = new clsoperation();
		clsdbhims objdbhims = new clsdbhims();

		#region "Methods"

		public DataView GetAll(int flag)
		{
			switch(flag)
			{
				case 1:
					objdbhims.Query = "select Count(*) As Rec from um_tprofile t Where PersonID = '"+ StrPersonID +"' And ApplicationID = '"+ StrApplicationID +"' And FormID = '"+ StrFormID +"'";						
					break;

				case 2:
					objdbhims.Query = "select version from um_tapplication where applicationid='"+StrApplicationID+"' and version='"+StrVersion+"'" ;
					break;
			}

			return objTrans.DataTrigger_Get_All(objdbhims);
		}

		#endregion
	}
}
