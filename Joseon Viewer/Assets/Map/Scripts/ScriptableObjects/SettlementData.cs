using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the scriptable object handling the data for settlement objects on the map, which is then handled by SettlementInstance.
/// </summary>

namespace Map { 
    [CreateAssetMenu(fileName = "New Settlement", menuName = "Map/Settlement")]
    public class SettlementData : MapElement {
        public int population;
 
    }
}