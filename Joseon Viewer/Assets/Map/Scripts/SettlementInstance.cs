using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  This class handles the settlement data, visuals etc as handled on the map.
/// </summary>

public class SettlementInstance : MonoBehaviour
{
    public SettlementData settlementData;

    public string name;
    public int population;

    public void Initialize(SettlementData data)
    {
        settlementData = data;
        name = settlementData.name;
        population = settlementData.population;
    }



}
