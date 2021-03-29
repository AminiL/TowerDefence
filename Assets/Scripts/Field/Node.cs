using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public enum OccupationAvailability
    {
        CanOccupy,
        CanNotOccupy,
        Undefined
    }

    public class Node
    {
        public Vector2Int             PositionOnGrid;
        public Vector3                Position;
        public Node                   NextNode;
        public bool                   IsOccupied;
        public float                  PathWeight;
        public OccupationAvailability OccupationAvailability;

        public Node(Vector3 position, Vector2Int posOnGrid)
        {
            PositionOnGrid = posOnGrid;
            Position = position;
        }

        public void ResetWeight()
        {
            PathWeight = float.MaxValue;
        }
    }
}
