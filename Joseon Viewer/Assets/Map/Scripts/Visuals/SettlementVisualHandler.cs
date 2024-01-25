using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map { 

    [RequireComponent(typeof(SettlementInstance))]
    public class SettlementVisualHandler: MonoBehaviour
    {
        [SerializeField] private VisualsList settlementBuildings;

        private float clusterRadius = 0.2f;
        private float clusterSpread = 0f;
        private float isolatedRadius = 5f;
        private int pointsPerCluster = 3;

        public MapDataTypes.SettlementSize size = MapDataTypes.SettlementSize.Hamlet;

        public void SetVisualSize()
        {
            switch(size)
            {
                case MapDataTypes.SettlementSize.Hamlet:
                    clusterRadius = 0.2f;
                    clusterSpread = 0f;
                    isolatedRadius = 5f;
                    pointsPerCluster = 3;
                    break;
                case MapDataTypes.SettlementSize.Village:
                    clusterRadius = 0.5f;
                    clusterSpread = 2f;
                    isolatedRadius = 5f;
                    pointsPerCluster = 3;
                    break;
                case MapDataTypes.SettlementSize.Town:
                    clusterRadius = 0.5f;
                    clusterSpread = 2.5f;
                    isolatedRadius = 3f;
                    pointsPerCluster = 15;
                    break;
                case MapDataTypes.SettlementSize.City:
                    clusterRadius = 0.5f;
                    clusterSpread = 1f;
                    isolatedRadius = 3f;
                    pointsPerCluster = 15;
                    break;
                case MapDataTypes.SettlementSize.Metropolis:
                    clusterRadius = 0.5f;
                    clusterSpread = 1f;
                    isolatedRadius = 3f;
                    pointsPerCluster = 15;
                    break;
            }
        }



        public void GenerateBuildings(int population)
        {
            SetVisualSize();

            int numBuildings = PopulationToBuildingDensity(population);
            Vector3[] buildingLocs = DistributePointsInCluster.GenerateClustersAroundPoint(numBuildings, transform.position, clusterRadius, isolatedRadius, pointsPerCluster, clusterSpread);

            float buildingRadius = 0.2f; // Set this to half the width/depth of your building
            int maxAttempts = 10; // Maximum attempts to find an unoccupied location
            float adjustmentStep = 1.0f; // Distance to move the location on each attempt

            foreach (Vector3 originalLocation in buildingLocs)
            {
                Vector3 location = originalLocation;
                int attempts = 0;

                while (Physics.CheckSphere(location, buildingRadius) && attempts < maxAttempts)
                {
                    float randomDirection = Random.Range(0f, 360f);
                    location += Quaternion.Euler(0, randomDirection, 0) * Vector3.forward * adjustmentStep;
                    attempts++;
                }

                if (attempts < maxAttempts)
                {
                    float randomYRotation = Random.Range(0f, 360f);
                    Quaternion randomRot = Quaternion.Euler(0, randomYRotation, 0);
                    GameObject building = Instantiate(settlementBuildings.GetBuilding(), location, randomRot);
                    SetVerticalHeight(building.transform);
                    building.transform.parent = gameObject.transform;
                    Physics.SyncTransforms();

                    // cleanup
                    BoxCollider objCollider = building.GetComponent<BoxCollider>();
                    if (objCollider != null)
                    {
                        Destroy(objCollider);
                    } else
                    {
                        Debug.LogWarning("Warning: No BoxCollider found on map building prefab.");
                    }
                }
            }
        }
        private int PopulationToBuildingDensity(int population)
        {
            float multiplier = 8.0f; // overall scaling
            float offset = -50.0f; // minimum number of buildings
            float minAdjustment = Random.Range(2, 3); // ensures a floor of 2 at minimum

            if (population <= 0)
            {
                Debug.LogWarning($"Population of {population} is invalid. Defaulting to 1.");
                population = 1;
            }

            int buildingCount = (int)(multiplier * Mathf.Log(population) + offset);

            Debug.LogWarning($"Building count for a population of {population} is {buildingCount}.");
            return Mathf.Max((int)minAdjustment, buildingCount);
        }


        private void SetVerticalHeight(Transform objTrans)
        {
            Debug.Log($"original position is + {objTrans.position}");

            RaycastHit hit;
            if (Physics.Raycast(objTrans.position + Vector3.up * 100, Vector3.down, out hit))
            {
               // Debug.Log($"raycast hit! at {hit}");


                Vector3 position = objTrans.position;

               // Debug.Log(position);
                position.y = hit.point.y;
                objTrans.position = position;
               // Debug.Log(position);
            } else
            {
                Debug.Log($"No ground detected below {objTrans.name}");
            }
        }

    }
}
