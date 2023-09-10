using System;
using System.Collections.Generic;
using System.Text;
using static TrailFunctions;
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
    public static int[][] InitializeAnts(int numAnts, int numCities, int start, int[][] distances)
    {
        int[][] ants = new int[numAnts][];
        Console.WriteLine("Prvi nasumični putevi mravi");
        for (int i = 0; i < numAnts; i++)
            ants[i] = RandomTrail(numCities, start, distances);

        return ants;
    }
    static int[] RandomTrail(int numCities, int start, int[][] distances)
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
        int length = TrailFunctions.TrailLength(trail, distances);
        TrailFunctions.PrintTrail(trail, length);

        return trail;
    }



}