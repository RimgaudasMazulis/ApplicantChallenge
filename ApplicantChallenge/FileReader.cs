using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApplicantChallenge
{
    public static class FileReader
    {
        public static IEnumerable<String> ReadFile(String filePath)
        {
            List<String> lines = new List<string>();

            try
            {
                String path = Environment.CurrentDirectory + filePath;
                using (StreamReader sr = File.OpenText(path))
                {
                    StringBuilder sb = new StringBuilder();
                    while (sb.Append(sr.ReadLine()).Length > 0)
                    {
                        lines.Add(sb.ToString());
                        sb.Clear();
                    }
                }
                return lines;
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read: " + e.Message);
                return null;
            }
        }
    }
}
