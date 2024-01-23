using System;
using UnityEngine;

public static class DistributePointsInCluster
{
    private static System.Random random = new System.Random();

    private static Vector3 GenerateRandomPoint(Vector3 centralPoint, float radius)
    {
        float angle = (float)random.NextDouble() * 2 * Mathf.PI;
        float distance = Mathf.Sqrt((float)random.NextDouble()) * radius;
        return new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle)) + centralPoint;
    }

    public static Vector3[] GenerateClustersAroundPoint(int totalPoints, Vector3 centralPoint, float clusterRadius, float isolatedPointRadius, int pointsPerCluster, float clusterSpreadRadius)
    {

        int numberOfClusters = totalPoints / pointsPerCluster;
        clusterRadius = (clusterRadius * (totalPoints * 0.01f + 1));
        Debug.Log(clusterRadius);

        Vector3[] clusterCenters = new Vector3[numberOfClusters];
        // Generate cluster centers around the main central point
        for (int i = 0; i < numberOfClusters; i++)
        {
            clusterCenters[i] = GenerateRandomPoint(centralPoint, clusterSpreadRadius);
        }

        // Generate points for each cluster
        Vector3[] points = new Vector3[numberOfClusters * pointsPerCluster];
        for (int i = 0; i < numberOfClusters; i++)
        {
            for (int j = 0; j < pointsPerCluster; j++)
            {
                int pointIndex = i * pointsPerCluster + j;
                // Occasionally generate an isolated point
                if (random.NextDouble() > 0.9) // 10% chance
                {
                    points[pointIndex] = GenerateRandomPoint(clusterCenters[i], isolatedPointRadius);
                }
                else
                {
                    points[pointIndex] = GenerateRandomPoint(clusterCenters[i], clusterRadius);
                }
            }
        }

        return points;
    }
}
