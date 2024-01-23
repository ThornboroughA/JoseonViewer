using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the manager class that instantiates the map objects based on their data. 
/// </summary>


public class MapManager : MonoBehaviour
{
    public List<SettlementData> settlements; 
    public GameObject settlementPrefab;


    // Start is called before the first frame update
    void Start()
    {
        InstanceSettlements();
    }


    private void InstanceSettlements()
    {

        foreach (SettlementData data in settlements)
        {
            GameObject obj = Instantiate(settlementPrefab);
            SettlementInstance objDat = obj.GetComponent<SettlementInstance>();
            objDat.Initialize(data);
            
        }

    }

}
