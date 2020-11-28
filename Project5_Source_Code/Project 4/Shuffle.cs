using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4
{
    class Shuffle
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        //Randomly create solutions for the initial population
        public static List<int> Random(List<int> Cities)
        {
            for(int i = 0; i < (int)(Cities.Count / 2); i++)
            {
                int indexA = RandomNumber(Cities.Count);
                int indexB = RandomNumber(Cities.Count);

                Cities = Swap(Cities, indexA, indexB);
            }

            Cities.Add(Cities[0]);

            return Cities;
        }

        //Swap two cities in a solution
        public static List<int> Swap(List<int> Cities, int indexA, int indexB)
        {
            int temp = Cities[indexA];
            
            Cities[indexA] = Cities[indexB];
            Cities[indexB] = temp;

            return Cities;
        }

        //Random number between 0 and max
        public static int RandomNumber(int max)
        {
            lock (syncLock)
            { 
                return random.Next(0, max);
            }
        }

        //Random number between 1 and max
        public static int RandomNumber_v2(int max)
        {
            lock (syncLock)
            {
                return random.Next(1, max);
            }
        }
    }
}
