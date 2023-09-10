using System;
using static Cities;
using static Init;
using static Pheromones;
using static AntMoves;
using static TrailFunctions;
namespace AntColony
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int numCities;
            Console.WriteLine("Unesite broj gradova manji ili jednak 100:");
            var input = Console.ReadLine();
            while (Int32.TryParse(input, out numCities) == false || numCities <= 0 || numCities > 100)
            {
                Console.WriteLine("Unos nije ispravan! Unesite ponovno:");
                input = Console.ReadLine();
            }
            int numAnts = numCities / 12;
            if (numAnts == 0)
                numAnts = 1;
            int numACOIterations = 3 * numCities * (int)Math.Pow(1.5, numAnts);
            Console.WriteLine("Broj mravi: " + numAnts);
            Console.WriteLine("Broj iteracija: " + numACOIterations);
            int[][] distances = Cities.RandomCityDistances(numCities);
            if (numCities <= 25)// because thats the most that'll fit on a full console screen
                Cities.ShowDistanceMatrix(numCities, distances);
            int start;
            Console.Write("Unesite početni grad (broj u intervalu [0," + (numCities - 1) + "]): ");
            input = Console.ReadLine();
            while (Int32.TryParse(input, out start) == false || start >= numCities || start < 0)
            {
                Console.WriteLine("Unos nije ispravan! Unesite ponovno:");
                input = Console.ReadLine();
            }
            int[][] ants = Init.InitializeAnts(numAnts, numCities, start, distances);
            double[][] pheromones = Init.InitializePheromones(numCities);
            int[] bestTrail = TrailFunctions.BestTrail(ants, distances);
            // determine the best initial trail
            int bestLength = TrailFunctions.TrailLength(bestTrail, distances);
            // the length of the best trail
            Console.WriteLine("Najbolji inicijalni put:");
            TrailFunctions.PrintTrail(bestTrail, bestLength);


            for (int i = 0; i < numACOIterations; i++)
            {
                AntMoves.UpdateAnts(ants, pheromones, distances, start);
                Pheromones.UpdatePheromones(pheromones, ants, distances);

                int[] currentBestTrail = TrailFunctions.BestTrail(ants, distances);
                int currentBestLength = TrailFunctions.TrailLength(currentBestTrail, distances);
                if (currentBestLength < bestLength)
                {
                    bestLength = currentBestLength;
                    bestTrail = currentBestTrail;
                    Console.WriteLine("Trenutačni najbolji put pronađen u iteraciji broj " + (i + 1));
                    TrailFunctions.PrintTrail(bestTrail, bestLength);
                }
            }
            Console.WriteLine("Konacni najbolji put");
            TrailFunctions.PrintTrail(bestTrail, bestLength);

        }

    }
}
