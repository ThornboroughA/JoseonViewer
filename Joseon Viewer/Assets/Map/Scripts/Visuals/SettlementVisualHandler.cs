using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SettlementInstance))]
public class SettlementVisualHandler: MonoBehaviour
{
    [SerializeField] private VisualsList settlementBuildings;

    [SerializeField] private float clusterRadius = 3;
    [SerializeField] private float clusterSpread = 1;
    [SerializeField] private float isolatedRadius = 2;
    [SerializeField] private int pointsPerCluster = 5;
    


    public void GenerateBuildings(int population)
    {
        // get the number of buildings to be generated, based on the settlement's population.
        int numBuildings = PopulationToDensity(population);

        Vector3[] buildingLocs = new Vector3[numBuildings];

        buildingLocs = DistributePointsInCluster.GenerateClustersAroundPoint(buildingLocs.Length, transform.position, clusterRadius, isolatedRadius, pointsPerCluster, clusterSpread);
        foreach (Vector3 point in buildingLocs) { Debug.Log($"Point location is {point}"); };



        foreach (Vector3 location in buildingLocs) 
        {
            // add randomization functionality within settlementBuildings
            GameObject building = Instantiate(settlementBuildings.visuals[0]);
            building.transform.position = location;
            building.transform.parent = gameObject.transform;
        }
    }
    private int PopulationToDensity(int population)
    {
        if (population <= 0)
        {
            Debug.Log($"Population of {population} is invalid. Defaulting to 1.");
            population = 1;
        }

        int density = Mathf.RoundToInt( population * 0.1f);
        return density;
    }

}
