using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DataAccessLayer
{
    public static class FileParser
    {
        public static List<T> ProccessJsonFile<T>(string pathToJsonFile)
        {
            using (var jsonReader = new StreamReader(pathToJsonFile))
            {
                string jsonFileLines = jsonReader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<List<T>>(jsonFileLines);

                return data;
            }
        }

        public static string[] GetAllLinesFromFile(string path)
        {
            var lines = File.ReadAllLines(path);

            return lines;
        }

    }
}
