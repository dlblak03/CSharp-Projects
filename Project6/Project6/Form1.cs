using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project6
{
    public partial class Partitioning_Form : Form
    {
        //Variables to store the subsets, differences, generated list, and indices
        List<int>[] S;
        List<List<int>> sOne = new List<List<int>>();
        List<List<int>> sTwo = new List<List<int>>();
        List<int> difference = new List<int>();
        List<int> index = new List<int>();

        //Expert solution
        List<int> expertSubOne = new List<int>();
        List<int> expertSubTwo = new List<int>();

        //Variable to store best solution for each GA
        List<int> bestSubListOne;
        List<int> bestSubListTwo;
        double bestDifference = double.PositiveInfinity;

        int populationSize;
        int lengthOfList;
        int K;

        public Partitioning_Form()
        {
            InitializeComponent();
        }

        //Complete
        private void Initialize_Population()
        {
            //Default values
            lengthOfList = 100;
            int maxNumber = 101;
            populationSize = 100;

            //User input values
            if(!String.IsNullOrEmpty(User_Input_List.Text))
            {
                lengthOfList = Convert.ToInt32(User_Input_List.Text);
            }
            if(!String.IsNullOrEmpty(Max_Number_Input.Text))
            {
                maxNumber = Convert.ToInt32(Max_Number_Input.Text) + 1;
            }
            if(Population_Size_Box.SelectedIndex != 0 && Population_Size_Box.SelectedIndex == 1)
            {
                populationSize = 200;
            }

            //Array to store population
            S = new List<int>[populationSize];

            //Temporary varaibles to store the list
            List<int> tempS = new List<int>();
            List<int> tempSOne = new List<int>();
            List<int> tempSTwo = new List<int>();
            int tempIndex = 0;
            int tempSumOne = 0;
            int tempSumTwo = 0;
            K = 0;
            //Randomly create a list
            index.Add(Shuffle.RandomNumber_v3((int)(lengthOfList * 0.3), (int)(lengthOfList * 0.7)));
            for(int j = 0; j < lengthOfList; j++)
            {
                int toAdd = Shuffle.RandomNumber(maxNumber);
                tempS.Add(toAdd);
                if(tempIndex < index[0])
                {
                    tempSOne.Add(toAdd);
                    tempSumOne += toAdd;
                }
                else
                {
                    tempSTwo.Add(toAdd);
                    tempSumTwo += toAdd;
                }
                K += toAdd;
                tempIndex++;
            }
            //If list is odd, and user wants a perfect partition, perform this operation
            if(checkBox1.Checked)
            {
                while((K % 2) == 1)
                {
                    tempS.RemoveAt(0);
                    tempS.Add(Shuffle.RandomNumber(maxNumber));
                    K = tempS.Sum();
                }
            }
            int tempDifference = Math.Abs(tempSumOne - tempSumTwo);

            S[0] = new List<int>(tempS);
            difference.Add(tempDifference);

            //Create different solutions for the population
            for(int i = 1; i < populationSize; i++)
            {
                List<int> tempList = new List<int>(S[0]);
                List<int> tempListTwo = new List<int>();
                List<int> tempListThree = new List<int>();
                tempSumOne = 0;
                tempSumTwo = 0;
                index.Add(Shuffle.RandomNumber_v3((int)(lengthOfList * 0.3), (int)(lengthOfList * 0.7)));
                S[i] = Shuffle.Random(tempList);
                for (int j = 0; j < lengthOfList; j++)
                {
                    if (j < index[i])
                    {
                        tempListTwo.Add(tempList[j]);
                        tempSumOne += tempList[j];
                    }
                    else
                    {
                        tempListThree.Add(tempList[j]);
                        tempSumTwo += tempList[j];
                    }
                }
                difference.Add(Math.Abs(tempSumOne - tempSumTwo));
            }
            
        }

        private void Next_Generation()
        {
            List<int> nextGeneration;
            List<int>[] nextGenerationFinal = new List<int>[populationSize];
            List<int> offspring;

            //Range for k tournament selection
            int range = (int)(populationSize * 0.30);

            //Create the new population
            for(int i = 0; i < populationSize; i++)
            {
                int sumOfSOne = 0;
                int sumOfSTwo = 0;
                int sumOfSOne_two = 0;
                int sumOfSTwo_two = 0;

                //K tournament selection
                List<int> parentOne = new List<int>(S[Crossover.Tournament_Selection(difference, range)]);
                List<int> parentTwo = new List<int>(S[Crossover.Tournament_Selection(difference, range)]);

                //Create offspring
                offspring = new List<int>(Crossover.PMX(parentOne, parentTwo));

                nextGeneration = offspring;

                List<int> listOne = new List<int>();
                List<int> listTwo = new List<int>();

                List<int> listOneS = new List<int>();
                List<int> listTwoS = new List<int>();

                for (int j = 0; j < lengthOfList; j++)
                {
                    if (j < index[i])
                    {
                        sumOfSOne += offspring[j];
                        sumOfSOne_two += S[i][j];
                        listOne.Add(nextGeneration[j]);
                        listOneS.Add(S[i][j]);
                    }
                    else
                    {
                        sumOfSTwo += offspring[j];
                        sumOfSTwo_two += S[i][j];
                        listTwo.Add(nextGeneration[j]);
                        listTwoS.Add(S[i][j]);
                    }
                }

                //Fitness of the offspring and current solution in the population
                int fitnessOne = Fitness.Calculate(sumOfSOne, sumOfSTwo);
                int fitnessTwo = Fitness.Calculate(sumOfSOne_two, sumOfSTwo_two);

                //If the offspring is better than the one in the population, add it. else keep the original
                if (fitnessOne < fitnessTwo)
                {
                    nextGenerationFinal[i] = nextGeneration;
                    difference[i] = fitnessOne;
                    if (fitnessOne < bestDifference)
                    {
                        bestDifference = fitnessOne;
                        bestSubListOne = new List<int>(listOne);
                        bestSubListTwo = new List<int>(listTwo);
                    }
                }
                else
                {
                    nextGenerationFinal[i] = S[i];
                    difference[i] = fitnessTwo;
                    if (fitnessTwo < bestDifference)
                    {
                        bestDifference = fitnessTwo;
                        bestSubListOne = new List<int>(listOneS);
                        bestSubListTwo = new List<int>(listTwoS);
                    }
                }
            }
            //Return the new generation 
            S = nextGenerationFinal;

        }

        private void Run_Program_Click(object sender, EventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            
            int sum_one = 0;
            int sum_two = 0;
            Run_Program.Enabled = false;

            Initialize_Population();

            List<int> greedyTemp = new List<int>(S[0]);

            Generated_List_Box.Text = String.Join(", ", S[0].ToArray());

            #region Wisdom of Crowds Setup
            //Temporary variables to store the original list
            List<int>[] S_temp = new List<int>[populationSize];
            S_temp = S;
            List<List<int>> sOne_temp = new List<List<int>>(sOne);
            List<List<int>> sTwo_temp = new List<List<int>>(sTwo);
            List<int> difference_temp = new List<int>(difference);
            List<int> index_temp = new List<int>(index);
            #endregion

            #region Genetic Algorithm Solutions

            //Genetic algorithm to get solution one
            #region GA #1
            timer.Start();
            int numOfGenerations = (int)(lengthOfList * 0.40);
            int generation = 0;
            while(generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_One = new List<int>(bestSubListOne);
            List<int> SubTwo_One = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(1, bestDifference);
            label5.Text = timer.Elapsed.ToString();
            #endregion

            //Genetic algorithm to get solution two
            #region GA #2
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Two = new List<int>(bestSubListOne);
            List<int> SubTwo_Two = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(2, bestDifference);
            #endregion

            //Same as above for all GA
            #region GA #3
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Three = new List<int>(bestSubListOne);
            List<int> SubTwo_Three = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(3, bestDifference);
            #endregion

            #region GA #4
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Four = new List<int>(bestSubListOne);
            List<int> SubTwo_Four = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(4, bestDifference);
            #endregion

            #region GA #5
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Five = new List<int>(bestSubListOne);
            List<int> SubTwo_Five = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(5, bestDifference);
            #endregion

            #region GA #6
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Six = new List<int>(bestSubListOne);
            List<int> SubTwo_Six = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(6, bestDifference);
            #endregion

            #region GA #7
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Seven = new List<int>(bestSubListOne);
            List<int> SubTwo_Seven = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(7, bestDifference);
            #endregion

            #region GA #8
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Eight = new List<int>(bestSubListOne);
            List<int> SubTwo_Eight = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(8, bestDifference);
            #endregion

            #region GA #9
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Nine = new List<int>(bestSubListOne);
            List<int> SubTwo_Nine = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(9, bestDifference);
            #endregion

            #region GA #10
            S = S_temp;
            sOne = new List<List<int>>(sOne_temp);
            sTwo = new List<List<int>>(sTwo_temp);
            difference = new List<int>(difference_temp);
            index = new List<int>(index_temp);
            bestDifference = double.PositiveInfinity;
            generation = 0;

            while (generation < numOfGenerations)
            {
                Next_Generation();

                Console.WriteLine(bestDifference);

                generation++;
            }
            List<int> SubOne_Ten = new List<int>(bestSubListOne);
            List<int> SubTwo_Ten = new List<int>(bestSubListTwo);
            PP_Chart.Series[0].Points.AddXY(10, bestDifference);
            #endregion

            #endregion

            #region Wisdom of Crowds

            //Compare the solutions generated by the GAs
            #region Compare Solution One
            //For each for each sublist to comapre every value in every list
            List<int> SubOneCopy = new List<int>(SubOne_One);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if(times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_One.Remove(value);
                    if(flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            List<int> SubTwoCopy = new List<int>(SubTwo_One);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_One.Remove(value);
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Two
            SubOneCopy = new List<int>(SubOne_Two);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Two.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Two);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Two.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Three
            SubOneCopy = new List<int>(SubOne_Three);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Three.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Three);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Three.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Four
            SubOneCopy = new List<int>(SubOne_Four);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Four.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Four);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Four.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Five
            SubOneCopy = new List<int>(SubOne_Five);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Five.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Five);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Five.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Six
            SubOneCopy = new List<int>(SubOne_Six);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Six.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Six);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Six.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Seven
            SubOneCopy = new List<int>(SubOne_Seven);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Seven.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Seven);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Seven.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Eight
            SubOneCopy = new List<int>(SubOne_Eight);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Eight.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Eight);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Eight.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Nine
            SubOneCopy = new List<int>(SubOne_Nine);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Nine.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubOne_Ten.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Nine);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Ten.Contains(value))
                {
                    times++;
                    flagTen = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Nine.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagTen)
                    {
                        SubTwo_Ten.Remove(value);
                    }
                }
            }
            #endregion

            #region Compare Solution Ten
            SubOneCopy = new List<int>(SubOne_Ten);
            foreach (var value in SubOneCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubOne_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubOne_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubOne_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubOne_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubOne_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubOne_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubOne_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubOne_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubOne_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (times > 7)
                {
                    expertSubOne.Add(value);
                    sum_one += value;
                    greedyTemp.Remove(value);
                    SubOne_Ten.Remove(value);
                    if (flagOne)
                    {
                        SubOne_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubOne_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubOne_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubOne_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubOne_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubOne_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubOne_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubOne_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubOne_Nine.Remove(value);
                    }
                }
            }
            SubTwoCopy = new List<int>(SubTwo_Ten);
            foreach (var value in SubTwoCopy)
            {
                bool flagOne = false;
                bool flagTwo = false;
                bool flagThree = false;
                bool flagFour = false;
                bool flagFive = false;
                bool flagSix = false;
                bool flagSeven = false;
                bool flagEight = false;
                bool flagNine = false;
                bool flagTen = false;
                int times = 1;
                if (SubTwo_One.Contains(value))
                {
                    times++;
                    flagOne = true;
                }
                if (SubTwo_Two.Contains(value))
                {
                    times++;
                    flagTwo = true;
                }
                if (SubTwo_Three.Contains(value))
                {
                    times++;
                    flagThree = true;
                }
                if (SubTwo_Four.Contains(value))
                {
                    times++;
                    flagFour = true;
                }
                if (SubTwo_Five.Contains(value))
                {
                    times++;
                    flagFive = true;
                }
                if (SubTwo_Six.Contains(value))
                {
                    times++;
                    flagSix = true;
                }
                if (SubTwo_Seven.Contains(value))
                {
                    times++;
                    flagSeven = true;
                }
                if (SubTwo_Eight.Contains(value))
                {
                    times++;
                    flagEight = true;
                }
                if (SubTwo_Nine.Contains(value))
                {
                    times++;
                    flagNine = true;
                }
                if (times > 7)
                {
                    expertSubTwo.Add(value);
                    sum_two += value;
                    greedyTemp.Remove(value);
                    SubTwo_Ten.Remove(value);
                    if (flagOne)
                    {
                        SubTwo_One.Remove(value);
                    }
                    if (flagTwo)
                    {
                        SubTwo_Two.Remove(value);
                    }
                    if (flagThree)
                    {
                        SubTwo_Three.Remove(value);
                    }
                    if (flagFour)
                    {
                        SubTwo_Four.Remove(value);
                    }
                    if (flagFive)
                    {
                        SubTwo_Five.Remove(value);
                    }
                    if (flagSix)
                    {
                        SubTwo_Six.Remove(value);
                    }
                    if (flagSeven)
                    {
                        SubTwo_Seven.Remove(value);
                    }
                    if (flagEight)
                    {
                        SubTwo_Eight.Remove(value);
                    }
                    if (flagNine)
                    {
                        SubTwo_Nine.Remove(value);
                    }
                }
            }
            #endregion

            #endregion

            #region Greedy Algorithm
            //Sort the remaining values in descending order then add each value to the list with the lowest sum
            greedyTemp = greedyTemp.OrderByDescending(x => x).ToList();

            foreach(var value in greedyTemp)
            {
                if(sum_one < sum_two)
                {
                    sum_one += value;
                    expertSubOne.Add(value);
                }
                else
                {
                    sum_two += value;
                    expertSubTwo.Add(value);
                }
            }

            //Display the data to the GUI
            label6.Text = timer.Elapsed.ToString();
            timer.Stop();

            Sublist_One_Box.Text = String.Join(", ", expertSubOne.ToArray());
            Sublist_Two_Box.Text = String.Join(", ", expertSubTwo.ToArray());
            Sum_Box_One.Text = sum_one.ToString();
            Sum_Box_Two.Text = sum_two.ToString();

            Difference_Box.Text = (Math.Abs(sum_one - sum_two)).ToString();

            for(int i = 1; i < 11; i++)
            {
                PP_Chart.Series[1].Points.AddXY(i, Math.Abs(sum_one - sum_two));
            }
            PP_Chart.Update();

            #endregion
        }
    }
}
