using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyCopy
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
                return;


            bool showOptions = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            for (int i = 0; i < args.Length; i++)
                if (args[i] == "options")
                {
                    showOptions = true;
                    break;
                }

            if (showOptions)
                Application.Run(new Views.Options());
            else
                Application.Run(new MainUI(args));
        }
        
    }
}
