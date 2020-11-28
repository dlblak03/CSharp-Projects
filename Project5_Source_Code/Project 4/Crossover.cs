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

        //Crossover function, randomly picks a segment of parent two and transfers that segment to parent one. Report will have visual representation
        public static List<int> PMX(List<int> parentOne, List<int> parentTwo, ComboBox Mutation_Method_Box)
        {
            parentOne.RemoveAt(parentOne.Count - 1);
            parentTwo.RemoveAt(parentTwo.Count - 1);

            int geneA = Shuffle.RandomNumber(parentOne.Count);
            int geneB = Shuffle.RandomNumber(parentOne.Count);

            int startingIndex = Math.Min(geneA, geneB);
            int endIndex = Math.Max(geneA, geneB);

            for(int i = startingIndex; i < endIndex; i++)
            {
                int valueParentTwo = parentTwo[i];

                parentOne = Shuffle.Swap(parentOne, i, parentOne.FindIndex(x => x == valueParentTwo));
            }

            //Mutation functions based on users selection
            if (Mutation_Method_Box.SelectedItem.Equals("Reverse Sequence Mutation"))
            {
                parentOne = Mutations.Reverse_Sequence_Mutation(parentOne);
            }

            //Add the starting node back to path
            parentOne.Add(parentOne[0]);

            //Return offspring
            return parentOne;
        }

        public static List<int> CX(List<int> parentOne, List<int> parentTwo, ComboBox Mutation_Method_Box)
        {
            parentOne.RemoveAt(parentOne.Count - 1);
            parentTwo.RemoveAt(parentTwo.Count - 1);

            List<int> offspring = new List<int>(parentOne);

            List<int> indicies = new List<int>();
            List<int> insertedValues = new List<int>();

            for(int i = 0; i < 100; i++)
            {
                indicies.Add(i);
            }

            int index = 1;

            foreach(var value in parentOne)
            {
                if(value == index)
                {
                    parentTwo.Remove(value);
                    insertedValues.Add(value);
                    indicies.Remove(index - 1);
                }
                index++;
            }

            foreach(var i in indicies)
            {
                foreach(var value in parentTwo)
                {
                    if(!insertedValues.Contains(value))
                    {
                        parentTwo.Remove(value);
                        offspring.Insert(i, value);
                        offspring.RemoveAt(i);
                        insertedValues.Add(value);
                        break;
                    }
                }
            }

            //Mutation functions based on users selection
            if (Mutation_Method_Box.SelectedItem.Equals("Reverse Sequence Mutation"))
            {
                offspring = Mutations.Reverse_Sequence_Mutation(offspring);
            }

            offspring.Add(offspring[0]);

            return offspring;
        }
    }
}
