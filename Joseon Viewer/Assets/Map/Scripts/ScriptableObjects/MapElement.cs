using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class handling scriptableobject data for map objects, like settlements.
/// </summary>

public class MapElement : ScriptableObject
{
    public string name;
    public Vector2 location = new Vector2(0,0);


}
