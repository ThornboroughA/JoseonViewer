using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map { 
    [CreateAssetMenu(fileName = "Visuals List", menuName = "Map/VisualsList")]
    public class VisualsList : ScriptableObject
    {

        public GameObject[] visuals;
        public GameObject[] visuals2;

        [Tooltip("Higher values increase probability of visuals2 objects appearing.")]
        [Range(0, 1)]
        [SerializeField] private float probabilityShift = 0.1f;

        public GameObject GetBuilding()
        {
            float visualsType = Random.Range(0, 100) * 0.01f;

            GameObject visual = null;

            if (visualsType <= probabilityShift)
            {
                visual = visuals2[Random.Range(0, visuals2.Length)];
            } else
            {
                visual = visuals[Random.Range(0, visuals.Length)];
            }

            if (visual == null)
            {
                Debug.LogError($"No visual found in {this}. Add before accessing.");
            }

            Debug.Log($"visualsType is {visualsType}, probabilityShift is {probabilityShift}");

            return visual;
        }

    }
}