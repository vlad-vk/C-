using System;
using Microsoft.Win32;

namespace ipcampic
{

    public static class GetDotNetVersion
    {

        private static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 461808)
                return "4.7.2 or later";
            if (releaseKey >= 461308)
                return "4.7.1";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";      
            if (releaseKey >= 393295)
                return "4.6";      
            if (releaseKey >= 379893)
                return "4.5.2";      
            if (releaseKey >= 378675)
                return "4.5.1";      
            if (releaseKey >= 378389)
                return "4.5";      
            return "4.0 later version .NET NOT detected";
        }

        public static void Get45PlusFromRegistry()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null) {
                    gVar.Dver = CheckFor45PlusVersion((int) ndpKey.GetValue("Release"));
                }
                else {
                    gVar.Dver = "4.0 later version .NET not detected.";
                }
            }
        }
    }
}

