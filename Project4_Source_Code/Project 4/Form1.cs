using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            if(Population_Size_Box.SelectedItem.Equals("1000"))
            {
                populationSize = 1000;
            }

            //Calculate the value the user wants to use for population size
            if (Population_Size_Box.SelectedItem.Equals("2000"))
            {
                populationSize = 2000;
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
                List<int> Cities = temp.ToList();
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
        }

        private void Next_Generation()
        {
            int range = (int)(fitnessValue.Count * 0.4);
            
            //For each solution in Population, create a offspring
            for (int i = 0; i < Population.Length; i++)
            {
                //Find parents using tournament selection
                List<int> parentOne = new List<int>(Population[Crossover.Tournament_Selection(fitnessValue, range)]);
                List<int> parentTwo = new List<int>(Population[Crossover.Tournament_Selection(fitnessValue, range)]);

                List<int> offSpring = new List<int>(Crossover.PMX(parentOne, parentTwo, Mutation_Method_Box));
                
                nextGeneration[i] = offSpring;

                double fitnessOne = Fitness.Calculate(nextGeneration[i], InputData);
                double fitnessTwo = Fitness.Calculate(Population[i], InputData);

                if (fitnessOne < fitnessTwo)
                {
                    nextGenerationFinal[i] = nextGeneration[i];
                }
                else
                {
                    nextGenerationFinal[i] = Population[i];
                }
            }

            //Transfer the next generation to the current population
            Population = nextGenerationFinal;
        }

        private void Run_Program_Click(object sender, EventArgs e)
        {
            //Create data table to have the value of each generation
            DataTable dt = new DataTable();

            for(int i = 0; i < 141; i++)
            {
                dt.Columns.Add(Convert.ToString(i + 1), typeof(string));
            }

            dt.Rows.Add();
            dt.Rows[0][0] = "Generation";
            dt.Rows.Add();
            dt.Rows[1][0] = "Cost";

            //Disable run program button
            Run_Program.Enabled = false;

            int Generations = 1;

            Initialize_Population();

            while (Generations != 141)
            {
                //Fitness Function first
                Fitness_Function();
                
                //Then next generation
                Next_Generation();

                Draw_Route();
                TSP_Chart.Update();

                //Add values to compare on a graph
                Graph_Data.Series[0].Points.AddXY((double)Generations, bestFitnessValueEver);
                Graph_Data.Update();

                //Add values to data table
                dt.Rows[0][Generations] = Convert.ToString(Generations);
                dt.Rows[1][Generations] = Convert.ToString(Math.Round(bestFitnessValueEver, 2));

                //And repeat until max generation is reached
                Generations++;
            }

            //Draw the final route
            Draw_Route();

            //Display it in textbox
            Path_Text_Box.Text = String.Join("->", bestRouteEver.ToArray());

            double cost = 0;

            //Find cost of final route
            for (int x = 1; x < bestRouteEver.Count; x++)
            {
                int nodeOne = bestRouteEver[x - 1] - 1;
                int nodeTwo = bestRouteEver[x] - 1;

                cost += Math.Sqrt(Math.Pow(InputData[nodeTwo, 1] - InputData[nodeOne, 1], 2) + Math.Pow(InputData[nodeTwo, 2] - InputData[nodeOne, 2], 2));
            }

            //Display it in textbox
            Cost_Text_Box.Text = Convert.ToString(cost);

            //Show the data table
            dataGridView1.DataSource = dt;
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
