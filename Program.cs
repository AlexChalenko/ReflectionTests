using Newtonsoft.Json;
using System.Diagnostics;

namespace ReflectionTests
{
    public class Program
    {
        static void Main(string[] args)
        {
            var f = F.Get();
            var serialized = string.Empty;
            int testCount = 100000;

            Console.WriteLine("Мой рефлекшен:");
            serialized = MakeSerializationTest(f, testCount, SimpleCSVSerializer.Serialize);
            MakeDeserializationTest(serialized, testCount, SimpleCSVSerializer.Deserialize<F>);

            Console.WriteLine("Стандартный механизм (NewtonsoftJson):");
            serialized = MakeSerializationTest(f, testCount, JsonConvert.SerializeObject);
            MakeDeserializationTest(serialized, testCount, JsonConvert.DeserializeObject<F>);
        }

        static string MakeSerializationTest(object obj, int testCount, Func<object, string> serializeFunc)
        {
            var serialized = string.Empty;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < testCount; i++)
            {
                serialized = serializeFunc(obj);
            }
            stopwatch.Stop();
            Console.WriteLine($"Время на сериализацию: {stopwatch.ElapsedMilliseconds} ms");
            return serialized;
        }

        static void MakeDeserializationTest(string serialized, int testCount, Func<string, object> deserializeFunc)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < testCount; i++)
            {
                var deserialized = deserializeFunc(serialized);
            }
            stopwatch.Stop();
            Console.WriteLine($"Время на десериализацию: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
