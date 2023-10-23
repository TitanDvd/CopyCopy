using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;
using System.Security.Cryptography;

namespace CcLib.ObjSerializer
{
    public static class Serializer
    {
        public static byte[] Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return memoryStream.ToArray();
            }
        }
        
        public static object Deserialize(byte[] message)
        {
            using (var memoryStream = new MemoryStream(message))
            {
                BinaryFormatter b = new BinaryFormatter();
                memoryStream.Position = 0;
                //b.Binder = new PreMergeToMergeDeserializationBinder();
                return b.Deserialize(memoryStream);
            }
                
        }

        public static string GetBytesHash(byte[] content)
        {
            try
            {
                MD5 hashing = MD5.Create();
                byte[] hash = hashing.ComputeHash(content, 0, content.Length);
                string str = "";
                foreach (var b in hash)
                    str += b.ToString("x2");
                return str;
            }
            catch { return ""; }
        }

        public static string GetInputHash(string input)
        {
            try
            {
                MD5 hashing = MD5.Create();
                byte[] content = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hash = hashing.ComputeHash(content, 0, content.Length);
                string str = "";
                foreach (var b in hash)
                    str += b.ToString("x2");
                return str;
            }
            catch { return ""; }
        }

        sealed class PreMergeToMergeDeserializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type returnType = null;
                if (assemblyName == "ISerializable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
                {
                    returnType = Type.GetType($"{typeName}, {assemblyName}");
                }

                return returnType;
            }
        }
    }

    
}
