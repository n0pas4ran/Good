using System;
using System.Collections.Generic;
namespace MyApp
{

    public class Entry
    {
        public string name { get; set; }
        public List<Sizes> size { get; set; }

        public Entry()
        {
            name = "default";

        }

        public Entry(string Name, List<Sizes> sizes)
        {
            name = Name;
            size = sizes;


        }

        public override string ToString()
        {
            string output = name + "\n";
            foreach(var tuple in size)
            {
                output += "Размер РФ/Бренда: " + tuple.rfSize+"/"+tuple.brSize+ "\n";
            }
            return output;
        }
    }
}
