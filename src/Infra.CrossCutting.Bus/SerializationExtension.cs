﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Infra.CrossCutting.Bus
{
    public static class SerializationExtension
    {

        public static byte[] Serialize<T>(this T data)
        {
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, data);

                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(this byte[] data)
        {
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                var obj = formatter.Deserialize(ms);

                return (T)obj;
            }
        }
    }
}
