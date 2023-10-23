using System;
namespace CcLib
{
    public class IPCServer : MarshalByRefObject
    {
        public delegate void SednMsg(string libname);
        public static event SednMsg OnSendMessage;


        public delegate void FileHook(string sources, string to, byte eventType);
        public static event FileHook OnFileHooked;
        

        public void OnFileHook(string sources, string to, byte eventType)
        {
            OnFileHooked?.Invoke(sources, to, eventType);
        }


        public void Report(string obj)
        {
            OnSendMessage?.BeginInvoke(obj, null, null);
        }
    }
}
   
