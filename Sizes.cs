using System;
namespace MyApp
{
    public class Sizes
    {
        public int rfSize { get; set; }
        public int brSize { get; set; }

        public Sizes()
        {
            rfSize = 0;
            brSize = 0;
        }
        public Sizes(int rSize, int bSize)
        {
            rfSize = rSize;
            brSize = bSize;

        }


    }
}
