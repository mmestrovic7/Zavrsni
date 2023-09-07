using System;
using System.Collections.Generic;
using System.Text;
// Utility class for making a distance graph etc
public static class Cities
{
    static Random rand = new Random();
    public static int[][] RandomCityDistances(int numCities)
    {
        int[][] distances = new int[numCities][];
        for (int i = 0; i < numCities; i++)
            distances[i] = new int[numCities];
        if (numCities == 8)
        {
            for (int i = 0; i < numCities; i++)
            {
                for (int j = i + 1; j < numCities; j++)// skip distance of city to itself
                {

                    int d = i + j;
                    distances[i][j] = d;
                    distances[j][i] = d;//distances need to be symetric
                }
                distances[i][i] = 0; //distance of city to itself
            }
        }
        else
        {
            for (int i = 0; i < numCities; i++)
            {
                for (int j = i + 1; j < numCities; j++)// skip distance of city to itself
                {

                    int d = rand.Next(1, 11); // [1,10]
                    distances[i][j] = d;
                    distances[j][i] = d;//distances need to be symetric
                }
                distances[i][i] = 0; //distance of city to itself
            }
        }

        return distances;
    }

    public static void ShowDistanceMatrix(int numCities, int[][] distances)
    {
        Console.WriteLine("TABLICA NASUMIČNIH UDALJENOSTI\n");
        Console.Write("GRADOVI \t");
        for (int i = 0; i < numCities; i++)
        {
            Console.Write(i);
            if (i % 10 != 0 || i == 0)
                Console.Write(" ");
            Console.Write("\t");
        }
        Console.WriteLine();
        for (int i = 0; i < numCities; i++)
        {

            Console.Write(i);
            if (i % 10 != 0 || i == 0)
                Console.Write(" ");
            Console.Write(" \t\t");
            for (int j = 0; j < numCities; j++)
            {

                if (distances[i][j] == 0)
                    Console.Write("\\ ");
                else
                    Console.Write(distances[i][j]);
                if (distances[i][j] % 10 != 0)//if its anything but 10
                    Console.Write(" ");
                Console.Write("\t");
            }
            Console.WriteLine();

        }
    }
}