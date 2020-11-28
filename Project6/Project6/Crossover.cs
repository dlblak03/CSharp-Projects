using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6
{
    class Crossover
    {
        public static int Tournament_Selection(List<int> difference, int range)
        {
            //Initialize to infinity so we can compare
            double temp = double.PositiveInfinity;

            //Run tournament selection test
            for (int x = 0; x < range; x++)
            {
                int fV = difference[Shuffle.RandomNumber_v2(difference.Count)];

                if (fV < temp)
                {
                    temp = fV;
                }
            }

            //Return the index so we can find it in the Population list
            return difference.FindIndex(x => x == temp);
        }

        public static List<int> PMX(List<int> parentOne, List<int> parentTwo)
        {
            int geneA = Shuffle.RandomNumber(parentOne.Count);
            int geneB = Shuffle.RandomNumber(parentOne.Count);

            int startingIndex = Math.Min(geneA, geneB);
            int endIndex = Math.Max(geneA, geneB);

            for (int i = startingIndex; i < endIndex; i++)
            {
                int valueParentTwo = parentTwo[i];

                parentOne = Shuffle.Swap(parentOne, i, parentOne.FindIndex(x => x == valueParentTwo));
            }
                
            parentOne = Mutations.Reverse_Sequence_Mutation(parentOne);

            //Return offspring
            return parentOne;
        }
    }
}
