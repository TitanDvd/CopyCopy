using CcCore.Base.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CcCore.Base
{
    /// <summary>
    /// Active Copy Handlers
    /// Contiene informacion y metodos para el manejo de copias de archivos
    /// </summary>
    internal class ACHNDL
    {
        Dictionary<string, List<string>> _sourceFiles;
        Form wndForm;
        public ICCD Device;

        public ACHNDL(Dictionary<string, List<string>> entrys)
        {
            wndForm = new Views.CopyCopyMF();
            wndForm.Show();
        }


        public void UpdateCopyList(List<string> files, string dest)
        {

        }

        public void PerformCopy()
        {

        }


        public void PauseCopy()
        {

        }


        public void ResumeCopy()
        {

        }


        public void CancelCopy()
        {

        }
    }
}
