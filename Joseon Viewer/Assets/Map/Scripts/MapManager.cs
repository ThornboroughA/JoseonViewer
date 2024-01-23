using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the manager class that instantiates the map objects based on their data. 
/// </summary>


public class MapManager : MonoBehaviour
{
    [Tooltip("A list of all the settlement GameObjects in the scene. Populated on play.")]
    public List<GameObject> settlements; 
    


    // Start is called before the first frame update
    void Awake()
    {
        GetSettlements();
    }


    private void GetSettlements()
    {
        SettlementInstance[] data = FindObjectsOfType<SettlementInstance>();
        foreach (SettlementInstance datum in data)
        {
            settlements.Add(datum.gameObject);
        }

    }

}
