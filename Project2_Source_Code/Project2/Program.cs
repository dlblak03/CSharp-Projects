using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project2
{
    class Program
    {
        /*****************************************************************Specify File Path Here*****************************************************************/
        private static string filePath = @"C:\Users\Dalto\OneDrive\Documents\University of Louisville\CSE 545\Project_2\Project2_Instructions\11PointDFSBFS.tsp";
        /*****************************************************************Specify File Path Here*****************************************************************/

        //Number of vertices variable
        public static int numOfVertices = 11;

        //Smallest cost vairable
        public static double smallestCost = -1;

        //Optimal path variable
        public static string optimalPath;

        //Linkedlist to store the edges
        public static LinkedList<int>[] edges = new LinkedList<int>[numOfVertices];

        //Driver Method
        static void Main(string[] args)
        {
            //Read node data from the input file specified by the user
            double[,] InputData = readFile();

            //Create a LinkedList array to store the edges from each node
            for(int i = 0; i < numOfVertices; i++)
            {
                edges[i] = new LinkedList<int>();
            }

            //Initialize the edges LinkedList with the appropriate data
            edgeData(edges);

            //Initialize user input variable
            string choice = "0";

            //Menu option to choose between DFS or BFS
            while(Int32.Parse(choice) != 3)
            {
                Console.WriteLine("Press (1) for DFS.");
                Console.WriteLine("Press (2) for BFS.");
                Console.WriteLine("Press (3) to quit.\n");

                choice = Console.ReadLine();

                //DFS
                if (Int32.Parse(choice) == 1)
                {
                    //Reset smallestCost and optimalPath variables for new run through
                    smallestCost = -1;
                    optimalPath = "";

                    //Initialize a bool array and a List to store the path for the DFS function
                    bool[] visisted = new bool[numOfVertices];
                    List<int> path = new List<int>();

                    DFS(0, 10, path, visisted, InputData);
                }
                if(Int32.Parse(choice) == 2)
                {
                    //Reset smallestCost and optimalPath variables for new run through
                    smallestCost = -1;
                    optimalPath = "";

                    //Initialize a bool array and a Queue to store the nodes for the BFS function
                    bool[] visisted = new bool[numOfVertices];
                    Queue<int> queue = new Queue<int>();

                    BFS(0, 10, queue, visisted, InputData);
                }
                else if(Int32.Parse(choice) != 3)
                {
                    //Do nothing
                }

                //Print the optimal path and smallest cost
                Console.Clear();
                Console.WriteLine("Optimal Path: " + optimalPath);
                Console.WriteLine("Smallest Cost: " + smallestCost + "\n");
            }
        }

        //Method to get the shortest path using a DFS traversal
        public static void DFS(int source, int target, List<int> path, bool[] visited, double[,] data)
        {
            //Empty path means this is the first DFS call. Add source to path
            if(path.Count == 0)
            {
                path.Add(source);
            }

            //Determines if the target node has been reached
            if(source == target)
            {
                //Get the cost of the path
                getCost(path.ToArray(), data);
            }

            //Set the visited node to true
            visited[source] = true;

            //Traverse each node in the List of connected nodes
            foreach(var value in edges[source])
            {
                //If the node has not been visisted, add the value to path and call DFS again with the new value and new path
                if(!visited[value])
                {
                    path.Add(value);
                    DFS(value, target, path, visited, data);
                    path.Remove(value); //Backtrack to the previous node
                }
            }

            //Set the node currently visited back to false
            visited[source] = false;
        }

        //Method to get the shortest path using a BFS traversal (Does not give shortest path by length rather the shortest path by number of nodes traversered)
        //My report will have a better explanation to this
        public static void BFS(int source, int target, Queue<int> queue, bool[] visited, double[,] data)
        {
            //Queue to store all the different paths
            Queue<List<int>> queuePaths = new Queue<List<int>>();

            //List to store the current path
            List<int> path = new List<int>();

            //Add the starting node to the path
            path.Add(source);

            //Enqueue the current path for later use
            queuePaths.Enqueue(path);

            //Enqueue the current node for later use
            queue.Enqueue(source);

            //Mark the current node as visited
            visited[source] = true;

            //While nodes queue is not empty
            while(queue.Count != 0)
            {
                //Dequeue the current path
                path = queuePaths.Dequeue();

                //Dequeue the current source
                source = queue.Dequeue();

                //If the last value in the path is the target node, get the cost of the path
                if(path[path.Count - 1] == target)
                {
                    getCost(path.ToArray(), data);
                }

                //Traverse the edges for current node
                foreach(var value in edges[source])
                {
                    //If not visited, create a new path concatenated with the new value, mark visited as true and enqueue the path and current node
                    if(!visited[value])
                    {
                        List<int> newPath = new List<int>(path);
                        newPath.Add(value);
                        visited[value] = true;
                        queue.Enqueue(value);
                        queuePaths.Enqueue(newPath);
                    } 
                }
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
            for (int i = 1; i < Nodes.Length; i++)
            {
                //Index x_1 and y_1 value
                nodeOne = Nodes[i - 1];
                //Index for x_2 and y_2 value
                nodeTwo = Nodes[i];

                //Add the cost to itself to get the total cost of each edge, since the x and y coordinates will always be in the same position
                //of the Data array. Only the first parameter of Data needs to have a dynamic value
                cost += Math.Sqrt(Math.Pow(Data[nodeTwo, 1] - Data[nodeOne, 1], 2) + Math.Pow(Data[nodeTwo, 2] - Data[nodeOne, 2], 2));
            }

            for(int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = Nodes[i] + 1;
            }

            //Displays the path and the cost to the console
            Console.Write("Path: " + String.Join(" -> ", Nodes) + "\n" + "Cost: " + cost + "\n\n");

            //Sets first path, which will always be the smallest cost, to the smallest cost variable and updates optimal path
            if (smallestCost == -1)
            {
                smallestCost = cost;
                optimalPath = String.Join(" -> ", Nodes);
            }
            else
            {
                //cost is less than the value stored in the smallest cost, update the appropriate variables with the right values
                if (cost < smallestCost)
                {
                    smallestCost = cost;
                    optimalPath = String.Join(" -> ", Nodes);
                }
            }
        }

        //Method to add data to the edges linkedlist
        public static void edgeData(LinkedList<int>[] edges)
        {
            //Edges connected from node 0
            edges[0].AddLast(1);
            edges[0].AddLast(2);
            edges[0].AddLast(3);

            //Edges connected from node 1
            edges[1].AddLast(2);

            //Edges connected from node 2
            edges[2].AddLast(3);
            edges[2].AddLast(4);

            //Edges connected from node 3
            edges[3].AddLast(4);
            edges[3].AddLast(5);
            edges[3].AddLast(6);

            //Edges connected from node 4
            edges[4].AddLast(6);
            edges[4].AddLast(7);

            //Edges connected from node 5
            edges[5].AddLast(7);

            //Edges connected from node 6
            edges[6].AddLast(8);
            edges[6].AddLast(9);

            //Edges connected from node 7
            edges[7].AddLast(8);
            edges[7].AddLast(9);
            edges[7].AddLast(10);

            //Edges connected from node 8
            edges[8].AddLast(10);

            //Edges connected from node 9
            edges[9].AddLast(10);

            //No edges from 10 to any other node
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
            double[,] InputData = new double[(lines.Count - index), 3];

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
