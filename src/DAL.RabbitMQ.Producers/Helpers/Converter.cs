using DAL.RabbitMQ.Producers.Bodies;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DAL.RabbitMQ.Producers.Helpers
{
    public static class Converter
    {
        internal static byte[] ToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
