using System;
using System.Collections.Generic;
using System.Text;
//Utility class for functions concerning trails
public static class TrailFunctions
{
    public static int[] BestTrail(int[][] ants, int[][] distances)
    {
        // best trail has shortest total length
        double bestLength = TrailLength(ants[0], distances);
        //Console.WriteLine(bestLength);
        int antId = 0;//which ant chose the best trail
        for (int i = 1; i < ants.Length; i++)
        {

            double length = TrailLength(ants[i], distances);
            //Console.WriteLine(length);
            if (length < bestLength)
            {
                bestLength = length;
                antId = i;
            }
        }
        int numCities = ants[0].Length;
        int[] bestTrail = new int[numCities];
        ants[antId].CopyTo(bestTrail, 0);
        return bestTrail;
    }
    public static int TrailLength(int[] trail, int[][] distances)
    {
        int city1, city2;
        int totalLength = 0;
        for (int i = 0; i < trail.Length - 1; i++)
        {
            city1 = trail[i];
            city2 = trail[i+1];
            totalLength += distances[city1][city2];//cumulative sum of distances between adjacent cities
        }
        return totalLength;

    }


}
