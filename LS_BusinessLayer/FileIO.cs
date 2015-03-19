/*============================================================================
 *	 If this program is defect free it was written by :
 *			"Author : Waqar Ali Khan"  
 *			Company : Trees Software (pvt) ltd
 *			Initial Start : 19/02/2006  
 * ============================================================================*/
using System;							// standard
using System.IO;						// for file read / write / create / delete ect...
using System.Windows.Forms;				// for ListBox
using System.Text.RegularExpressions;	// for Regex
using System.Collections;				// for ArrayList

namespace FILE_MODS
{
	/// <summary>
	/// Summary description for FileIO.
	/// Items needed in the Form.
	/// ___________________________
	/// using FILE_MODS;
	/// ___________________________
	/// private FileIO FileIO;
	/// private ReadFileTextBox RFT;
	/// ___________________________
	/// // InitializeComponent();
	/// FileIO = new FileIO();
	/// ___________________________
	/// // private void InitializeComponent()
	/// // 
	///	// Form1
	///	// 
	/// this.Controls.Add(this.RFT); 
	/// ____________________________
	/// 
	/// </summary>
	public class FileIO
	{
		int Cnt = 0;
		public string path , FileName , sOut , sOut1;
		public bool MyPath , MyFile , FA;
		private static FileStream aFile;
		private static StreamReader sr;

		public string[] astr;

		/// <summary>
		/// DelFile as it states you can delete the specified file with a legitimate path and file name.
		/// If the path is invalid, file does not exist, or is bieng used by another process you will recive an error.
		/// string "sOut" is the ouput from the readfile method.
		/// </summary>
		/// <param name="Path"></param>
		/// <param name="FileName"></param>
		public void DelFile(string Path , string FileName)
		{	
			string delpath = Path + "\\" + FileName;
			try 
			{
				//check if file exists
				if (File.Exists(delpath))
				{
					File.Delete( delpath );
					// Success
					//MessageBox.Show ( "File " + FileName + " deleted." , "Mission Accomplished !" , MessageBoxButtons.OK , MessageBoxIcon.Information );
					return;
				}
				// No file found...
				//MessageBox.Show ( "File " + FileName + " not found! \nThere is no File to Delete." , "Missing File !!" , MessageBoxButtons.OK , MessageBoxIcon.Error );
				return;
			}
			catch (Exception e)
			{
				//MessageBox.Show( e.Message + " \n There may have been a problem deleting the temp file." , "Error on file deletion !" , MessageBoxButtons.OK , MessageBoxIcon.Error);
			}
			finally{}
		}		
		/// <summary>
		///  AppendLastLine will only append "Create a new line @ end of file". 
		/// If file / Dir is invalid or the file cannot be accessed you will recieve an error.
		/// sApended is the string you want to add to the end of file!
		/// </summary>
		/// <param name="Path"></param>
		/// <param name="FileName"></param>
		/// <param name="sApended"></param>
		public void AppendLastLine( string Path , string FileName , string sApended)
		{		// Unsure if bak file is absolutly neccessary...
			Cnt++;
			string sOutFileName = Path + "\\" + FileName;
			string sOutBak = Path + "\\" + "txt.bak"; 
			//string sApended = textBox5.Text;
			try 
			{
				if ( File.Exists( sOutFileName ) == false )
				{
					//MessageBox.Show("The file " + FileName + " does not exist in \n" + Path + "\n @ " +  File.GetCreationTime(Path), "File \"write\" Mission Failed !" , MessageBoxButtons.OK , MessageBoxIcon.Error );
					return;
				}
				if ( File.Exists( sOutFileName ))
				{
					// Need to create a copy of the file
					File.Copy( sOutFileName , sOutBak , true );

					//Append the text from hex.bak to this file.
					TextWriter outfile= File.AppendText( sOutFileName );

					//Append text from this file to CopytoFile.txt.
					TextReader infile = File.OpenText( sOutBak );

					outfile.WriteLine( sApended);
					//outfile.WriteLine( sApended + ":" + Cnt.ToString());

					infile.Close();
					outfile.Close();


					File.Delete( sOutBak );
					// on success
					//MessageBox.Show("The file " + FileName + " was writen to successfully at \n" + Path + "\n @ " +  File.GetCreationTime(Path), "File \"write\" Mission Accomplished !" , MessageBoxButtons.OK , MessageBoxIcon.Information );
							
					return;
				}
			}
			catch( IOException ex )
			{
				//MessageBox.Show( "Error: Writing to File. "  + ex.Message + " " + sOutFileName );       
			}

		}	
		/*  Append line
				 *  
				 * Original ... from http://www.experts-exchange.com/Programming/Programming_Languages/C_Sharp/Q_20738522.html
				 *	//Make sure you have included this 2 library.
				 *	using System;
					using System.IO

					//Make sure you have prepared this 2 variables
					private static FileStream aFile;
					private static StreamReader sr;

					//To Initial
					aFile = new FileStream("a.txt",FileMode.Open);
					sr = new StreamReader(aFile);

					//To Read the file into a string
					str = sr.ReadToEnd();

					//Remember to close it after use
					sr.Close();
					aFile.Close();

					textBox1.text = str; 
		*/ 


		/// <summary>
		/// CreateFile  as it states: Given a legitimate path and full file name it will create the file in the specified path.
		/// If file already exits, you will recieve an error. 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="FileName"></param>
		public void CreateFile( string path , string FileName )
		{	// check for directory
			if ( Directory.Exists( path ) )
			{
				try
				{
					
					// Determine whether the directory exists so we can create the file in it.
					if ( File.Exists( path + "\\" + FileName ) == false ) 
					{
						// Create FileStream to create a new file in the specified path...
						FileStream CreateFileName = new FileStream( path + "\\" + FileName , FileMode.CreateNew );

						// If succesfull
						//MessageBox.Show("The file " + FileName + " was created successfully at \n" + path + "\n @ " +  File.GetCreationTime(path), "Write file Mission Accomplished !" , MessageBoxButtons.OK , MessageBoxIcon.Information );
						// Always close the stream at the HIGHEST level ....
						CreateFileName.Close();
						return;
					}	
					// No Path
					//MessageBox.Show ( "File " + FileName + ".\n Already Exists." , "Cannot Create File it already exists !" , MessageBoxButtons.OK , MessageBoxIcon.Error );
					return;
				}
				catch ( IOException e) 
				{
					// On Error
					//MessageBox.Show("The process failed: ", e.ToString() + " \n There may have been a problem creating the file in\n" + path + "\\" + FileName + "@ " +  Directory.GetCreationTime(path) , MessageBoxButtons.OK , MessageBoxIcon.Error);
				} 
				finally {}
			}
			// No Path
			//MessageBox.Show ( "Please create the Dir " + path + ".\n Then retry the file create operation." , "Cannot Create File the directory does not exist !" , MessageBoxButtons.OK , MessageBoxIcon.Error );

		}
		/// <summary>
		/// WriteLine  checks for a valid path and file name. If the file exists it will write/rewrite the file's FIRST line only !
		/// </summary>
		/// <param name="path"></param>
		/// <param name="FileName"></param>
		public void WriteLine( string path , string FileName , string sWriteLine )
		{
			// Path does not exist...
			if ( Directory.Exists( path ) == false ) 
			{
				//MessageBox.Show ( "Directory path is nonexistent ! \n Please create directory and path , and try again." , "Directory path is nonexistent !" , MessageBoxButtons.OK , MessageBoxIcon.Error );
				return;
			}
			// File does not exist
			if ( File.Exists( path + "\\" + FileName ) == false ) 
			{
				//MessageBox.Show ( "File is nonexistent ! \n Please create the file then try again." , "File is nonexistent !" , MessageBoxButtons.OK , MessageBoxIcon.Error );
				return;
			}	
			// Build the Path ....
			string sOutFileName = path + "\\" + FileName;

			// Now we now the PATH , DIR and FILE Exist so we can write to it....
			try 
			{	
				// The using statement also closes the StreamWriter.
				using (StreamWriter sw = new StreamWriter( sOutFileName ) ) 
				{
					// Add some text to the file.
					sw.Write( sWriteLine + "\r\n");
					//					sw.Close();
				}

				// on success
				//MessageBox.Show("The file " + FileName + " was writen to successfully at \n" + path + "\n @ " +  File.GetCreationTime(path), "File \"write\" Mission Accomplished !" , MessageBoxButtons.OK , MessageBoxIcon.Information );

				return;
			}
			catch( Exception ex )
			{	// On error creating writing to file
				//MessageBox.Show( "Error:  writing to created file \n" + ex.Message + "\n" + sOutFileName );       
			}
			finally{}
			
		}
		/// <summary>
		/// Directory path test:
		/// Using an integer for an overload..*******
		/// 
		/// int 1 = CreateDir(path)  checks for legitimate fully qualified path Dir.
		/// *******
		/// int 2 = CreateFile(path , FileName) checks for legitimate fully qualified path and file name with extension.
		/// path = a fully qualified Drive and Dir.
		/// </summary>
		/// <param name="TEST"></param>
		public void PathTest( int TEST , string path )
		{	
			/*
			 *				boolean statement to check if "PATH" is a legitamite one.
			 *	 \w means Matches any word character including underscore _ . Equivalent to ‘[A-Za-z0-9_]’
			 *		We are only testing the first 4 CHAR / Digits to see if they "COULD" be a legitamite path
			 *									  [ 1st ] 2 3 4										*/
			MyPath = Regex.IsMatch( path , @"[a-zA-Z]:\\\w" , RegexOptions.IgnoreCase );
			// Check if the path is legitamite
			if ( MyPath == false )
			{
				//MessageBox.Show("Please Input a valid Path" + "\n Example C:\\ThisDir");
				return;
			}
				
			// int 1
			if ( ( path == "" || path == null ) && TEST == 1 )
			{	// If Path is null
				//MessageBox.Show("Please Insert A Directory Path and Name.", "No Path Exsits" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
				return;
			}
				
			//			MyFile = Regex.IsMatch( FileName , @"\w.[a-zA-Z_]" , RegexOptions.IgnoreCase );
			//			// Check if the path is legitamite
			//			if ( MyFile == false )
			//			{
			//				MessageBox.Show("Please Input a valid File Name !" + "\n Example \"MyFile101.txt\".");
			//				return;
			//			}
			// int 2
			//			if ( ( FileName == "" || FileName == null ) && TEST == 2 )
			//			{	// If filename is null
			//				MessageBox.Show("Please Insert A File Name and extension.", "No File Name Exsits" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
			//				return;
			//			}

			if ( MyPath == true  && TEST == 1 ) // Check for path and proper int
				CreateDir( path );
			
			//			if ( MyPath == true  && TEST == 2 ) // Check for Path / filename and proper int
			//				CreateFile( path , FileName );

		}
		/// <summary>
		/// Directory & File test:
		/// /// Using an integer for an overload..*******
		/// 
		/// int 1 = CreateDir(path)  checks for legitimate full qualifyin path.
		/// *******
		/// int 2 = CreateFile(path , FileName) checks for legitimate fully qualifyin path and file name with extension.
		/// 
		/// path = a fully qualified Drive and Dir. FileName = a legitimate filename and extension.
		/// </summary>
		/// <param name="TEST"></param>
		/// <param name="path"></param>
		/// <param name="FileName"></param>
		public void PathTest( int TEST , string path , string FileName )
		{	
			/*
			 *				boolean statement to check if "PATH" is a legitamite one.
			 *	 \w means Matches any word character including underscore _ . Equivalent to ‘[A-Za-z0-9_]’
			 *		We are only testing the first 4 CHAR / Digits to see if they "COULD" be a legitamite path
			 *									  [ 1st ] 2 3 4										*/
			MyPath = Regex.IsMatch( path , @"[a-zA-Z]:\\\w" , RegexOptions.IgnoreCase );
			// Check if the path is legitamite
			if ( MyPath == false )
			{
				//MessageBox.Show("Please Input a valid Path" + "\n Example C:\\ThisDir");
				return;
			}
				
			// int 1
			//			if ( ( path == "" || path == null ) && TEST == 1 )
			//			{	// If Path is null
			//				MessageBox.Show("Please Insert A Directory Path and Name.", "No Path Exsits" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
			//				return;
			//			}
				
			MyFile = Regex.IsMatch( FileName , @"\w.[a-zA-Z_]" , RegexOptions.IgnoreCase );
			// Check if the path is legitamite
			if ( MyFile == false )
			{
				//MessageBox.Show("Please Input a valid File Name !" + "\n Example \"MyFile101.txt\".");
				return;
			}
			// int 2
			if ( ( FileName == "" || FileName == null ) && TEST == 2 )
			{	// If filename is null
				//MessageBox.Show("Please Insert A File Name and extension.", "No File Name Exsits" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
				return;
			}

			//			if ( MyPath == true  && TEST == 1 ) // Check for path and proper int
			//				CreateDir( path );
			
			if ( MyPath == true  && TEST == 2 ) // Check for Path / filename and proper int
				CreateFile( path , FileName );

		}
		/// <summary>
		/// CreateDir(string path) uses the DirectoryCreate method.
		/// Also checks to see if the path exsits. 
		///  If successfull it will enable the Delete Dir button also.
		/// </summary>
		/// <param name="path"></param>
		public void CreateDir(string path)
		{	
			if ( MyPath == true )
				try 
				{
					// Determine whether the directory exists.
					if (Directory.Exists(path)) 
					{
						//MessageBox.Show("The path " + path + " exists already.", "Directory Exsits" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
						// enable the option to delete the directory..
						//this.btnDelDir.Enabled = true;
						return;
					}			
					// Try to create the directory.
					DirectoryInfo di = Directory.CreateDirectory(path);
					// If succesfull
					//MessageBox.Show("The directory was created successfully at \n" + path + "\n @ " +  Directory.GetCreationTime(path), "Success" , MessageBoxButtons.OK , MessageBoxIcon.Information );
					// enable the option to delete the directory..
					//this.btnDelDir.Enabled = true;
				}			
				catch (Exception e) 
				{
					// On Error
					//MessageBox.Show("The process failed: ", e.ToString() + " \n There may have been a problem creating the DIR in\n" + path + "@ " +  Directory.GetCreationTime(path));
				} 
				finally {}
				
		}
		// ReadFile : Will read a text file into a spcified textBox.
		// If the file does not Exist or cannot be accessed you will recieve an error.
		/// <summary>
		/// ReadFile : Will read a text file into a spcified textBox(RFT).
		/// If the file does not Exist or cannot be accessed you will recieve an error.
		/// Example of use on a form Button click:
		/// 		FileIO.ReadFile( path , FileName );
		///			this.RFT.Text = FileIO.sOut;
		/// </summary>
		/// <param name="path"></param>
		/// <param name="FileName"></param>
		public void ReadFile( string path , string FileName )
		{
			ReadFileTextBox RFT = new ReadFileTextBox();
			// Clear the text Box
			RFT.Text = "";
			
			// Build the Path ....
			string sInFileName = path + "\\" + FileName;
			try
			{
				// Check for file and path
				if ( !File.Exists( sInFileName ) )
				{
					//MessageBox.Show( sInFileName + " does not exist.", FileName + " Missing" );
					return;
				}
				//	To Initial
				aFile = new FileStream(sInFileName,FileMode.Open);
				sr = new StreamReader(aFile);

				//To Read the file into a string
				sOut = sr.ReadToEnd();

				//Remember to close it after use
				sr.Close();
				aFile.Close();

				RFT.Text = sOut; 
			}
			catch ( IOException e )
			{
				//MessageBox.Show( "Problem with reading file " + FileName + ".\n" + e.Message , "Read file error !" , MessageBoxButtons.OK , MessageBoxIcon.Error );
			}
		
			finally{}
		}

		//http://authors.aspalliance.com/olson/methods/

		/// <summary>
		/// FileToArray this will loop thru a file and place one string @ a time into a listBox.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="FileName"></param>
		/// <returns></returns>
		public string[] FileToArray( string path , string FileName )
		{
			// a fake array to return so we do not PUKE. Do not know why it is needed.....
			string[] fake = null;
			// Build the Path ....
			string sInFileName = path + "\\" + FileName;
			
			try
			{
				if ( File.Exists( sInFileName ))
				{
					ArrayList al = new ArrayList();
					StreamReader sr = File.OpenText(sInFileName);
					string str = sr.ReadLine();
					while( str != null )
					{
						al.Add(str);
						str = sr.ReadLine();
					}
					sr.Close();
					string[] astr = new string[al.Count];
					al.CopyTo(astr);
						
					FA = true;
					return astr;
						
				}
				//else 
					//MessageBox.Show ( "File not found" , "File not found" , MessageBoxButtons.OK , MessageBoxIcon.Error );
			}
			catch ( Exception e)
			{
				//MessageBox.Show( e.Message , "Error in file." , MessageBoxButtons.OK , MessageBoxIcon.Error );
			}
			finally{}
			FA = false;
			return fake;
		
		}

		/// <summary>
		/// DeleteDir: as is states will delete the directory and ALL of its contents!
		/// It will be disposed of into the recycle bin. 
		/// If the path does not exist or it is being used by another process you will recieve an error.
		/// </summary>
		/// <param name="path"></param>
		public void DeleteDir(string path) // Deletes Dir and contents to the recycle bin
		{		// Check if path exists
			if ( Directory.Exists( path ) && ( path.Length >= 4 ) )
			{	
				
				// trim path down to root dir   "C:\SOMEDIR"  if we do not do this 
				//							when Directory.Delete is called only the last subDir of the path is deleted..

				// Get path length
				int plength = ( path.Length  );
				// Search the string after "C:\" for an occurance of another backslash
				int nextBackSlash = path.IndexOf( "\\" , 3  );
				// if nextBackSlash is greater then or equal to zero we must trim the path including 
				//														the xtra backslash to delete the DIR and all its contents
				if ( nextBackSlash >= 0 )
					path = path.Remove( nextBackSlash , plength - nextBackSlash );
				
				// Initializes the variables to pass to the MessageBox.Show method.
				string message = "Do you want to delete " + path + " and all of it's contents ?";
				string caption = "Delete Directory ?";
				MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult result;
				// Displays the MessageBox.
				//	removed	( this , .....
				result = MessageBox.Show( message, caption, buttons,
					MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 
					MessageBoxOptions.RightAlign);
				// User chooses to delete dir
				if( result == DialogResult.Yes)
				{
					try
					{	
						// Delete the dir and ALL of it;s contents...
						Directory.Delete( path , true );
						//MessageBox.Show("Directory & Contents Deleted.", "Mission Accomplished" , MessageBoxButtons.OK , MessageBoxIcon.Information);
						// Disable the option to delete the directory..
						//this.btnDelDir.Enabled = false;
						return;
					}
						// catch any errors
					catch (Exception e) 
					{
						// On Error
						//MessageBox.Show("The process failed: " + e.Message + " \n There may have been a problem deleting the DIR in\n" + path , "ERROR !!", MessageBoxButtons.OK , MessageBoxIcon.Error );
					} 
					finally {}
				}
			}
			//else // If the Path does not exsit.
				//MessageBox.Show("Directory Path does not exsist !", "No Path Exsits" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
		}

	}

}
	/// <summary>
	/// A TextBox For FileRead.
	/// 
	/// </summary>
public class ReadFileTextBox : TextBox
{
	private bool clearTextBox;

	/// <summary>
	/// "ReadFileTextBox" initializes the textbox thru this portion of the class.
	/// </summary>
	public ReadFileTextBox()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		this.Name = "ReadFileTextBox";
		this.Multiline = true;
		this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.Size = new System.Drawing.Size(624, 208);
		this.Text = "";	
	}

	/// <summary>
	///Clears the textBox "Unfinished Function"
	/// </summary>
	public bool ClearTextBox
	{
		get { return clearTextBox; }
		set { clearTextBox = value; }
	}
}
