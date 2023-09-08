using System;
using System.Collections.Generic;
using System.Text;
//Utility class for initalizing ants and pheromones
public static class Init
{
    static Random rand = new Random();
    public static double[][] InitializePheromones(int numCities)
    {
        double[][] phers = new double[numCities][];
        for (int i = 0; i < numCities; i++)
            phers[i] = new double[numCities];
        for (int i = 0; i < numCities; i++)
            for (int j = 0; j < numCities; j++)
                phers[i][j] = 0.01; //set all values to arbitrary value 0.01
        return phers;
    }
    public static int[][] InitializeAnts(int numAnts, int numCities, int start)
    {
        int[][] ants = new int[numAnts][];
        Console.WriteLine("Prvi nasumični putevi mravi");
        for (int i = 0; i < numAnts; i++)
            ants[i] = RandomTrail(numCities,start);

        return ants;
    }
    static int[] RandomTrail(int numCities, int start)
    {
        
        int[] trail = new int[numCities];
        trail[0] = start;
        for (int i = 1; i < numCities; i++)
            if (i == start)
                trail[i] = 0;
            else
                trail[i] = i;

        for (int i = 1; i < numCities; i++)
        {
            int r = rand.Next(i, numCities);
            int tmp = trail[r];
            trail[r] = trail[i];
            trail[i] = tmp;
        }

        for (int i = 0; i < numCities; i++)
            Console.Write(trail[i] + " ");
        Console.WriteLine();
        return trail;
    }

}