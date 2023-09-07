using System;
using static TrailFunctions;
//Utility class for handling pheromone updating
public static class Pheromones
{
    // pheromone decrease factor
    private static double rho = 0.01;
    // pheromone increase factor
    private static double Q = 2.0;
    static double minPheromones = 0.001;
    static double maxPheromones = 100000;

    public static void UpdatePheromones(double[][] pheromones, int[][] ants, int[][] distances)
    {
        for (int i = 0; i < pheromones.Length; i++)
        {
            for (int j = i + 1; j < pheromones[i].Length; j++)
            {
                for (int k = 0; k < ants.Length; k++)
                {
                    double length = TrailFunctions.TrailLength(ants[k], distances);
                    double decrease = (1.0 - rho) * pheromones[i][j];
                    double increase = 0.0;
                    if (AreCitiesAdjacent(i, j, ants[k]))
                        increase = (Q / length);

                    pheromones[i][j] = decrease + increase;

                    if (pheromones[i][j] < minPheromones)
                        pheromones[i][j] = minPheromones;
                    if (pheromones[i][j] > maxPheromones)
                        pheromones[i][j] = maxPheromones;


                    pheromones[j][i] = pheromones[i][j];
                }
            }
        }
    }

    private static bool AreCitiesAdjacent(int city1, int city2, int[] trail)
    {
        // are cities next to each other in the trail 
        int lastPosition = trail.Length - 1;
        int city1Position = FindCityInTrail(trail, city1);// at which position in the trail is the city
        if (city1Position == 0)
        {
            if (trail[1] == city2 || trail[lastPosition] == city2)
                return true;
            else
                return false;
        }
        else if (city1Position == lastPosition)
        {
            if (trail[0] == city2 || trail[lastPosition-1] == city2)
                return true;
            else
                return false;
        }
        else if (trail[city1Position - 1] == city2|| trail[city1Position + 1] == city2)
            return true;
      
        else
            return false;


    }
    private static int FindCityInTrail(int[] trail, int city)
    {
        for (int i = 0; i <= trail.Length - 1; i++)
            if (trail[i] == city)
                return i;
        throw new Exception("City wasn't found in the trail");
    }

}