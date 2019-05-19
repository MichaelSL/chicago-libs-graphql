using System;
using System.Collections.Generic;
using System.Text;

namespace Ingress.Csv
{
    public static class LinesSplitter
    {
        public static IList<string> SplitLine(string line, char stringDelimeter, char separator)
        {
            var lineValues = new List<string>();
            bool isInSubstring = false;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == stringDelimeter)
                {
                    isInSubstring = !isInSubstring;
                }
                else
                {
                    if (line[i] == separator && !isInSubstring)
                    {
                        lineValues.Add(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }
                    else
                    {
                        stringBuilder.Append(line[i]);
                    }
                }
            }
            lineValues.Add(stringBuilder.ToString());

            return lineValues;
        }
    }
}
