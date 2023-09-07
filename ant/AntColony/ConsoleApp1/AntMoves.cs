using System;
using System.Collections.Generic;
using System.Text;
using AntColony;
//Utility class for updating ants trails

public static class AntMoves
{
    static Random rand = new Random();
    static double alpha = 3; //influence on pheromone levels
    static double beta = 2;//influence of favorability of distance
    public static void UpdateAnts(int[][] ants, double[][] pheromones, int[][] dists)
    {
        int numCities = pheromones.Length;
        for (int i = 0; i < ants.Length; i++)
        {
            int[] newTrail = NewTrail(pheromones, dists);
            ants[i] = newTrail;
        }
    }
    public static int[] NewTrail(double[][] pheromones, int[][] distances)
    {

        int numCities = pheromones.Length;
        int start = rand.Next(0, numCities);
        int[] trail = new int[numCities];
        bool[] visited = new bool[numCities];
        trail[0] = start;
        visited[start] = true;
        for (int i = 0; i < numCities - 1; i++)
        {
            int currentCity = trail[i];
            int nextCity = NextCity(currentCity, visited, pheromones, distances);
            trail[i + 1] = nextCity;
            visited[nextCity] = true;
        }
        return trail;
    }
    static int NextCity(int currentCity, bool[] visited, double[][] pheromones, int[][] distances)
    {
        double[] probs = CalculateProbabilities(currentCity, visited, pheromones, distances);

        double[] cumulativeProbs = new double[probs.Length + 1];
        cumulativeProbs[0] = 0;
        for (int i = 0; i < probs.Length; ++i)
            cumulativeProbs[i + 1] = cumulativeProbs[i] + probs[i];

        double p = rand.NextDouble();// returns a number in range[0,1>
        for (int i = 0; i < cumulativeProbs.Length - 1; ++i)
            if (p >= cumulativeProbs[i] && p < cumulativeProbs[i + 1])
                return i;
        throw new Exception("Greška u funkciji NextCity");
    }
    static double[] CalculateProbabilities(int currentCity, bool[] visited, double[][] pheromones, int[][] distances)
    {
        int numCities = pheromones.Length;
        double[] probs = new double[numCities];
        double pherProbs;
        double distProbs;
        double sum = 0.0;
        for (int i = 0; i < numCities; ++i)
        {
            if (i == currentCity)
                probs[i] = 0.0; // probability of moving to itself is 0
            else if (visited[i] == true)
                probs[i] = 0.0; //cant move to a visited node
            else
            {
                pherProbs = Math.Pow(pheromones[currentCity][i], alpha);
                distProbs = Math.Pow((1.0 / distances[currentCity][i]), beta);
                probs[i] = pherProbs * distProbs;
                if (probs[i] < 0.0001)
                {
                    probs[i] = 0.0001;
                }
                else if (probs[i] > (double.MaxValue / (numCities * 100)))
                {
                    probs[i] = double.MaxValue / (numCities * 100);
                }

            }
            sum += probs[i];
        }


        for (int i = 0; i < probs.Length; ++i)
            probs[i] = probs[i] / sum;// to get a "percentage"
        return probs;
    }
}
