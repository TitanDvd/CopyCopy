using System;

namespace IpcCore
{
    public class EmptyPacketException:Exception
    {
        public EmptyPacketException(string msg):base(msg)
        {

        }

        public EmptyPacketException(string msg, Exception inner):base(msg, inner)
        {

        }
    }
}
