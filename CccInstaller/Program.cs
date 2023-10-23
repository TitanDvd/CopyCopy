using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace CccInstaller
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool buildInstaller = false;
            bool uninstall = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "build")
                    buildInstaller = true;
                if (args[i] == "uninstall")
                    uninstall = true;
            }


            if (buildInstaller)
            {
                string[] appFiles = Directory.GetFiles("Build", "*.*", SearchOption.AllDirectories);
                Dictionary<string, byte[]> filesContent = new Dictionary<string, byte[]>();
                foreach (var file in appFiles)
                {
                    string filePath = file.Replace("Build\\", "");
                    filesContent.Add(filePath, File.ReadAllBytes(file));
                }


                byte[] bin = CcLib.ObjSerializer.Serializer.Serialize(filesContent);
                File.WriteAllBytes("ccc_installer.bin", bin);
            }
            else if(uninstall)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Views.UninstallView());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MUI());
            }
        }
    }
}
