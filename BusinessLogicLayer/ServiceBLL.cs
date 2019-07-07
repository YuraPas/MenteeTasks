using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer
{
    public class ServiceBLL : IServiceBLL
    {
        public string[] SplitLine(string line)
        {
            string newLine = RemoveQuotes(line);

            return newLine.Split(",", StringSplitOptions.None);
        }

        public string RemoveQuotes(string line)
        {
            string newLine = line.Replace("\"", string.Empty);

            return newLine;
        }

        public bool IsValid(string[] items, string timeZone)
        {
            string IATACode = items[4];
            string ICAOCode = items[5];

            if (Regex.IsMatch(IATACode, @"(^$)") || IATACode == @"\N" ||
                Regex.IsMatch(ICAOCode, @"(^$)") || ICAOCode == @"\N" ||
                items.Length != 11 ||
                timeZone == null)
            {
                return false;
            }

            return true;
        }

        public bool SerializedFilesExist()
        {
            if (FileExists("countries.json") &&
                FileExists("cities.json") &&
                FileExists("airports.json"))
            {
                return true;
            }

            return false;
        }

        private bool FileExists(string fileName)
        {
            string basePath = @"C:\Users\Рома\Downloads\";

            if (File.Exists(basePath + fileName))
            {
                return true;
            }

            return false;
        }

        public List<Airport> AssignGatheredData(List<Airport> airports, List<City> cities, List<Country> countries)
        {
            int range = airports.Count();

            for (int i = 0; i < range; i++)
            {
                airports[i].City = cities[i];
                airports[i].City.Country = countries[i];
                airports[i].Country = countries[i];
            }

            return airports;
        }

    
}
}
