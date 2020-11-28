using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project_4
{
    class Fitness
    {
        //Calculate the fitness value solution that has been passed into it
        public static double Calculate(List<int> City, double[,] InputData)
        {
            double fitnessValue = 0;
            
            for(int x = 1; x < City.Count; x++) 
            {
                int nodeOne = City[x - 1] - 1;
                int nodeTwo = City[x] - 1;

                fitnessValue += Math.Sqrt(Math.Pow(InputData[nodeTwo, 1] - InputData[nodeOne, 1], 2) + Math.Pow(InputData[nodeTwo, 2] - InputData[nodeOne, 2], 2));
            }

            return fitnessValue;
        }
    }
}
