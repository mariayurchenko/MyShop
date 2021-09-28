using System;
using System.IO;

namespace Tests
{
    public class FileReader
    {
        private readonly string _path;
        public FileReader(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException();
            }

            _path = path;
        }
        public string ReadFile()
        {
            if (!File.Exists(_path))
            {
                throw new ArgumentException("The " + _path + " does not exist.");
            }

            string file = File.ReadAllText(_path);

            return file;
        }
    }
}