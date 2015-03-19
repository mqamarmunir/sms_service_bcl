using System;

namespace LS_BusinessLayer
{
	/// <summary>
	/// Summary description for User.
	/// </summary>
	public class User
	{
		#region Class Variable
		private const string Default = "~!@";
		private const string TableName = "LS_TPersonnel";
		private string StrErrorMessage = "";

		private string StrPersonID = Default;
		private string StrActive = Default;
		private string StrSalutation = Default;
		private string StrPersonName = Default;
		private string StrAcronym = Default;
		private string StrSex = Default;
		private string StrRoleID = Default;
		private string StrLoginID = Default;
		private string StrPassword = Default;
		#endregion
		#region Properties
		public string PersonID
		{
			get{	return StrPersonID;	}
			set{	StrPersonID = value;	}
		}
		public string Active
		{
			get{	return StrActive;	}
			set{	StrActive = value;	}
		}
		public string Salutation
		{
			get{	return StrSalutation;	}
			set{	StrSalutation = value;	}
		}
		public string PersonName
		{
			get{	return StrPersonName;	}
			set{	StrPersonName = value;	}
		}
		public string Acronym
		{
			get{	return StrAcronym;	}
			set{	StrAcronym = value;	}
		}
		public string Sex
		{
			get{	return StrSex;	}
			set{	StrSex = value;	}
		}
		public string RoleID
		{
			get{	return StrRoleID;	}
			set{	StrRoleID = value;	}
		}
		public string LoginID
		{
			get{	return StrLoginID;	}
			set{	StrLoginID = value;	}
		}
		public string Password
		{
			get{	return StrPassword;	}
			set{	StrPassword = value;	}
		}
		#endregion

		public User()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
