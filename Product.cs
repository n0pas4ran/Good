using System;
namespace Goods
{
    public class Product
    {

        public string name { get; set; }
        public string description { get; set; }
        public string bName { get; set; }
        public int size { get; set; }

        public Product()
        {
            name = "";
            description = "";
            bName = "";
            size = 0;
        }

        public Product(string n, string d, string b, int s)
        {
            name = n;
            description = d;
            bName = b;
            size = s;
        }

        public override string ToString()
        {
            return name + "  " + bName +"  " + size + "\n" + description;
        }
    }
}
