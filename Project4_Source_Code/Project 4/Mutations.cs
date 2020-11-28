using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4
{
    class Mutations
    {
        //Inverses each section of a list, report will contain visual representation
        public static List<int> Center_Inverse_Mutation(List<int> Order)
        {
            int numberOne = 0;
            int numberTwo = 0;

            while (numberOne == numberTwo)
            {
                numberTwo = Shuffle.RandomNumber(Order.Count);
            }

            Order.Reverse(numberOne, numberTwo);

            numberOne = numberTwo;
            numberTwo = Order.Count - numberOne;

            Order.Reverse(numberOne, numberTwo);

            return Order;
        }

        //Reverse only part of a list, report will contain visual representation
        public static List<int> Reverse_Sequence_Mutation(List<int> Order)
        {
            int numberOne = 0;
            int numberTwo = 0;

            while (numberOne == numberTwo)
            {
                numberOne = Shuffle.RandomNumber(Order.Count);
                numberTwo = Shuffle.RandomNumber2(Order.Count - numberOne);
            }

            Order.Reverse(numberOne, numberTwo);

            return Order;
        }
    }
}
