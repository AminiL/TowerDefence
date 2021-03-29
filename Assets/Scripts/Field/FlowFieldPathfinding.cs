using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    struct Connection
    {
        public Vector2Int coordinate;
        public float weight;

        public Connection(Vector2Int coordinate, float weight)
        {
            this.coordinate = coordinate;
            this.weight = weight;
        }
    }

    public class FlowFieldPathfinding : MonoBehaviour
    {
        private Grid m_Grid;
        private Vector2Int m_Target;
        private Vector2Int m_Start;

        public FlowFieldPathfinding(Grid grid, Vector2Int target, Vector2Int start)
        {
            m_Grid = grid;
            m_Target = target;
            m_Start = start;
        }

        public void UpdateField()
        {
            
            foreach (Node node in m_Grid.EnumerateAllNodes())
            {
                node.ResetWeight();
                node.OccupationAvailability = OccupationAvailability.CanOccupy;
            }

            Queue<Connection> queue = new Queue<Connection>();
            queue.Enqueue(new Connection(m_Target, 0f));
            m_Grid.GetNode(m_Target).PathWeight = 0f;

            while (queue.Count > 0)
            {
                Connection current = queue.Dequeue();
                Node currentNode = m_Grid.GetNode(current.coordinate);

                foreach (Connection neighbour in GetNeighbours(current.coordinate))
                {
                    Node neighbourNode = m_Grid.GetNode(neighbour.coordinate);
                    if (current.weight + neighbour.weight < neighbourNode.PathWeight - Mathf.Epsilon)
                    {
                        neighbourNode.NextNode = currentNode;
                        neighbourNode.PathWeight = current.weight + neighbour.weight;
                        queue.Enqueue(new Connection(neighbour.coordinate, neighbourNode.PathWeight));
                    }
                }
            }

            Node curNode = m_Grid.GetNode(m_Start);
            curNode.OccupationAvailability = OccupationAvailability.CanNotOccupy;
            curNode = curNode.NextNode;
            while (curNode.PositionOnGrid != m_Target)
            {
                curNode.OccupationAvailability = OccupationAvailability.Undefined;
                curNode = curNode.NextNode;
            }
            curNode.OccupationAvailability = OccupationAvailability.CanNotOccupy;
        }

        public bool CanOccupy(Vector2Int coordinate)
        {
            if (!IsOnField(coordinate) || coordinate == m_Start)
            {
                return false;
            }

            Node node = m_Grid.GetNode(coordinate);
            switch (node.OccupationAvailability)
            {
                case OccupationAvailability.CanOccupy:
                    return true;
                case OccupationAvailability.CanNotOccupy:
                    return false;
            }
            node.IsOccupied = true;

            bool[,] isVisited = new bool[m_Grid.Width, m_Grid.Height];
            isVisited[m_Start.x, m_Start.y] = true;
            Queue<Vector2Int> q = new Queue<Vector2Int>();
            q.Enqueue(m_Start);

            while (q.Count > 0)
            {
                Vector2Int cur = q.Dequeue();
                if (cur == m_Target)
                {
                    break;
                }
                foreach (Connection neighbour in GetNeighbours(cur))
                {
                    Vector2Int coord = neighbour.coordinate;
                    if (isVisited[coord.x, coord.y])
                    {
                        continue;
                    }
                    isVisited[coord.x, coord.y] = true;
                    q.Enqueue(coord);
                }
            }

            node.IsOccupied = false;
            if (isVisited[m_Target.x, m_Target.y])
            {
                node.OccupationAvailability = OccupationAvailability.CanOccupy;
                return true;
            }
            else
            {
                node.OccupationAvailability = OccupationAvailability.CanNotOccupy;
                return false;
            }
        }

        private IEnumerable<Connection> GetNeighbours(Vector2Int coordinate)
        {
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    Vector2Int neighbour = coordinate;
                    neighbour.x += i;
                    neighbour.y += j;
                    if (IsReachableNeighbour(coordinate, neighbour, out float weight))
                    {
                        yield return new Connection(neighbour, weight);
                    }
                }
            }
        }

        private bool IsReachableNeighbour(Vector2Int current, Vector2Int neighbour, out float weight)
        {
            weight = float.MaxValue;
            if (!IsOnField(neighbour) || m_Grid.GetNode(neighbour).IsOccupied)
            {
                return false;
            }

            Vector2Int sub = neighbour - current;
            int lenghtSquare = sub.x * sub.x + sub.y * sub.y;
            weight = Mathf.Sqrt(lenghtSquare);

            switch (lenghtSquare)
            {
                case 1:
                    return true;
                case 2:
                    if (m_Grid.GetNode(current.x, current.y + sub.y).IsOccupied || m_Grid.GetNode(current.x + sub.x, current.y).IsOccupied)
                    {
                        return false;
                    }
                    return true;
                default:
                    return false; 
            }
        }

        private bool IsOnField(Vector2Int position)
        {
            return position.x >= 0 && position.x < m_Grid.Width && position.y >= 0 && position.y < m_Grid.Height;
        }
    }
}
