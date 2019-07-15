using DataAccessLayer.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccessLayer
{
    public class FileService : IFileService
    {
        private JsonSerializer serializer;
        private readonly string basePath = @"C:\Users\Рома\Downloads\";

        public FileService()
        {
            serializer = new JsonSerializer();
        }
        public List<T> ProccessJsonFile<T>(string jsonFileName)
        {
            using (var jsonReader = new StreamReader(basePath + jsonFileName))
            {
                string jsonFileLines = jsonReader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<List<T>>(jsonFileLines);

                return data;
            }
        }

        public string[] GetAllLinesFromFile(string path)
        {
            var lines = File.ReadAllLines(path);

            return lines;
        }

        public void SerializeToFile<T>(IEnumerable<T> elements, string jsonFileName)
        {
            using (StreamWriter file = File.AppendText(basePath + jsonFileName))
            {
                serializer.Serialize(file, elements);
            }
        }
    }
}
