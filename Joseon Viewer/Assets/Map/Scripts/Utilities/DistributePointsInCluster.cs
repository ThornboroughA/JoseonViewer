using System;
using UnityEngine;

public static class DistributePointsInCluster
{
    private static System.Random random = new System.Random();

    private static Vector3 GenerateRandomPoint(Vector3 centralPoint, float radius)
    {
        float angle = (float)random.NextDouble() * 2 * Mathf.PI;
        float distance = Mathf.Sqrt((float)random.NextDouble()) * radius;
        Vector3 toReturn = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle)) + centralPoint;

        return toReturn;
        
    }

    public static Vector3[] GenerateClustersAroundPoint(int totalPoints, Vector3 centralPoint, float baseClusterRadius, float baseIsolatedPointRadius, int pointsPerCluster, float clusterSpreadRadius)
    {
        int numClusters = totalPoints / pointsPerCluster;
        int remainderPoints = totalPoints % pointsPerCluster; // Points that don't fit in a full cluster

        Vector3[] clusterCenters = new Vector3[numClusters];
        // Generate cluster centers around the main central point
        for (int i = 0; i < numClusters; i++)
        {
            clusterCenters[i] = GenerateRandomPoint(centralPoint, clusterSpreadRadius);
        }

        // Generate points for each cluster
        Vector3[] points = new Vector3[totalPoints];
        for (int i = 0; i < numClusters; i++)
        {
            // Dynamic adjustment of the radii based on points per cluster
            float dynamicClusterRadius = baseClusterRadius * Mathf.Sqrt(pointsPerCluster);
            float dynamicIsolatedPointRadius = baseIsolatedPointRadius * Mathf.Sqrt(pointsPerCluster);

            for (int j = 0; j < pointsPerCluster; j++)
            {
                int pointIndex = i * pointsPerCluster + j;
                // Occasionally generate an isolated point
                if (random.NextDouble() > 0.9) // 10% chance
                {
                    points[pointIndex] = GenerateRandomPoint(clusterCenters[i], dynamicIsolatedPointRadius);
                }
                else
                {
                    points[pointIndex] = GenerateRandomPoint(clusterCenters[i], dynamicClusterRadius);
                }
            }
        }

        // Handle remainder points, distributing them around the central point
        for (int i = 0; i < remainderPoints; i++)
        {
            int pointIndex = numClusters * pointsPerCluster + i;
            float dynamicClusterRadius = baseClusterRadius * Mathf.Sqrt(pointsPerCluster);
            float dynamicIsolatedPointRadius = baseIsolatedPointRadius * Mathf.Sqrt(pointsPerCluster);

            // Optionally, you could randomly decide to place these remainder points in a cluster or isolated
            if (random.NextDouble() > 0.9) // 10% chance
            {
                points[pointIndex] = GenerateRandomPoint(centralPoint, dynamicIsolatedPointRadius); // Or pick a random cluster center
            }
            else
            {
                points[pointIndex] = GenerateRandomPoint(centralPoint, dynamicClusterRadius); // Or pick a random cluster center
            }
        }

        return points;
    }


}
