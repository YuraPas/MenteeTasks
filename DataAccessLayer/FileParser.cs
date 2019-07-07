using DataAccessLayer.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccessLayer
{
    public class FileParser : IFileParser
    {
        public List<T> ProccessJsonFile<T>(string pathToJsonFile)
        {
            using (var jsonReader = new StreamReader(pathToJsonFile))
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
    }
}
