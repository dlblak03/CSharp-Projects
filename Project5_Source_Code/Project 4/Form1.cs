using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project_4
{
    public partial class TSP_Form : Form
    {
        private static double[,] InputData;

        private static List<int>[] Population;

        private static List<double> fitnessValue;

        private static List<int> bestRouteEver;
        private static double bestFitnessValueEver = double.PositiveInfinity;

        private static List<int>[] nextGeneration;
        private static List<int>[] nextGenerationFinal;

        Stopwatch timer = new Stopwatch();

        public TSP_Form()
        {
            InitializeComponent();
        }

        private void Open_File_Click(object sender, EventArgs e)
        {
            string filePath = null;

            //Popup that allows user to choose file
            OpenFileDialog OFD = new OpenFileDialog();

            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = OFD.FileName;
            }

            //If user does not specify a file path
            if(filePath == null)
            {
                MessageBox.Show("\tDid not specify a file.\t");
                return;
            }

            //Get the data from that file
            InputData = Read_File(filePath);

            //Initialize the chart with the appropriate configuration
            Misc.Initialize_Chart(InputData.GetLength(0), TSP_Chart, Graph_Data);

            //Graph the nodes on the chart
            Graph_Nodes();

            //Disable open file button
            Open_File.Enabled = false;
        }

        private double[,] Read_File(string filePath)
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

        private void Graph_Nodes()
        {
            //Add the nodes to the chart
            for (int i = 0; i < InputData.GetLength(0); i++)
            {
                TSP_Chart.Series[0].Points.AddXY(InputData[i, 1], InputData[i, 2]);
                TSP_Chart.Series[0].Points[i].Label = Convert.ToString(i + 1);
            }
        }

        private void Initialize_Population()
        {
            //Initialize Population Size
            int populationSize = 0;

            //Initialize a list for the cities
            List<int> temp = new List<int>();

            //Calculate the value the user wants to use for population size
            if(Population_Size_Box.SelectedItem.Equals("600"))
            {
                populationSize = 600;
            }

            //Calculate the value the user wants to use for population size
            if (Population_Size_Box.SelectedItem.Equals("1200"))
            {
                populationSize = 1200;
            }

            //Declare list for the population
            Population = new List<int>[populationSize];

            //Creat a list of the cities in order
            for(int i = 0; i < InputData.GetLength(0); i++)
            {
                temp.Add(Convert.ToInt32(InputData[i, 0]));
            }

            //Initialize list for the population
            for(int i = 0; i < populationSize; i++)
            {
                List<int> Cities = new List<int>(temp);
                Population[i] = new List<int>(Shuffle.Random(Cities));
            }

            nextGeneration = new List<int>[Population.Length];
            nextGenerationFinal = new List<int>[Population.Length];
        }

        private void Fitness_Function()
        {
            //Keep track of all the fitness values
            fitnessValue = new List<double>();

            //Calculate the fitness value for each solution and update best ever variables
            for (int i = 0; i < Population.Length; i++)
            {
                double fV = Fitness.Calculate(Population[i], InputData);
                if(fV < bestFitnessValueEver)
                {
                    bestFitnessValueEver = fV;
                    bestRouteEver = new List<int>(Population[i]);
                }

                fitnessValue.Add(fV);
            }
        }

        private void Draw_Route()
        {
            //Draw the route of the path on the chart
            for (int i = 1; i < bestRouteEver.Count; i++)
            {
                TSP_Chart.Series[i].Points.Clear();
                TSP_Chart.Series[i].Points.AddXY(InputData[bestRouteEver[i - 1] - 1, 1], InputData[bestRouteEver[i - 1] - 1, 2]);
                TSP_Chart.Series[i].Points.AddXY(InputData[bestRouteEver[i] - 1, 1], InputData[bestRouteEver[i] - 1, 2]);
            }
            TSP_Chart.Update();
        }

        private void Draw_Route_v2(List<List<int>> expertEdges)
        {
            int i = 1;
            foreach(var edge in expertEdges)
            {
                TSP_Chart.Series[i].Points.Clear();
                TSP_Chart.Series[i].Points.AddXY(InputData[edge[0] - 1, 1], InputData[edge[0] - 1, 2]);
                TSP_Chart.Series[i].Points.AddXY(InputData[edge[1] - 1, 1], InputData[edge[1] - 1, 2]);
                i++;
            }
            TSP_Chart.Update();
        }

        private void Next_Generation()
        {
            List<int> offSpring = new List<int>();

            int range = (int)(fitnessValue.Count * 0.30);

            //For each solution in Population, create a offspring
            for (int i = 0; i < Population.Length; i++)
            {
                //Find parents using tournament selection
                List<int> parentOne = new List<int>(Population[Crossover.Tournament_Selection(fitnessValue, range)]);
                List<int> parentTwo = new List<int>(Population[Crossover.Tournament_Selection(fitnessValue, range)]);

                
                if ((string)Crossover_Box.SelectedItem == "PMX")
                {
                    offSpring = new List<int>(Crossover.PMX(parentOne, parentTwo, Mutation_Method_Box));
                }
                else if ((string)Crossover_Box.SelectedItem == "CX")
                {
                    offSpring = new List<int>(Crossover.CX(parentOne, parentTwo, Mutation_Method_Box));
                }
                
                nextGeneration[i] = offSpring;

                double fitnessOne = Fitness.Calculate(nextGeneration[i], InputData);
                double fitnessTwo = Fitness.Calculate(Population[i], InputData);

                if (fitnessOne < fitnessTwo)
                {
                    nextGenerationFinal[i] = nextGeneration[i];
                    fitnessValue[i] = fitnessOne;
                    if(fitnessOne < bestFitnessValueEver)
                    {
                        bestFitnessValueEver = fitnessOne;
                        bestRouteEver = new List<int>(nextGeneration[i]);
                    }
                }
                else
                {
                    nextGenerationFinal[i] = Population[i];
                    fitnessValue[i] = fitnessTwo;
                    if (fitnessTwo < bestFitnessValueEver)
                    {
                        bestFitnessValueEver = fitnessTwo;
                        bestRouteEver = new List<int>(Population[i]);
                    }
                }
            }

            //Transfer the next generation to the current population
            Population = nextGenerationFinal;
        }

        private void Run_Program_Click(object sender, EventArgs e)
        {
            List<List<int>> expertEdges = new List<List<int>>();
            Dictionary<int, int> visistNodes = new Dictionary<int, int>();
            List<int> remaining = new List<int>();
            //Creat a list of the cities in order
            for (int i = 0; i < InputData.GetLength(0); i++)
            {
                remaining.Add(Convert.ToInt32(InputData[i, 0]));
                visistNodes.Add(Convert.ToInt32(InputData[i, 0]), 0);
            }

            #region GA #1
            timer.Start();
            List<List<int>> firstSolution = new List<List<int>>();
            //Disable run program button
            Run_Program.Enabled = false;

            int Generations = 1;
            int numOfGenerations = (int)(InputData.GetLength(0) * 0.43);

            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }

            //MessageBox.Show(timer.Elapsed.ToString());

            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                firstSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(1, bestFitnessValueEver);
            #endregion

            #region GA #2
            List <List<int>> secondSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                secondSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(2, bestFitnessValueEver);
            #endregion

            #region GA #3
            List<List<int>> thirdSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                thirdSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(3, bestFitnessValueEver);
            #endregion

            #region GA #4
            List<List<int>> fourthSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                fourthSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(4, bestFitnessValueEver);
            #endregion

            #region GA #5
            List<List<int>> fifthSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                fifthSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(5, bestFitnessValueEver);
            #endregion

            #region GA #6
            List<List<int>> sixthSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                sixthSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(6, bestFitnessValueEver);
            #endregion

            #region GA #7
            List<List<int>> seventhSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                seventhSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(7, bestFitnessValueEver);
            #endregion

            #region GA #8
            List<List<int>> eigthSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                eigthSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(8, bestFitnessValueEver);
            #endregion

            #region GA #9
            List<List<int>> ninthSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                ninthSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(9, bestFitnessValueEver);
            #endregion

            #region GA #10
            List<List<int>> tenthSolution = new List<List<int>>();
            bestFitnessValueEver = double.PositiveInfinity;
            Generations = 1;
            Initialize_Population();

            //Fitness Function first
            Fitness_Function();

            while (Generations != numOfGenerations)
            {
                //Then next generation
                Next_Generation();

                //Draw_Route();
                //TSP_Chart.Update();

                //And repeat until max generation is reached
                Generations++;
            }
            foreach (var value in bestRouteEver)
            {
                int index = bestRouteEver.IndexOf(value);
                List<int> temp = new List<int>();
                if (index == bestRouteEver.Count - 1)
                {
                    if (value < bestRouteEver[0])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[0]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[0]);
                        temp.Add(value);
                    }
                }
                else
                {
                    if (value < bestRouteEver[index + 1])
                    {
                        temp.Add(value);
                        temp.Add(bestRouteEver[index + 1]);
                    }
                    else
                    {
                        temp.Add(bestRouteEver[index + 1]);
                        temp.Add(value);
                    }
                }
                tenthSolution.Add(temp);
            }
            Graph_Data.Series[0].Points.AddXY(10, bestFitnessValueEver);
            #endregion

            List<int> segment = new List<int>();
            
            foreach (var value in firstSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in secondSolution)
            {
                int times = 1;
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in thirdSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in fourthSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in fifthSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in sixthSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in seventhSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in eigthSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in ninthSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (tenthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            foreach (var value in tenthSolution)
            {
                int times = 1;
                if (secondSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (thirdSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fourthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (fifthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (sixthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (seventhSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (eigthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (ninthSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (firstSolution.Any(v => v.SequenceEqual(value)))
                {
                    times++;
                }
                if (times > 6)
                {
                    if (!expertEdges.Any(v => v.SequenceEqual(value)))
                    {
                        expertEdges.Add(value);
                        visistNodes[value[0]]++;
                        visistNodes[value[1]]++;
                        if (visistNodes[value[0]] == 2)
                        {
                            remaining.Remove(value[0]);
                        }
                        if (visistNodes[value[1]] == 2)
                        {
                            remaining.Remove(value[1]);
                        }
                    }
                }
            }

            List<int> keys = new List<int>(visistNodes.Keys);

            //Greedy
            foreach (var kvp in keys)
            {
                double dis = double.PositiveInfinity;
                int cityOne = 0;
                int cityTwo = 0;
                if(visistNodes[kvp] == 0)
                {
                    foreach(var value in remaining)
                    {
                        if(value != kvp)
                        {
                            double temp = Math.Abs(Math.Sqrt(Math.Pow(InputData[kvp - 1, 1] - InputData[value - 1, 1], 2) + Math.Pow(InputData[kvp - 1, 2] - InputData[value - 1, 2], 2)));
                            if (temp < dis)
                            {
                                dis = temp;
                                cityOne = kvp;
                                cityTwo = value;
                            }
                        }
                    }
                    visistNodes[cityOne]++;
                    visistNodes[cityTwo]++;
                    if (visistNodes[cityOne] == 2)
                    {
                        remaining.Remove(cityOne);
                    }
                    if (visistNodes[cityTwo] == 2)
                    {
                        remaining.Remove(cityTwo);
                    }
                    List<int> ed = new List<int> { cityOne, cityTwo };
                    expertEdges.Add(ed);
                }
            }

            
            segment.Add(expertEdges[0][0]);
            segment.Add(expertEdges[0][1]);
            while (remaining.Count != 2)
            {
                double dis = double.PositiveInfinity;
                int cityToAdd = 0;
                int cityToRemove = 0;
                int numberAdded = 0;
                int previousNumber = -1;
                while (numberAdded != previousNumber)
                {
                    //Add to path
                    previousNumber = numberAdded;
                    foreach (var value in expertEdges)
                    {
                        if (!segment.Contains(value[1]) && segment.Contains(value[0]))
                        {
                            int indexCity = segment.IndexOf(value[0]);
                            if (indexCity == 0)
                            {
                                segment.Insert(0, value[1]);
                                numberAdded++;
                            }
                            else if (indexCity == segment.Count - 1)
                            {
                                segment.Insert(segment.Count, value[1]);
                                numberAdded++;
                            }
                            else
                            {
                                List<int> compareListOne = new List<int>();
                                List<int> compareListTwo = new List<int>();
                                compareListOne.Add(value[0]);
                                compareListTwo.Add(value[0]);
                                compareListOne.Add(segment[indexCity + 1]);
                                compareListTwo.Add(segment[indexCity - 1]);
                                if (!expertEdges.Any(y => y.SequenceEqual(compareListOne)))
                                {
                                    segment.Insert(indexCity + 1, value[1]);
                                    numberAdded++;
                                }
                                else
                                {
                                    segment.Insert(indexCity, value[1]);
                                    numberAdded++;
                                }
                            }
                        }
                        else if (!segment.Contains(value[0]) && segment.Contains(value[1]))
                        {
                            int indexCity = segment.IndexOf(value[1]);
                            if (indexCity == 0)
                            {
                                segment.Insert(0, value[0]);
                                numberAdded++;
                            }
                            else if (indexCity == segment.Count - 1)
                            {
                                segment.Insert(segment.Count, value[0]);
                                numberAdded++;
                            }
                            else
                            {
                                List<int> compareListOne = new List<int>();
                                List<int> compareListTwo = new List<int>();
                                compareListOne.Add(value[1]);
                                compareListTwo.Add(value[1]);
                                compareListOne.Add(segment[indexCity + 1]);
                                compareListTwo.Add(segment[indexCity - 1]);
                                if (!expertEdges.Any(y => y.SequenceEqual(compareListOne)))
                                {
                                    segment.Insert(indexCity + 1, value[0]);
                                    numberAdded++;
                                }
                                else
                                {
                                    segment.Insert(indexCity, value[0]);
                                    numberAdded++;
                                }
                            }
                        }
                    }
                }
                foreach (var remain in remaining)
                {
                    double temp = Math.Abs(Math.Sqrt(Math.Pow(InputData[remain - 1, 1] - InputData[segment[segment.Count - 1] - 1, 1], 2) + Math.Pow(InputData[remain - 1, 2] - InputData[segment[segment.Count - 1] - 1, 2], 2)));
                    if(temp < dis && !segment.Contains(remain))
                    {
                        dis = temp;
                        cityToAdd = remain;
                        cityToRemove = segment[segment.Count - 1];
                    } 
                }
                List<int> cities = new List<int> { cityToRemove, cityToAdd };
                expertEdges.Add(cities);
                visistNodes[cityToRemove]++;
                visistNodes[cityToAdd]++;
                if (visistNodes[cityToRemove] == 2)
                {
                    remaining.Remove(cityToRemove);
                }
                if (visistNodes[cityToAdd] == 2)
                {
                    remaining.Remove(cityToAdd);
                }
            }

            int numberAdded2 = 0;
            int previousNumber2 = -1;
            while (numberAdded2 != previousNumber2)
            {
                //Add to path
                previousNumber2 = numberAdded2;
                foreach (var value in expertEdges)
                {
                    if (!segment.Contains(value[1]) && segment.Contains(value[0]))
                    {
                        int indexCity = segment.IndexOf(value[0]);
                        if (indexCity == 0)
                        {
                            segment.Insert(0, value[1]);
                            numberAdded2++;
                        }
                        else if (indexCity == segment.Count - 1)
                        {
                            segment.Insert(segment.Count, value[1]);
                            numberAdded2++;
                        }
                        else
                        {
                            List<int> compareListOne = new List<int>();
                            List<int> compareListTwo = new List<int>();
                            compareListOne.Add(value[0]);
                            compareListTwo.Add(value[0]);
                            compareListOne.Add(segment[indexCity + 1]);
                            compareListTwo.Add(segment[indexCity - 1]);
                            if (!expertEdges.Any(y => y.SequenceEqual(compareListOne)))
                            {
                                segment.Insert(indexCity + 1, value[1]);
                                numberAdded2++;
                            }
                            else
                            {
                                segment.Insert(indexCity, value[1]);
                                numberAdded2++;
                            }
                        }
                    }
                    else if (!segment.Contains(value[0]) && segment.Contains(value[1]))
                    {
                        int indexCity = segment.IndexOf(value[1]);
                        if (indexCity == 0)
                        {
                            segment.Insert(0, value[0]);
                            numberAdded2++;
                        }
                        else if (indexCity == segment.Count - 1)
                        {
                            segment.Insert(segment.Count, value[0]);
                            numberAdded2++;
                        }
                        else
                        {
                            List<int> compareListOne = new List<int>();
                            List<int> compareListTwo = new List<int>();
                            compareListOne.Add(value[1]);
                            compareListTwo.Add(value[1]);
                            compareListOne.Add(segment[indexCity + 1]);
                            compareListTwo.Add(segment[indexCity - 1]);
                            if (!expertEdges.Any(y => y.SequenceEqual(compareListOne)))
                            {
                                segment.Insert(indexCity + 1, value[0]);
                                numberAdded2++;
                            }
                            else
                            {
                                segment.Insert(indexCity, value[0]);
                                numberAdded2++;
                            }
                        }
                    }
                }
            }

            segment.Add(segment[0]);
            remaining.Remove(segment[0]);
            List<int> lastEdge = new List<int> { segment[0], remaining[0]};
            expertEdges.Add(lastEdge);

            List<int> bestRouteEverOriginal = new List<int>(segment);
            bestRouteEver = new List<int>(segment);

            //2-opt post processing search
            double cost = 0;
            for (int x = 1; x < bestRouteEver.Count; x++)
            {
                int nodeOne = bestRouteEver[x - 1] - 1;
                int nodeTwo = bestRouteEver[x] - 1;

                cost += Math.Sqrt(Math.Pow(InputData[nodeTwo, 1] - InputData[nodeOne, 1], 2) + Math.Pow(InputData[nodeTwo, 2] - InputData[nodeOne, 2], 2));
            }
            Repeat:
            double compare = 0;
            for (int i = 1; i <= bestRouteEver.Count - 2; i++)
            {
                for (int k = i + 1; k <= bestRouteEver.Count - 1; k++)
                {
                    compare = 0;
                    bestRouteEver.Reverse(i, k - i);
                    for (int x = 1; x < bestRouteEver.Count; x++)
                    {
                        int nodeOne = bestRouteEver[x - 1] - 1;
                        int nodeTwo = bestRouteEver[x] - 1;

                        compare += Math.Sqrt(Math.Pow(InputData[nodeTwo, 1] - InputData[nodeOne, 1], 2) + Math.Pow(InputData[nodeTwo, 2] - InputData[nodeOne, 2], 2));
                    }
                    if(compare < cost)
                    {
                        cost = compare;
                        bestRouteEverOriginal = new List<int>(bestRouteEver);
                        goto Repeat;
                    }
                    bestRouteEver = new List<int>(bestRouteEverOriginal);
                }
            }
            bestRouteEver = new List<int>(bestRouteEverOriginal);
            Draw_Route();

            for(int i = 1; i < 11; i++)
            {
                Graph_Data.Series[1].Points.AddXY(i, cost);
            }

            Graph_Data.ChartAreas[0].AxisY.Minimum = cost - 50;
            Graph_Data.ChartAreas[0].AxisY.LabelStyle.Format = "0";

            #region display data
            //Find cost of final route

            Path_Text_Box.Text = String.Join("->", bestRouteEver.ToArray());
            Cost_Text_Box.Text = Convert.ToString(cost);
            TextBox Timer_Box = new System.Windows.Forms.TextBox();
            Timer_Box.BackColor = Color.Black;
            Timer_Box.BorderStyle = BorderStyle.None;
            Timer_Box.Font = new Font(Timer_Box.Font.FontFamily, 14);
            Timer_Box.ForeColor = Color.White;
            Timer_Box.Width = 125;
            Graph_Data.Controls.Add(Timer_Box);
            Timer_Box.Text = (timer.Elapsed).ToString();
            Timer_Box.Location = new Point(Graph_Data.ClientSize.Width - Timer_Box.Width - 22, 70);
            #endregion
        }

        private void Copy_Path_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Path_Text_Box.Text);
        }

        private void Copy_Cost_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Cost_Text_Box.Text);
        }
    }
}
