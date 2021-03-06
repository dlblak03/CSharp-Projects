﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6
{
    class Mutations
    {
        //Reverse only part of a list, report will contain visual representation
        public static List<int> Reverse_Sequence_Mutation(List<int> Order)
        {
            int numberOne = 0;
            int numberTwo = 0;

            while (numberOne == numberTwo)
            {
                numberOne = Shuffle.RandomNumber_v2(Order.Count);
                numberTwo = Shuffle.RandomNumber(Order.Count - numberOne);
            }

            Order.Reverse(numberOne, numberTwo);

            return Order;
        }
    }
}
