using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer
{
    public class ServiceBLL : IServiceBLL
    {
        public string[] TransformData(string line)
        {
            Regex pattern = new Regex("[ \" \\ ]");
            string newLine = pattern.Replace(line, string.Empty);

            return newLine.Split(",", StringSplitOptions.None);
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
    }
}
