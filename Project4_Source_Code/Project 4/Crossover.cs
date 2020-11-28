using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_4
{
    class Crossover
    {
        public static int Tournament_Selection(List<double> fitnessValue, int range)
        {
            //Initialize to infinity so we can compare
            double temp = double.PositiveInfinity;

            //Run tournament selection test
            for (int x = 0; x < range; x++)
            {
                double fV = fitnessValue[Shuffle.RandomNumber(fitnessValue.Count)];

                if(fV < temp)
                {
                    temp = fV;
                }
            }

            //Return the index so we can find it in the Population list
            return fitnessValue.FindIndex(x => x == temp);
        }

        //Crossover function, randomly picks a segment of parent two and trasnfers that segment to parent one. Report will have visual representation
        public static List<int> PMX(List<int> parentOne, List<int> parentTwo, ComboBox Mutation_Method_Box)
        {
            parentOne.RemoveAt(parentOne.Count - 1);
            parentTwo.RemoveAt(parentTwo.Count - 1);

            int geneA = Shuffle.RandomNumber(parentOne.Count);
            int geneB = Shuffle.RandomNumber(parentOne.Count);

            int startingIndex = Math.Min(geneA, geneB);
            int endIndex = Math.Max(geneA, geneB);

            for(int i = startingIndex; i <= endIndex; i++)
            {
                int valueParentTwo = parentTwo[i];

                parentOne = Shuffle.Swap(parentOne, i, parentOne.FindIndex(x => x == valueParentTwo));
            }

            //Mutation functions based on users selection
            if (Mutation_Method_Box.SelectedItem.Equals("Center Inverse Mutation"))
            {
                parentOne = Mutations.Center_Inverse_Mutation(parentOne);
            }

            if (Mutation_Method_Box.SelectedItem.Equals("Reverse Sequence Mutation"))
            {
                parentOne = Mutations.Reverse_Sequence_Mutation(parentOne);
            }

            //Add the starting node back to path
            parentOne.Add(parentOne[0]);

            //Return offspring
            return parentOne;
        }
    }
}
