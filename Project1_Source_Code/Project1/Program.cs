using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Project1
{
    class Program
    {
        /**************************************************Specify File Path Here**************************************************/
        private static string filePath = @"C:\Users\Dalto\OneDrive\Documents\University of Louisville\CSE 545\Project1\Random5.tsp";
        /**************************************************Specify File Path Here**************************************************/

        //Variable to keep track of number of permutations
        private static int numOfPermutations = 0;

        //Variable to store the smallest cost of the optimal path
        private static double smallestCost = -1;

        //Variable to store the optimal path
        private static string optimalPath;

        //Driver method
        public static void Main(string[] args)
        {
            //Read node data from the input file specified by the user
            double[,] InputData = readFile();

            //Declare an array to store the number of nodes in the data
            int[] Nodes = new int[InputData.GetLength(0)];

            //Assign the value from the data to the Nodes array
            for (int i = 0; i < InputData.GetLength(0); i++)
            {
                Nodes[i] = Convert.ToInt32(InputData[i, 0]);
            }

            //Inialize an empty list to pass to the Permutations function
            var perm = new List<int>();

            //Call the Permutations function
            Permutations(Nodes, perm, InputData);

            //Display the number of permutations, optimal path, and cost to the console
            Console.WriteLine("\nNumber of Permutations: " + numOfPermutations);
            Console.WriteLine("Optimal Path: " + optimalPath);
            Console.WriteLine("Cost: " + smallestCost);

            Console.ReadLine();
        }

        //Recursive method to find all permutations, explanaiton to algorithm in report
        public static void Permutations(int[] nodes, List<int> perm, double[,] Data)
        {
            if (nodes.Length == 0)
            {
                getCost(perm.ToArray(), Data);
                numOfPermutations++;
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                var newNodes = nodes.Where((n, index) => index != i).ToList();
                perm.Add(nodes[i]);
                Permutations(newNodes.ToArray(), perm, Data);
                perm.RemoveAt(perm.Count - 1);
            }
        }

        //Method to get the cost of each permutation
        public static void getCost(int[] Nodes, double[,] Data)
        {
            //Inialize variables to store the index of node one(x_1, y_1) and node two(x_2, y_2)
            int nodeOne = 0;
            int nodeTwo = 0;
            //Inialize cost variable to zero
            double cost = 0;
            
            //For loop to calculate the cost, gets the index of two nodes making the edge. The Nodes array being passed into the function is the path
            for(int i = 1; i < Nodes.Length; i++)
            {
                //Index x_1 and y_1 value
                nodeOne = Nodes[i - 1] - 1;
                //Index for x_2 and y_2 value
                nodeTwo = Nodes[i] - 1;

                //Add the cost to itself to get the total cost of each edge, since the x and y coordinates will always be in the same position
                //of the Data array. Only the first parameter of Data needs to have a dynamic value
                cost += Math.Sqrt(Math.Pow( Data[nodeTwo, 1] - Data[nodeOne, 1], 2) + Math.Pow( Data[nodeTwo, 2] - Data[nodeOne, 2], 2));
            }
            //Since the final edge is connecting back to the starting node, take the last value stored in node two and calculate the cost by
            //taking the value stored at Nodes[0]
            cost += Math.Sqrt(Math.Pow(Data[Nodes[0] - 1, 1] - Data[nodeTwo, 1], 2) + Math.Pow(Data[Nodes[0] - 1, 2] - Data[nodeTwo, 2], 2));

            //Displays the path and the cost to the console
            Console.Write(String.Join(" ", Nodes) + " " + cost + "\n");

            //Sets first path, which will always be the smallest cost, to the smallest cost variable and updates optimal path
            if(smallestCost == -1)
            {
                smallestCost = cost;
                optimalPath = String.Join(" -> ", Nodes);
                optimalPath += " -> ";
                optimalPath += Nodes[0];
            }
            else
            {
                //cost is less than the value stored in the smallest cost, update the appropriate variables with the right values
                if(cost <= smallestCost)
                {
                    smallestCost = cost;
                    optimalPath = String.Join(" -> ", Nodes);
                    optimalPath += " -> ";
                    optimalPath += Nodes[0];
                }
            }
        }

        //Method to read data from a .tsp file
        public static double[,] readFile()
        {
            //Inialize ID variable for assigning data to InputData array
            int ID = 0;

            //Read all lines from .tsp file into a list
            List<string> lines = File.ReadAllLines(filePath).ToList();

            //Inialize a variable with the value NODE_COORD_SECTION
            string findNode = "NODE_COORD_SECTION";
            //Find the index of where the findNode variable is located in the list
            int index = lines.FindIndex(a => a == findNode);
            //Add one to the index to allocate the right size needed for the array
            index += 1;

            //Declare an array InputData with the correct number of elements in the row column
            double[,] InputData = new double[(lines.Count - index),3];

            //Assign values to the InputData array using a foreach loop to iterate across the lines
            //Skips to the index given to bypass all the meta data until it reaches the first node coordinate
            foreach (string line in lines.Skip(index))
            {
                string[] entries = line.Split(' ');

                InputData[ID, 0] = Convert.ToDouble(entries[0]);
                InputData[ID, 1] = Convert.ToDouble(entries[1]);
                InputData[ID, 2] = Convert.ToDouble(entries[2]);

                ID++;
            }

            //Return the array to access the data in anothe method
            return InputData;
        }

    }
}
