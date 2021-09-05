using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Interfaces
{
    public interface IFileParser
    {
        List<T> ParseFiles<T>(string[] filePath);
        List<T> ParseDirectory<T>(string directoryPath);
        List<T> ParseFile<T>(string filePath);
    }
}
