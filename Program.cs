using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Newtonsoft.Json;// for reading and serializing json data for further processing 
using Newtonsoft.Json.Linq;

namespace jsondatatest
{
    class Program
    {
        static void Main(string[] args)
        {
            //define a file path for the 
            string readFilePath = @"/Users/rpandey/myprojects/jsondatatest/jsondatatest/csv_data.csv";
            //store in a list item 
            string[] lines = File.ReadAllLines(readFilePath);
            var filePath = @"/Users/rpandey/myprojects/jsondatatest/jsondatatest/write_to_csv.csv";
            //string output = "";
            int x = 0;
            //skip the f
            lines = lines.Skip(1).ToArray();
            foreach (string line in lines)
            {
                
                    // now split the lines and get json data
                    string[] splittedLine = line.Split("|");
                    var obj = JObject.Parse(splittedLine[4]);
                    var result = obj.Descendants()
                    .OfType<JProperty>()
                    .Select(p => new KeyValuePair<string, object>(p.Path,
                        p.Value.Type == JTokenType.Array || p.Value.Type == JTokenType.Object
                            ? null : p.Value));
                var project_id = splittedLine[3];
                    foreach (var kvp in result)
                    {
                        // string.Format added , fro separating key and values any other value like | or : can be given format [key, value, pid(project_id)]
                        // if kvp.Value = "" // do more like add null 
                        File.AppendAllText(filePath, string.Format("{0}, {1} {2} {3}", kvp.Key, kvp.Value, ", PID : "+project_id, Environment.NewLine));

                    }
                    //check how many line is has processed
                    Console.Write("no of rows Processed" + x +"\n");
                    x++;
                
                
            }

            
        }
    }
}
