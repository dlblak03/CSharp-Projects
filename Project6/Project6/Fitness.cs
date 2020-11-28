using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6
{
    class Fitness
    {
        public static int Calculate(int subListOne, int subListTwo)
        {
            int difference = subListOne - subListTwo;

            bool negative = difference < 0;

            if(negative)
            {
                difference = difference * (-1);
            }

            return difference;
        }
    }
}
