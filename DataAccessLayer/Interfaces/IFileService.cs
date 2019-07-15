using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IFileService
    {
        List<T> ProccessJsonFile<T>(string pathToJsonFile);
        string[] GetAllLinesFromFile(string path);
        void SerializeToFile<T>(IEnumerable<T> elements, string path);
    }
}
