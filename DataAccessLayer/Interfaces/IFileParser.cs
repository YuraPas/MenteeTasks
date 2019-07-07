using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IFileParser
    {
        List<T> ProccessJsonFile<T>(string pathToJsonFile);
        string[] GetAllLinesFromFile(string path);
    }
}
