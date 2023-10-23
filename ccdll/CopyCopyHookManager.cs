using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyHook;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;

namespace CcLib
{
    public class CopyCopyHookManager : IEntryPoint
    {
        IPCServer pcserver;


        /// <summary>
        /// Converts an item identifier list to a file system path. (Note: SHGetPathFromIDList calls the ANSI version, must call SHGetPathFromIDListW for .NET)
        /// </summary>
        /// <param name="pidl">Address of an item identifier list that specifies a file or directory location relative to the root of the namespace (the desktop).</param>
        /// <param name="pszPath">Address of a buffer to receive the file system path. This buffer must be at least MAX_PATH characters in size.</param>
        /// <returns>Returns TRUE if successful, or FALSE otherwise. </returns>
        [DllImport("shell32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SHGetPathFromIDListW(IntPtr pidl, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszPath);


        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = false)]
        public delegate void CopyItems_Delegate(IID_IFileOperation Self, IShellItem punkItems, IPersistIDList psiDestinationFolder);
        

        public CopyCopyHookManager(RemoteHooking.IContext context, string chanelname)
        {
            try
            {
                pcserver = RemoteHooking.IpcConnectClient<IPCServer>(chanelname);
                pcserver.Report("Hook instalado...");
            }
            catch (Exception ex)
            {
                pcserver.Report(ex.Message);
            }
        }

        
        public void Run(RemoteHooking.IContext context, string chanelname)
        {
            try
            {
                COMClassInfo comcinfo = new COMClassInfo(typeof(CLSID_IFileOperation), typeof(IID_IFileOperation), "CopyItems");
                comcinfo.Query();
                
                var hook = LocalHook.Create(comcinfo.MethodPointers[0], new CopyItems_Delegate(CopyItemsHooked), this);
                hook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });

                while (IsProcActive())
                    System.Threading.Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                pcserver.Report($"{ex.Message}\r\n{ex.StackTrace}");
            }
        }



        private bool IsProcActive()
        {
            return Process.GetProcessesByName("cccore").Length > 0;
        }



        public void CopyItemsHooked(IID_IFileOperation Self, IShellItem sourceItem, IPersistIDList psiDestinationFolder)
        {
            // Original behavior
            // Self.CopyItems(punkItems, psiDestinationFolder);
            try
            {
                // Ruta fuente del archivo
                IntPtr p = IntPtr.Zero;
                sourceItem.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out p);
                string from = Marshal.PtrToStringUni(p);


                //
                // Ruta destino del archivo
                // StringBuilder TIENE que tener un buffer en el contructor
                //
                StringBuilder to = new StringBuilder(1024);
                psiDestinationFolder.GetIDList(out IntPtr pidl);
                SHGetPathFromIDListW(pidl, to);

                pcserver.OnFileHook(from, to.ToString(), 0);
            }
            catch (Exception ex)
            {
                pcserver.Report(ex.Message);
            }
        }
        

        ///
        /// https://blog.birost.com/a?ID=00800-293cd1b6-073f-42d4-a921-f4d55e172f59
        /// 
        #region IFileOperation 

        [ComImport]
        [Guid("947aab5f-0a5c-4c13-b4d6-4bf7836fc9f8")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IID_IFileOperation
        {
            uint Advise(IntPtr pfops, IntPtr pdwCookie);

            void Unadvise(uint dwCookie);

            void SetOperationFlags(IntPtr dwOperationFlags);

            void SetProgressMessage([MarshalAs(UnmanagedType.LPWStr)] String pszMessage);

            void SetProgressDialog([MarshalAs(UnmanagedType.Interface)] object popd);

            void SetProperties([MarshalAs(UnmanagedType.Interface)] object pproparray);

            void SetOwnerWindow(uint hwndParent);

            void ApplyPropertiesToItem(IntPtr psiItem);

            void ApplyPropertiesToItems([MarshalAs(UnmanagedType.Interface)] object punkItems);

            void RenameItem(IntPtr psiItem, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName, IntPtr pfopsItem);

            void RenameItems(IntPtr pUnkItems, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

            void MoveItem(
                IntPtr psiItem,
                IntPtr psiDestinationFolder,
                [MarshalAs(UnmanagedType.LPWStr)] string pszNewName,
                IntPtr pfopsItem);

            void MoveItems(
                IntPtr punkItems,
                IntPtr psiDestinationFolder);

            void CopyItem(
                IntPtr psiItem,
                IntPtr psiDestinationFolder,
                [MarshalAs(UnmanagedType.LPWStr)] string pszCopyName,
                IntPtr pfopsItem);

            void CopyItems(
                IntPtr punkItems,
                IntPtr psiDestinationFolder);

            void DeleteItem(
            IntPtr psiItem,
            IntPtr pfopsItem);

            void DeleteItems(
                IntPtr punkItems);

            uint NewItem(
                IntPtr psiDestinationFolder,
                IntPtr dwFileAttributes,
                [MarshalAs(UnmanagedType.LPWStr)] String pszName,
                [MarshalAs(UnmanagedType.LPWStr)] String pszTemplateName,
                IntPtr pfopsItem);

            long PerformOperations();


            [return: MarshalAs(UnmanagedType.Bool)]

            bool GetAnyOperationsAborted();
        }



        [ComVisible(true)]
        [Guid("3ad05575-8857-4850-9277-11b85bdb8e09")]
        public class CLSID_IFileOperation
        {
            // content of their class interfaces may be empty, only the position of the interface to facilitate subsequent determination by the API EasyHook
        }
        #endregion;
    }
    

    /// <summary>
    /// http://pinvoke.net/default.aspx/Enums/SIGDN.html
    /// </summary>
    public enum SIGDN : uint
    {
        SIGDN_NORMALDISPLAY = 0,
        SIGDN_PARENTRELATIVEPARSING = 0x80018001,
        SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000,
        SIGDN_PARENTRELATIVEEDITING = 0x80031001,
        SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000,
        SIGDN_FILESYSPATH = 0x80058000,
        SIGDN_URL = 0x80068000,
        SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001,
        SIGDN_PARENTRELATIVE = 0x80080001,
        SIGDN_PARENTRE
    }


    /// <summary>
    /// http://pinvoke.net/default.aspx/Interfaces/IShellItem.html
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
    public interface IShellItem
    {
        void BindToHandler(IntPtr pbc,
            [MarshalAs(UnmanagedType.LPStruct)]Guid bhid,
            [MarshalAs(UnmanagedType.LPStruct)]Guid riid,
            out IntPtr ppv);

        void GetParent(out IShellItem ppsi);

        void GetDisplayName(SIGDN sigdnName, out IntPtr ppszName);

        void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);

        void Compare(IShellItem psi, uint hint, out int piOrder);
    }


    /// <summary>
    /// Exposes methods that are used to persist item identifier lists.
    /// </summary>
    /// https://github.com/dwmkerr/sharpshell/blob/master/SharpShell/SharpShell/Interop/IPersistIDList.cs
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1079acfc-29bd-11d3-8e0d-00c04f6837d5")]
    public interface IPersistIDList : IPersist
    {
        #region Overriden IPersist Methods

        /// <summary>
        /// Retrieves the class identifier (CLSID) of the object.
        /// </summary>
        /// <param name="pClassID">A pointer to the location that receives the CLSID on return.
        /// The CLSID is a globally unique identifier (GUID) that uniquely represents an object class that defines the code that can manipulate the object's data.</param>
        /// <returns>
        /// If the method succeeds, the return value is S_OK. Otherwise, it is E_FAIL.
        /// </returns>
        [PreserveSig]
        new int GetClassID(out Guid pClassID);

        #endregion

        /// <summary>
        /// Sets a persisted item identifier list.
        /// </summary>
        /// <param name="pidl">A pointer to the item identifier list to set.</param>
        /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [PreserveSig]
        int SetIDList(IntPtr pidl);

        /// <summary>
        /// Gets an item identifier list.
        /// </summary>
        /// <param name="pidl">The address of a pointer to the item identifier list to get.</param>
        /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [PreserveSig]
        int GetIDList(out IntPtr pidl);
    }



    /// https://github.com/dwmkerr/sharpshell/blob/master/SharpShell/SharpShell/Interop/IPersist.cs
    /// <summary>
    /// Provides the CLSID of an object that can be stored persistently in the system. 
    /// Allows the object to specify which object handler to use in the client process, as it is used in the default implementation of marshaling.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000010c-0000-0000-c000-000000000046")]
    public interface IPersist
    {
        /// <summary>
        /// Retrieves the class identifier (CLSID) of the object.
        /// </summary>
        /// <param name="pClassID">A pointer to the location that receives the CLSID on return. 
        /// The CLSID is a globally unique identifier (GUID) that uniquely represents an object class that defines the code that can manipulate the object's data.</param>
        /// <returns>If the method succeeds, the return value is S_OK. Otherwise, it is E_FAIL.</returns>
        [PreserveSig]
        int GetClassID(out Guid pClassID);
    }
}
   
