using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6
{
    class Shuffle
    {

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        //Randomly create solutions for the initial population
        public static List<int> Random(List<int> set)
        {
            for (int i = 0; i < (int)(set.Count / 2); i++)
            {
                int indexA = RandomNumber_v2(set.Count);
                int indexB = RandomNumber_v2(set.Count);

                set = Swap(set, indexA, indexB);
            }

            return set;
        }

        //Swap two nodes in a set
        public static List<int> Swap(List<int> set, int indexA, int indexB)
        {
            int temp = set[indexA];

            set[indexA] = set[indexB];
            set[indexB] = temp;

            return set;
        }

        //Random number between 1 and max
        public static int RandomNumber(int max)
        {
            lock (syncLock)
            {
                return random.Next(1, max);
            }
        }

        //Random number between 0 and max
        public static int RandomNumber_v2(int max)
        {
            lock (syncLock)
            {
                return random.Next(0, max);
            }
        }

        //Random number between min and max
        public static int RandomNumber_v3(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }
    }
}
