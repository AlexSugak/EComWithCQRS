using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ECom.EventStore.Redis
{
    public class JsonSerializer
    {
        private IEnumerable<Type> types;

        public JsonSerializer(params Type[] knownTypes)
        {
            this.types = knownTypes;
        }

        public T Deserialize<T>(string data)
        {
            using (var mem = new MemoryStream(data.Length))
            {
                using (var w = new StreamWriter(mem))
                {
                    w.Write(data);
                    w.Flush();
                    mem.Seek(0, SeekOrigin.Begin);
                    return this.Deserialize<T>(mem);
                }
            }
        }

        public string Serialize<T>(T obj)
        {
            using (var mem = new MemoryStream())
            {
                this.Serialize(mem, obj);
                mem.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(mem))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private T Deserialize<T>(Stream input)
        {
            var dcs = new DataContractJsonSerializer(typeof(T), this.types);
            return (T)dcs.ReadObject(input);
        }

        private void Serialize<T>(Stream output, T graph)
        {
            var dcs = new DataContractJsonSerializer(typeof(T), this.types);
            dcs.WriteObject(output, graph);
        }
    }
}
