using System;

namespace IpcCore
{
    public class TypeDifferException : Exception
    {
        public TypeDifferException(string msg) : base(msg)
        {

        }

        public TypeDifferException(string msg, Exception inner) : base(msg, inner)
        {

        }
    }
}
