using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IServiceBLL
    {
        string[] TransformData(string line);
        bool IsValid(string[] items, string timeZone);

    }
}
