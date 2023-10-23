using CcCore.Base.DbManager;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;

namespace CcCore
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            SetDir();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CCContext());
        }



        private static void SetDir()
        {
            string k = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\App Paths\cccore.exe", "Path", null);
            if(k != null)
                System.IO.Directory.SetCurrentDirectory(k);
        }
    }
}
