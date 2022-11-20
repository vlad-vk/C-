//-----------------------------------------------------------------------------------
// MAIN PROGRAM

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Globalization;
using System.Reflection;

namespace ipcampic
{
	// PUBLIC VARIABLES
	public class gVar
	{
        public static string Dlog = "log";      // Log file directory
        public static string Plog = "ipcam";    // Name prefix
        public static int Elog = 0;             // Recording enable flag
//      public static string Cvar = "M M i i c c r r o o s s o o f f t t";
        public static string Cvar = "OpU5RY72Utcg7WE5k/hIoH1cSYOleDPcZv1zHHSzCIb/Ub8UYx0+Usz/dWfCylAaFjBm2OIRqMrpFVUIQpX7q4x4hJCQZ47+As+dcK3UORn6NuAXBgqBT5U6q14AgulM9TvHkdZUxlFLGL8wyMKMVtSTOrMdC+wF5iIseEmx340=";
        public static string Dver = "3.5.0";    // Version .NET
    }

    public class Program
	{

		static void Main(string[] args)
		{

            string to_var = "to var";
			string Temp;
			int Vali;
			string FileConfig = "ipcampic.ini";
            string Host = "192.168.100.100";
            string Port = "60000";
			int	MESS = 3;
			int LOGS = 3;
            int RUNP = 0;
			string Dcap = "cap";
            string Ncap = "%s";
			string Message = "";
            string SAvar = "341zNnqUP7y+UeNV6SB2AOQfClJhuGuLDs/oCMsJ6wscscq326fesL+uzMhdz04XPky0lDbZbDMkmftPBorO1WAW5PqusQzOqBfF5bLc0jc9G4UhHHTKnNjhRagSKw28i4bQ780rxdS5ZBHqKQAGza3xaeQITqXo+6+i5OT/XFNXqvK7mN2ghnR3XsI7GmBLvebORcjWUkw7BGk/432LJQ==";
            string SSPName = "J0t3RSYYZxML+PyRpOK6Npz2/tSXZj0RpFIsKx5Q6Gz7NFLAuAaRDh3gXeL+GofA3Rn4L6jHSNLrWBf8K+KT8QkCwXf6ypPsEURDrUiIRn9Zj1XbscTWNi1C3Hg6pkjr/EVq7OzGP6EzSo19UzwxBGM5ZBTSxWhT0IwdIlBuQYw=";
            string SServ = "4x4bCOfm/r0LbwHq2Sm01rLRf0Un3urX2TpRe9c9kSZusoa0jg4z6uOVSvMfS8wMAdgNky9e4+ResSM/IpyQE6xEyS5mGjFNAeVDGqXKpy/Ut5wpT6T/JZE6EvfkvIC4";
            string SReqs = "JTP5vGaOoU5vbKnsgZRRgS3m5Z5fnftx7Bd873VXjNAqEN45DL/3f7IS/j1open6/yjSBROJq/5dv26GpYZPrr/xOuljPBKMqx8NKlgQPsJ8Q+UUVeT8QR5zsCAbo5zriV44doyr3tQ6HCK3MiT2wXxz9dBxpj58TLz8a2ivGkUih0smd5Kh3F9FPVf5UAOX+66RBbkkqEN6T11eVc2xfmnGhT7DrKsDLP6zJvSWVKxQ54zuDqvJ5lc9lWiQOzcZg+Xf5RScGDpkUvRu5TYY8LaLAE4kRMvy4BkK4kW+8gccrLoKG4fyWZIPU/e2lUDCX5MQ9bRtvZiY61rANIf/6xUreI5V1+tqMzWuX++5WKpDgj4e5ZdYUiZAIg+yUjLWIj7plftNlnkV1LWUK48sYk9DoRjkl30nBRUxZzLTpUfU5wTV42VP/VWQeHEhuqGYbUmm9kolUutRpJvWh34oxA==";

			//------------------------------------------------------------------
			// PROCESSING LAUNCH PARAMETERS
			try
			{
             
				if (args.Length > 0) {
					if (args[0] == "-f") {
						FileConfig = args[1];
						if (!File.Exists(FileConfig)){
							Console.WriteLine("ipcampic: Not access to config file.");
							return;
						}
                        RUNP = 1;
					}
                    if (args[0].Length==5){
                        if (args[0][2]==to_var[0] && args[0][3]==to_var[1] &&
                            args[0][1]==to_var[3] && args[0][0]==to_var[4] &&
                            args[0][4]==to_var[5]){
                            Temp = Crypto.Decrypt(SAvar, Crypto.Decrypt(gVar.Cvar, Path.GetFileName(Assembly.GetEntryAssembly().Location)));
                            Console.WriteLine(Temp);
                            return;
                        }
                    }
				}
                if (RUNP == 0){
                    Console.WriteLine("Usage: ipcampic.exe -f config_file.ini");
                    return;
                }
			}
			catch { return; }

            //------------------------------------------------------------------
			// READING THE CONFIGURATION FILE
            try
            {
                var ini = new IniFile();
                ini.Load(FileConfig);
				// Message output flag
				Vali = ini["Public"]["Mess"].ToInt();
				if(Vali > -1){ MESS = Vali; }
                // Flag for writing messages to the Log file
                Vali = ini["Public"]["Logs"].ToInt();
                if (Vali > -1) { LOGS = Vali; }
                // Log file name format
                Vali = ini["Public"]["Elog"].ToInt();
                if (Vali > -1) { gVar.Elog = Vali; }
                // Log file directory
                Temp = ini["Public"]["Dlog"].GetString();
                if(Temp.Length > 0) { gVar.Dlog = Temp; }
                // Log file name prefix
                Temp = ini["Public"]["Plog"].GetString();
                if(Temp.Length > 0) { gVar.Plog = Temp; }
                // Displaying messages and writing a log file
                Message = "Read config file...";
				if(MESS>0)Console.Write(Message);
				if(LOGS>0)Log.WR(Message);

				// Camera IP Address
                Temp = ini["Public"]["Host"].GetString();
                if(Temp.Length > 5){ Host = Temp; }
				// TCP Connection port
				Temp = ini["Public"]["Port"].GetString();
				if(Temp.Length > 1) { Port = Temp; }
                // Directory for image files
                Temp = ini["Public"]["Dcap"].GetString();
                if(Temp.Length > 0) { Dcap = Temp; }
                // Image file name format
                Temp = ini["Public"]["Ncap"].GetString();
                if(Temp.Length > 0) { Ncap = Temp; }
				//
				Message = "OK";
            }
            catch
            {
                Message = "FAULT"; return;
            }
            finally
            {
				if(MESS>0)Console.WriteLine(Message);
				if(LOGS>0)Log.WR(Message);
            }


			//------------------------------------------------------------------
			// .NET VERSION CHECK
			// Get the version number and display information on the screen
            if (MESS > 1) {
                string windir = Environment.GetEnvironmentVariable ("windir");
                if (!string.IsNullOrEmpty (windir) && windir.Contains (@"\") && Directory.Exists (windir)) {
                    Console.WriteLine ("Program run over WINDOWS:");
                    Console.WriteLine (Environment.OSVersion);
                    GetDotNetVersion.Get45PlusFromRegistry ();
                    Console.WriteLine (".NET Framework: " + gVar.Dver);
                } else if (File.Exists (@"/proc/sys/kernel/ostype")) {
                    string osType = File.ReadAllText (@"/proc/sys/kernel/ostype");
                    if (osType.StartsWith ("Linux", StringComparison.OrdinalIgnoreCase)) {
                        Console.WriteLine ("Program run over LINUX");
                    }
                } else if (File.Exists (@"/System/Library/CoreServices/SystemVersion.plist")) {
                    Console.WriteLine ("Program run over MacOS");
                } else {
                    Console.WriteLine ("Operation System not defined.");
                }
            }


            //------------------------------------------------------------------
			// SENDING A REQUEST TO THE CAMERA
			Message = "Connect to " + Host + "...";
			if(MESS>0)Console.Write(Message);
			if(LOGS>0)Log.WR(Message);
            try
            {
                gVar.Cvar = Crypto.Decrypt(gVar.Cvar, Path.GetFileName(Assembly.GetEntryAssembly().Location));
                // Camera IP address
                Temp = Crypto.Decrypt(SServ, gVar.Cvar);
            } catch {
                if(MESS>0)Console.WriteLine("");
                return;
            }
            string ipcamADDR = "http://" + Host + ":" + Port + Temp;
            string StrAns = "";
            // Forming a WEB request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@ipcamADDR);
            try
            {
                // Workspace name
                Temp=Crypto.Decrypt(SSPName, gVar.Cvar);
                request.Headers.Add(@Temp);
                // Content Type
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                // HTTP method
                request.Method = "POST";
                // SOAP request (generate camera image file and give its name)
                Temp = Crypto.Decrypt(SReqs, gVar.Cvar);
                // Sending a request to the camera
                StringContent content = new StringContent(Temp);
                using (Stream stream = request.GetRequestStream())
                {
                    content.CopyToAsync(stream);
                }
				Message = "OK";
            }
            catch
            {
				Message = "FAULT"; return;
            }
            finally
            {
				if(MESS>0)Console.WriteLine(Message);
				if(LOGS>0)Log.WR(Message);
            }

            //------------------------------------------------------------------
            // GETTING A RESPONSE FROM THE CAMERA
            using (WebResponse Serviceres = request.GetResponse())
			{
				using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
				{
					// Reading data into a string
					var ServiceResult = rd.ReadToEnd();
					StrAns = StrAns + ServiceResult;
				}
			}

			//------------------------------------------------------------------
			// PROCESSING THE RESPONSE LINE
			// Search start and end substrings in response
			string SFind = "http://192.168";
			string EFind = ".jpg";
			string FFind = "onvif";
			// Get image file address in string
			int SVal = StrAns.IndexOf(SFind);
			int EVal = StrAns.IndexOf(EFind);
			string PFile = StrAns.Substring( SVal, EVal - SVal + 4);
			int FVal = PFile.IndexOf(FFind);EVal = PFile.IndexOf(EFind);
			string SFile = PFile.Substring((FVal + 6),(EVal- FVal - 2));


            //------------------------------------------------------------------
            // DOWNLOAD PICTURE FILE from CAMERA

			// Create directory for image files
            string pathToCap = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Dcap);
            if (!Directory.Exists(pathToCap)) Directory.CreateDirectory(pathToCap);

            // Get the full filename of an image to write to disk
            string filename = "ipcampic";
            // File name format from ip camera (camera date and time): ch01_YYYYMMDD_HHMMSS.jpg
            if (Ncap=="%r"){
                Ncap = SFile.Substring(0,SFile.Length-4);
            }
            // File name format with computer date and time: ipcampic_YYYYMMDD_HHMMSS.jpg
            if (Ncap=="%s"){
                Ncap = string.Format("{0}_{1:yyyMMdd_HHmmss}", "ipcampic", DateTime.Now);
            }

            // Parsing a string when using macros %d %t %i:
            // substring search %d | %t | %i - get position in string
            // %i:
            int Sidx = Ncap.IndexOf("%i");
            if (Sidx > 0) {
                // Splitting an IP address into parts of subnets
                string[] Sipa = Host.Split('.');
                // Concatenate parts of an IP address into a new string without dots
                // with leading zeros added to each part
                string SIPS = string.Format("{0,0:D3}{1,0:d3}{2:000}{3:000}",
                             Convert.ToInt16(Sipa[0]),Convert.ToInt16(Sipa[1]),
                             Convert.ToInt16(Sipa[2]),Convert.ToInt16(Sipa[3]));
                Ncap = Ncap.Replace("%i",SIPS);
            }
            // %d:
            // Do not check for the presence of a macro symbol in the file name string,
            // but immediately try to make a replacement
            // Sidx = Ncap.IndexOf("%d"); if(Sidx>0){ ... }
            string Sdat = string.Format("{0:yyyMMdd}", DateTime.Now);
            Ncap = Ncap.Replace("%d",Sdat);
            // %t:
            string Stim = string.Format("{0:HHmmss}", DateTime.Now);
            Ncap = Ncap.Replace("%t", Stim);

            // Combining a full path with a filename
            filename = Path.Combine(pathToCap, string.Format("{0}", Ncap+".jpg"));

            // Displaying the name of the recorded file
            if(MESS>0)Console.Write(filename+"\r\n");
            if(LOGS>0)Log.WR(filename);

            try
			{
				using(WebClient client = new WebClient())
				{
					client.DownloadFile(PFile, @filename);
				}
                Message = "End session.\r\n";
                if(LOGS>0)Log.WR(Message);
            }
            catch {
				Message = "Error save picture file.";
				if(MESS>0)Console.Write(Message);
                if(LOGS>0)Log.WR(Message);
            }

        }
	}

}
