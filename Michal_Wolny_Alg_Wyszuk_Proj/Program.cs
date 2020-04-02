using System;
using System.Diagnostics;

/*************************
 * Autor: Michał Wolny 
 * **********************/


namespace Michal_Wolny_Alg_Wyszuk_Proj
{
    class Program
    {
        private static bool SimpleSearch(int[] tab, int el)
        {

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == el)
                {
                    return true;
                }

            }
            return false;

        }

        static int comparison;
        private static bool SimpleSearchInstrum(int[] tab, int el)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                comparison++;
                if (tab[i] == el)
                {
                    return true;
                }

            }
            return false;

        }

        private static int BinarySearch(int[] tab, int element)
        {
            int left = 0;
            int right = tab.Length - 1;
            int mid;
            while (left <= right)
            {
                mid = (right + left) / 2;
                if (tab[mid] == element) return mid;
                else if (tab[mid] < element) left = mid + 1;
                else right = mid - 1;
            }

            return -1;
        }


        static int comparisonBin;
        private static int BinarySearchInstrum(int[] tab, int element)
        {
            int left = 0;
            int right = (tab.Length) - 1;
            int mid;
            while (left <= right)
            {
                mid = (right + left) / 2;
                comparisonBin++;
                if (tab[mid] == element)
                {
                    return mid;
                }
                else
                {
                    comparisonBin++;
                    if (tab[mid] < element)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }

                }

            }

            return -1;
        }

        public static int[] RandomizeTable(int size, int maxVal)
        {
            Random r = new Random();
            int[] r_tab = new int[size];
            for (int i = 0; i < r_tab.Length; i++)
            {
                r_tab[i] = r.Next(1, maxVal);
            }
            return r_tab;
        }

        public static int[] RandomizeTableAvg(int size, int maxVal)
        {
            Random r = new Random();
            int[] r_tab = new int[size];
            for (int i = 0; i < r_tab.Length; i++)
            {
                r_tab[i] = i;
            }
            return r_tab;
        }


        static void Main(string[] args)
        {
            Stopwatch stoper = new Stopwatch();

            long res = 0;
            long avgTime = 0;
            int counter = 0;
            int counter2 = 0;

            // Wyszukiwanie liniowe przypadek pesymistyczny

            Console.WriteLine("WYSZUKIWANIE LINIOWE PRZYPADEK PESYMISTYCZNY");
            for (int i = 2000000; i < 265000000; i += 7000000)
            {
                int[] tab = new int[i];
                tab = Program.RandomizeTable(tab.Length, 50);
                stoper.Reset();
                stoper.Start();
                SimpleSearch(tab, 51);
                stoper.Stop();
                res = stoper.ElapsedTicks;
                Console.WriteLine("{0},time,{1}", i, res);
            }
            Console.WriteLine("Koniec wyszukiwania liniowego w przypadku pesymistycznym, ostateczna ilosc tickow procesorow {0} ", res);


            // Wyszukiwanie binarne przypadek pesymistyczny

            Console.WriteLine("WYSZUKIWANIE BINARNE PRZYPADEK PESYMISTYCZNY");
            for (int i = 2000000; i < 265000000; i += 7000000)
            {
                int[] tab = new int[i];
                tab = Program.RandomizeTable(tab.Length, 50);
                Array.Sort(tab);
                stoper.Reset();
                stoper.Start();
                BinarySearch(tab, 51);
                stoper.Stop();
                res = stoper.ElapsedTicks;
                Console.WriteLine("{0},time,{1}", i, res);
            }

            Console.WriteLine("Koniec wyszukiwania binarnego w przypadku pesymistycznym, ostateczna ilosc tickow procesorow {0} ", res);


            // Wyszukiwanie liniowe przypadek średni

            Console.WriteLine("WYSZUKIWANIE LINIOWE PRZYPADEK ŚREDNI");
            for (int i = 2000000; i < 265000000; i += 7000000)
            {
                int[] tab = new int[i];
                tab = Program.RandomizeTableAvg(i, i);
                int mid = tab[tab.Length / 2]; // element srodkowy
                stoper.Reset();
                stoper.Start();
                SimpleSearch(tab, mid);
                stoper.Stop();
                res = stoper.ElapsedTicks;
                Console.WriteLine("{0},time,{1}", i, res);
            }
            Console.WriteLine("Koniec wyszukiwania liniowego w przypadku srednim, ostateczna ilosc tickow procesorow {0} ", res);

            // Wyszukiwanie binarne przypadek średni

            Console.WriteLine("WYSZUKIWANIE BINARNE PRZYPADEK ŚREDNI");
            for (int i = 2000000; i < (int)Math.Pow(2, 28); i += 7000000)
            {
                int[] tab = new int[i];
                tab = Program.RandomizeTable((int)Math.Pow(2, 28), 50);
                Array.Sort(tab);
                stoper.Reset();
                stoper.Start();

                for (int j = 0; j < 1000; j++)
                {
                    BinarySearch(tab, tab[i]);
                }
                stoper.Stop();
                res = stoper.ElapsedTicks;
                avgTime = res;
                Console.WriteLine("{0};time;{1}", i, avgTime / 1000.00);
            }

            Console.WriteLine("Koniec wyszukiwania binarnego w przypadku srednim, ostateczna ilosc tickow procesorow {0} ", res);



            // Wyszukiwanie liniowe (instrumentacja) pesymistyczny

            Console.WriteLine("WYSZUKIWANIE LINIOWE (INSTRUMENTACJA) PRZYPADEK PESYMISTYCZNY");
            for (int i = 2000000; i < 265000000; i += 7000000)
            {
                counter++;
                comparison = 0;
                int[] tab = new int[i];
                SimpleSearchInstrum(tab, -1);

                Console.WriteLine("{0};\t{1};\t{2}", counter, i, comparison);


            }

            // wyszukiwanie liniowe (instrumentacja) sredni

            Console.WriteLine("WYSZUKIWANIE LINIOWE (INSTRUMENTACJA) PRZYPADEK ŚREDNI");
            for (int i = 2000000; i < 265000000; i += 7000000)
            {
                counter2++;
                comparison = 0;
                int[] tab = new int[i];
                tab = Program.RandomizeTableAvg(i, i);
                SimpleSearchInstrum(tab, i / 2);

                Console.WriteLine("{0};{1};{2}", counter2, i, comparison);


            }


            // Wyszukiwanie binarne (instrumentacja) pesymistyczne

            Console.WriteLine("WYSZUKIWANIE BINARNE (INSTRUMENTACJA) PRZYPADEK PESYMISTYCZNY");
            for (int i = 2000000; i < 265000000; i += 7000000)
            {
                comparisonBin = 0;
                int[] tab = new int[i];
                tab = Program.RandomizeTable(tab.Length, 50);
                Array.Sort(tab);
                BinarySearchInstrum(tab, 51);
                Console.WriteLine("{0},count,{1}", i, comparisonBin);
            }



            // Wyszukiwanie binarne (instrumentacja) średni

            Console.WriteLine("WYSZUKIWANIE BINARNE (INSTRUMENTACJA) PRZYPADEK ŚREDNI");
            int counterAvg = 0;
            double comparisonSum = 0;
            for (int i = 2000000; i < (int)Math.Pow(2, 28); i += 7000000)
            {
                counterAvg = 0;
                comparisonSum = 0;

                int[] tab = new int[i];
                tab = Program.RandomizeTable((int)Math.Pow(2, 28), 50);
                Array.Sort(tab);

                for (int j = 0; j < 10000; j++)
                {
                    BinarySearchInstrum(tab, tab[i]);
                    comparisonSum += comparisonBin;
                    counterAvg++;
                }
                Console.WriteLine("{0};{1};{2}", comparisonBin, i, comparisonSum / counterAvg);

            }

            Console.WriteLine("KONIEC EKSPERYMENTÓW!!");
            Console.ReadKey();
        }
    }
}
