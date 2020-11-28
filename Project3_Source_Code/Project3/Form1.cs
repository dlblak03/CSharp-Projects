using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project3
{
    public partial class Form1 : Form
    {
        //Variable to store user specified file path and data from the file
        private static string filePath;
        private static double[,] InputData;

        //Variable to store the starting node, and a compare variable to disable/enable run program button
        private int? startingNode = null;
        private int compareStarting = -1;

        //Variable to store the optimal path to get the smallest cost
        private static List<int> optimalPath = new List<int>();

        //List to store the order the nodes are added
        private static List<int> order = new List<int>();

        //Variable for user to specify whether they want to step through all the nodes
        bool stepThrough;

        //Lists to store the series names, the edges created, and the nodes that need to be traversed
        List<string> series;
        List<string> edgesCreated = new List<string>();
        List<int> nodes = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        //Sets up first edge to prepare for cloest edge insertion
        private void Init()
        {
            //Resets the path each time the program run button is pressed
            int counter = 0;
            foreach (var series in chart1.Series)
            {
                if (counter == 0)
                {
                    //Skip
                }
                else
                {
                    series.Points.Clear();
                }
                counter++;
            }

            //Clears the optimal Path and order list for a new run through
            optimalPath.Clear();
            order.Clear();

            //Variables needed to find the shortest path from the starting node
            double distance;
            double compare = -1;
            int closestNode = 0;

            //Finds the shortest path from the starting node
            for (int i = 0; i < InputData.GetLength(0); i++)
            {
                if (i == startingNode)
                {
                    //Skip
                }
                else if (compare == -1)
                {
                    compare = Math.Sqrt(Math.Pow(InputData[i, 1] - InputData[(int)startingNode, 1], 2) + Math.Pow(InputData[i, 2] - InputData[(int)startingNode, 2], 2));
                    closestNode = i;
                }
                else
                {
                    distance = Math.Sqrt(Math.Pow(InputData[i, 1] - InputData[(int)startingNode, 1], 2) + Math.Pow(InputData[i, 2] - InputData[(int)startingNode, 2], 2));
                    if (distance < compare)
                    {
                        closestNode = i;
                        compare = distance;
                    }
                }

            }

            //Adds a edge from the starting node to the 
            chart1.Series[series[0]].Points.AddXY(InputData[(int)startingNode, 1], InputData[(int)startingNode, 2]);
            chart1.Series[series[0]].Points.AddXY(InputData[closestNode, 1], InputData[closestNode, 2]);

            //Add the two nodes to the optimal path list
            optimalPath.Add((int)startingNode + 1);
            optimalPath.Add(closestNode + 1);
            optimalPath.Add((int)startingNode + 1);

            //Add the nodes to the order list
            order.Add((int)startingNode + 1);
            order.Add(closestNode + 1);

            //Remove the visisted nodes from the nodes list
            nodes.RemoveAt(nodes.FindIndex(x => x == (int)startingNode));
            nodes.RemoveAt(nodes.FindIndex(x => x == closestNode));

            //Add the series name to the edges created list to traverse later
            edgesCreated.Add(series[0]);

            //Remove the series created from the available series list
            series.RemoveAt(0);
        }

        //Function to find all closest edges and get the optimal path
        private void closestEdge()
        {
            //Variable to store the distance from edge, compare value, and the closest node
            double distanceFromEdge;
            double compare = -1;
            int closestNode = 0;

            //If there is only one edge
            if (edgesCreated.Count == 1)
            {
                //Travarse each node in nodes to check the distance for each point
                foreach (var n in nodes)
                {
                    //a = x - x1, where x1 is the first point in the edge and x is the target node
                    double a = InputData[n, 1] - chart1.Series[edgesCreated[0]].Points[0].XValue;

                    //b = y - y1, whre y1 is the first point in the edge and y is the target node
                    double b = InputData[n, 2] - chart1.Series[edgesCreated[0]].Points[0].YValues[0];

                    //c = x2 - x1, where x2 is the second point in the edge
                    double c = chart1.Series[edgesCreated[0]].Points[1].XValue - chart1.Series[edgesCreated[0]].Points[0].XValue;

                    //d = y2 - y1, where y2 is the second point in the edge
                    double d = chart1.Series[edgesCreated[0]].Points[1].YValues[0] - chart1.Series[edgesCreated[0]].Points[0].YValues[0];

                    //Calculate the dot product
                    double dotProduct = a * c + b * d;

                    //Get the length of the line segment between the two points in the edge
                    double edgeLength = c * c + d * d;
                    
                    //Point on the line where the node is closest to
                    double scalar = dotProduct / edgeLength;

                    //x and y value that the node is closest to on the entire line
                    double xx;
                    double yy;

                    //If the target node is closest to the first point on the edge outside the line segment
                    if (scalar < 0)
                    {
                        //Set the x and y coordinates for the point on the line
                        xx = chart1.Series[edgesCreated[0]].Points[0].XValue;
                        yy = chart1.Series[edgesCreated[0]].Points[0].YValues[0];
                    }
                    //If the target node is closest to the second point on the edge outside the line segment
                    else if (scalar > 1)
                    {
                        //Set the x and y coordinates for the point on the line
                        xx = chart1.Series[edgesCreated[0]].Points[1].XValue;
                        yy = chart1.Series[edgesCreated[0]].Points[1].YValues[0];
                    }
                    //If the target node is closest to some point within the line segment
                    else
                    {
                        //Set the x and y coordinates for the point on the line
                        xx = chart1.Series[edgesCreated[0]].Points[0].XValue + scalar * c;
                        yy = chart1.Series[edgesCreated[0]].Points[0].YValues[0] + scalar * d;
                    }

                    //Vector from the target node to the point on the line
                    double dx = InputData[n, 1] - xx;
                    double dy = InputData[n, 2] - yy;

                    if (compare == -1)
                    {
                        //Distance from the vector to the target node
                        compare = Math.Sqrt(dx * dx + dy * dy);
                        closestNode = n;
                    }
                    else
                    {
                        //Distance from the vector to the target node
                        distanceFromEdge = Math.Sqrt(dx * dx + dy * dy);

                        //Compares the values to see if one the new distance is smaller
                        if (distanceFromEdge < compare)
                        {
                            closestNode = n;
                            compare = distanceFromEdge;
                        }
                    }
                }

                //First point on the edge
                double x1 = chart1.Series[edgesCreated[0]].Points[0].XValue;
                double y1 = chart1.Series[edgesCreated[0]].Points[0].YValues[0];

                //Second point on the edge
                double x2 = chart1.Series[edgesCreated[0]].Points[1].XValue;
                double y2 = chart1.Series[edgesCreated[0]].Points[1].YValues[0];

                //Create a new line from the first point to the target node
                chart1.Series[series[0]].Points.AddXY(x1, y1);
                chart1.Series[series[0]].Points.AddXY(InputData[closestNode, 1], InputData[closestNode, 2]);

                //Create a new line from the second point to the target node
                chart1.Series[series[1]].Points.AddXY(x2, y2);
                chart1.Series[series[1]].Points.AddXY(InputData[closestNode, 1], InputData[closestNode, 2]);

                //Variables to store point one and point two node values
                double nodeOne = 0;
                double nodeTwo = 0;

                for(int i = 0; i < InputData.GetLength(0); i++)
                {
                    if( InputData[i, 1] == x1)
                    {
                        nodeOne = i + 1;
                    }
                    if (InputData[i, 1] == x2)
                    {
                        nodeTwo = i + 1;
                    }
                }
                
                //Removes first element in optimal path
                optimalPath.RemoveAt(0);
                
                //Gets the index of each node in optimal path list
                int nodeOneIndex = optimalPath.FindIndex(x => x == nodeOne);
                int nodeTwoIndex = optimalPath.FindIndex(x => x == nodeTwo);

                //Inserts the closest node between the two nodes
                optimalPath.Insert(nodeOneIndex, closestNode + 1);

                //Add the closest node to the order list
                order.Add(closestNode + 1);

                //Reinserts the starting node at the beginning of the list
                optimalPath.Insert(0, (int)startingNode + 1);

                //Removes the closest node from the nodes list
                nodes.RemoveAt(nodes.FindIndex(x => x==closestNode));

                //Adds the created series names to the edges created list to traverse later
                edgesCreated.Add(series[1]);
                edgesCreated.Add(series[0]);

                //Removes the two new created edges from the available series list
                series.RemoveAt(0);
                series.RemoveAt(0);
                
                //If user wants to step through nodes
                if(stepThrough)
                {
                    //Print the path
                    textBox1.Text = String.Join("->", optimalPath);
                    textBox3.Text = String.Join(" ", order);
                    MessageBox.Show("Click OK to traverse to next node.");
                }
            }

            //When there is more than one edge, traverse all edges
            while(series.Count != 1)
            {
                //Variables to store the closest edge name and a compare variable to find the shortest distance
                string closestEdge = "";
                compare = -1;

                //Traverse each edge created and find the distance from each node to the edge
                foreach(var e in edgesCreated)
                {
                    foreach(var n in nodes)
                    {
                        //a = x - x1, where x1 is the first point in the edge and x is the target node
                        double a = InputData[n, 1] - chart1.Series[e].Points[0].XValue;

                        //b = y - y1, whre y1 is the first point in the edge and y is the target node
                        double b = InputData[n, 2] - chart1.Series[e].Points[0].YValues[0];

                        //c = x2 - x1, where x2 is the second point in the edge
                        double c = chart1.Series[e].Points[1].XValue - chart1.Series[e].Points[0].XValue;

                        //d = y2 - y1, where y2 is the second point in the edge
                        double d = chart1.Series[e].Points[1].YValues[0] - chart1.Series[e].Points[0].YValues[0];

                        //Calculate the dot product
                        double dotProduct = a * c + b * d;

                        //Get the length of the line segment between the two points in the edge
                        double edgeLength = c * c + d * d;

                        //Point on the line where the node is closest to
                        double scalar = dotProduct / edgeLength;

                        //x and y value that the node is closest to on the entire line
                        double x;
                        double y;

                        //If the target node is closest to the first point on the edge outside the line segment
                        if (scalar < 0)
                        {
                            //Set the x and y coordinates for the point on the line
                            x = chart1.Series[e].Points[0].XValue;
                            y = chart1.Series[e].Points[0].YValues[0];
                        }
                        //If the target node is closest to the second point on the edge outside the line segment
                        else if (scalar > 1)
                        {
                            //Set the x and y coordinates for the point on the line
                            x = chart1.Series[e].Points[1].XValue;
                            y = chart1.Series[e].Points[1].YValues[0];
                        }
                        //If the target node is closest to some point within the line segment
                        else
                        {
                            //Set the x and y coordinates for the point on the line
                            x = chart1.Series[e].Points[0].XValue + scalar * c;
                            y = chart1.Series[e].Points[0].YValues[0] + scalar * d;
                        }

                        //Vector from the target node to the point on the line
                        double dx = InputData[n, 1] - x;
                        double dy = InputData[n, 2] - y;

                        if (compare == -1)
                        {
                            //Distance from the vector to the target node
                            compare = Math.Sqrt(dx * dx + dy * dy);
                            distanceFromEdge = compare;
                            closestNode = n;
                            closestEdge = e;
                        }
                        else
                        {
                            //Distance from the vector to the target node
                            distanceFromEdge = Math.Sqrt(dx * dx + dy * dy);

                            if (distanceFromEdge < compare)
                            {
                                closestNode = n;
                                compare = distanceFromEdge;
                                closestEdge = e;
                            }
                        }
                    }
                }

                //First point on the edge
                double x1 = chart1.Series[closestEdge].Points[0].XValue;
                double y1 = chart1.Series[closestEdge].Points[0].YValues[0];

                //Second point on the edge
                double x2 = chart1.Series[closestEdge].Points[1].XValue;
                double y2 = chart1.Series[closestEdge].Points[1].YValues[0];

                //Add edge from the first point to the closest node
                chart1.Series[series[0]].Points.AddXY(x1, y1);
                chart1.Series[series[0]].Points.AddXY(InputData[closestNode, 1], InputData[closestNode, 2]);

                //Add edge from the second point to the closest node
                chart1.Series[series[1]].Points.AddXY(x2, y2);
                chart1.Series[series[1]].Points.AddXY(InputData[closestNode, 1], InputData[closestNode, 2]);

                //Clear the edge since we have added a new node
                chart1.Series[closestEdge].Points.Clear();
                
                //Remove the closest node from the available nodes
                nodes.RemoveAt(nodes.FindIndex(x => x == closestNode));

                //Add the two series created to traverse
                edgesCreated.Add(series[0]);
                edgesCreated.Add(series[1]);

                //Remove the edge that the two point were previously connected to and add it back to available edges
                edgesCreated.RemoveAt(edgesCreated.FindIndex(x => x == closestEdge));
                series.Add(closestEdge);

                //Remove the two new edges from the available series
                series.RemoveAt(0);
                series.RemoveAt(0);

                //Variables to store point one and point two node values
                double nodeOne = 0;
                double nodeTwo = 0;

                for (int i = 0; i < InputData.GetLength(0); i++)
                {
                    if (InputData[i, 1] == x1)
                    {
                        nodeOne = i + 1;
                    }
                    if (InputData[i, 1] == x2)
                    {
                        nodeTwo = i + 1;
                    }
                }

                //Gets the index of each node in optimal path list
                int nodeOneIndex = optimalPath.FindIndex(x => x == nodeOne);
                int nodeTwoIndex = optimalPath.FindIndex(x => x == nodeTwo);

                //If node two is ahead of node one then add at node two index
                if (nodeTwoIndex - nodeOneIndex == 1)
                {
                    optimalPath.Insert(nodeTwoIndex, closestNode + 1);
                }
                //If node one is greated than node two then add at node one index
                else if (nodeOneIndex - nodeTwoIndex == 1)
                {
                    optimalPath.Insert(nodeOneIndex, closestNode + 1);
                }
                //If node one is the starting index, add at node two plus one. This will add it at the end of list before returning back to starting node
                else if (nodeOneIndex - nodeTwoIndex < 0)
                {
                    optimalPath.Insert(nodeTwoIndex + 1, closestNode + 1);
                }
                //If node two is the starting index, add at node one plus one. This will add it at the end of list before returning back to starting node
                else if (nodeOneIndex - nodeTwoIndex > 0)
                {
                    optimalPath.Insert(nodeOneIndex + 1, closestNode + 1);
                }

                //Add closest node to order list
                order.Add(closestNode + 1);

                //If user wants to step through nodes
                if (stepThrough)
                {
                    //Print the path
                    textBox1.Text = String.Join("->", optimalPath);
                    textBox3.Text = String.Join(" ", order);
                    MessageBox.Show("Click OK to traverse to next node.");
                }
            }

            //Print the path
            textBox1.Text = String.Join("->", optimalPath);
            textBox3.Text = String.Join(" ", order);
        }

        //Run Program Button
        private void button1_Click(object sender, EventArgs e)
        {
            //Clear the nodes for when the run program button is pressed again with a new starting node
            nodes.Clear();

            //Clear the edges for when the run program button is pressed again with a new starting node
            edgesCreated.Clear();

            //Number of nodes in the input data
            int edges = InputData.GetLength(0);

            //Creates a new series list to store all the available sereis
            series = new List<string>();

            //Creates names for the series and adds them to the series list
            for (int i = 0; i <= edges; i++)
            {
                int temp = i;
                temp++;
                string edgeNumber = "edge";
                edgeNumber += temp;
                series.Add(edgeNumber);
            }

            //Picks a random starting node when the user does not choose one
            if (startingNode == null)
            {
                Random rand = new Random();
                startingNode = rand.Next(1, InputData.GetLength(0));
                comboBox1.SelectedIndex = Convert.ToInt32(startingNode);
            }

            //Adds all the nodes to the available nodes list 
            for (int i = 0; i < InputData.GetLength(0); i++)
            {
                nodes.Add(i);
            }

            //Compare value to disable/enable run program button when user wants to start over with a new value
            compareStarting = (int)startingNode;

            //Initializes graph with the first edge
            Init();

            //Method to find the optimal path by inserting a node at the closest edge
            closestEdge();

            //Get the cost of the optimal path found
            getCost(optimalPath.ToArray(), InputData);

            //Disable run program button
            button1.Enabled = false;

            //Clear the series to recreate the names later
            series.Clear();
        }

        //User specified starting node
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Starting node equals what the user selects in the combo box
            startingNode = Convert.ToInt32(comboBox1.SelectedItem.ToString()) - 1;

            //Disables run program button until user picks a new starting node
            if(compareStarting == (int)startingNode)
            {
                button1.Enabled = false;
            }    
            else
            {
                button1.Enabled = true;
            }
            
        }

        //Open file button
        private void button2_Click(object sender, EventArgs e)
        {
            //Resest the compart staring node value so the button will be enabled when openning a new file
            compareStarting = -1;

            //Get the number of series in chart
            int chartCount = chart1.Series.Count;

            //Remove all of the series except the first since we will be readding new series with new data
            while(chartCount != 1)
            {
                chart1.Series.RemoveAt(chartCount - 1);
                chartCount--;
            }

            //Clear combo box items
            comboBox1.Items.Clear();

            //Clear data points from data file
            chart1.Series[0].Points.Clear();

            //Popup that allows user to choose file
            OpenFileDialog OFD = new OpenFileDialog();

            if(OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = OFD.FileName;
            }

            //Get the data from that file
            InputData = readFile();

            //Add the nodes to the combo box so user can pick a starting node
            for(int i = 0; i < InputData.GetLength(0); i++)
            {
                comboBox1.Items.Add(InputData[i, 0].ToString());
                nodes.Add(i);
            }

            //Add the nodes to the chart
            for (int i = 0; i < InputData.GetLength(0); i++) 
            {
                chart1.Series[0].Points.AddXY(InputData[i, 1], InputData[i, 2]);
                chart1.Series[0].Points[i].Label = Convert.ToString(i + 1);
            }

            //Configure the chart
            chart1.ChartAreas[0].AxisX.Interval = 10;
            chart1.ChartAreas[0].AxisY.Interval = 10;
            chart1.ChartAreas[0].AxisX.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            //Number of edges and series list to store available edges
            int edges = InputData.GetLength(0);
            series = new List<string>();

            //Create the series in the chart
            for(int i = 0; i <= edges; i++)
            {
                int temp = i;
                temp++;
                string edgeNumber = "edge";
                edgeNumber += temp;
                series.Add(edgeNumber);
                chart1.Series.Add(edgeNumber);
                chart1.Series[edgeNumber].IsVisibleInLegend = false;
                chart1.Series[edgeNumber].ChartType = SeriesChartType.Line;
                chart1.Series[edgeNumber].Color = Color.White;
            }
        }

        //Method to get the cost of each permutation
        public void getCost(int[] Nodes, double[,] Data)
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
                nodeOne = Nodes[i - 1] - 1;
                //Index for x_2 and y_2 value
                nodeTwo = Nodes[i] - 1;

                //Add the cost to itself to get the total cost of each edge, since the x and y coordinates will always be in the same position
                //of the Data array. Only the first parameter of Data needs to have a dynamic value
                cost += Math.Sqrt(Math.Pow(Data[nodeTwo, 1] - Data[nodeOne, 1], 2) + Math.Pow(Data[nodeTwo, 2] - Data[nodeOne, 2], 2));
            }

            textBox2.Text = Convert.ToString(cost);
        }

        //Method to read data from a .tsp file
        private double[,] readFile()
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

            button1.Enabled = true;

            //Return the array to access the data in anothe method
            return InputData;
        }

        //Method to check whether user wants to step through code
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                stepThrough = true;
            }
            else
            {
                stepThrough = false;
            }
        }

        //Copy Text buttons
        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox3.Text);
        }
    }
}
