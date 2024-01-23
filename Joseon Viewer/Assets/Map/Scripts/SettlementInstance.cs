using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  This class handles the settlement data, visuals etc as handled on the map.
/// </summary>

[RequireComponent(typeof(SettlementVisualHandler))]
public class SettlementInstance : MonoBehaviour
{
    public SettlementData settlementData;

    [HideInInspector] public string name;
    [HideInInspector] public int population;


    private void Start()
    {
        Initialize(settlementData);

        InitializeVisuals();
    }

    public void Initialize(SettlementData data)
    {
        if (data == null)
        {
            Debug.LogWarning("SettlementData for " + this.name + " reads as null.");
            return;
        }

        settlementData = data;
        name = settlementData.name;
        population = settlementData.population;

        string nameStart = "Settlement: ";
        gameObject.name = nameStart + name;
    }

    private void InitializeVisuals()
    {
        GetComponent<SettlementVisualHandler>().GenerateBuildings(population);
    }


}
