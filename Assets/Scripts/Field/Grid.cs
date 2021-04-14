using Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] m_Nodes;

        private int m_Width;
        private int m_Height;

        private Vector3 m_Offset;
        private float m_NodeSize;

        private FlowFieldPathfinding m_Pathfinding;

        private Vector2Int m_StartCoordinate;
        private Vector2Int m_TargetCoordinate;

        private Node m_SelectedNode = null;

        public int Width { get => m_Width; }
        public int Height { get => m_Height; }

        public Grid(int width, int height, Vector3 offset, float nodeSize, Vector2Int start, Vector2Int target)
        {
            m_Width = width;
            m_Height = height;

            m_Offset = offset;
            m_NodeSize = nodeSize;

            m_StartCoordinate = start;
            m_TargetCoordinate = target;

            m_Nodes = new Node[m_Width, m_Height];

            for (int i = 0; i < m_Nodes.GetLength(0); i++)
            {
                for (int j = 0; j < m_Nodes.GetLength(1); j++)
                {
                    m_Nodes[i, j] = new Node(offset + new Vector3(i + 0.5f, 0, j + 0.5f) * nodeSize, new Vector2Int(i, j));
                }
            }

            // todo replace zero
            m_Pathfinding = new FlowFieldPathfinding(this, target, start);

            m_Pathfinding.UpdateField();
        }

        public Node GetNodeAtPoint(Vector3 point)
        {
            if (point.x < m_Offset.x || point.x > m_Offset.x + m_Width * m_NodeSize)
            {
                return null;
            }
            if (point.z < m_Offset.z || point.z > m_Offset.z + m_Height * m_NodeSize)
            {
                return null;
            }
            int x = (int)((point.x - m_Offset.x) / m_NodeSize);
            int y = (int)((point.z - m_Offset.z) / m_NodeSize);
            return GetNode(new Vector2Int(x, y));
        }

        public List<Node> GetNodeInCircle(Vector3 point, float radius)
        {
            List<Node> answerList = new List<Node>();
            for (int i = 0; i < m_Width; ++i)
            {
                for (int j = 0; j < m_Height; ++j)
                {
                    if ((m_Nodes[i, j].Position - point).magnitude < radius + m_NodeSize)
                    {
                        answerList.Add(m_Nodes[i, j]);
                    }
                }
            }
            return answerList;
        }

        public bool CanOccupy(Node nodeToOccupy)
        {
            if (nodeToOccupy.IsOccupied)
            {
                return false;
            }
            return m_Pathfinding.CanOccupy(nodeToOccupy.PositionOnGrid);
        }

        public void TryOccupyNode(Vector2Int coordinate, bool occupy)
        {
            Node node = GetNode(coordinate);
            if (occupy == node.IsOccupied || (occupy && !m_Pathfinding.CanOccupy(coordinate)))
            {
                return;
            }
            node.IsOccupied = occupy;
            UpdatePathfinding();
        }

        public Node GetStartNode()
        {
            return GetNode(m_StartCoordinate);
        }

        public Node GetTargetNode()
        {
            return GetNode(m_TargetCoordinate);
        }

        public void SelectCoordinate(Vector2Int coordinate)
        {
            m_SelectedNode = GetNode(coordinate);
        }

        public void UnselectNode()
        {
            m_SelectedNode = null;
        }

        public bool HasSelectedNode()
        {
            return m_SelectedNode != null;
        }

        public Node GetSelectedNode()
        {
            return m_SelectedNode;
        }

        public Node GetNode(Vector2Int coordinate)
        {
            return GetNode(coordinate.x, coordinate.y);
        }

        public Node GetNode(int i, int j)
        {
            if (i < 0 || i >= m_Width)
            {
                return null;
            }

            if (j < 0 || j >= m_Height)
            {
                return null;
            }

            return m_Nodes[i, j];
        }

        public IEnumerable<Node> EnumerateAllNodes()
        {
            for (int i = 0; i < m_Width; i++)
            {
                for (int j = 0; j < m_Height; j++)
                {
                    yield return GetNode(i, j);
                }
            }
        }

        public void UpdatePathfinding()
        {
            m_Pathfinding.UpdateField();
        }

        public void ClearDeadEnemies(IReadOnlyList<EnemyData> deadEnemies)
        {
            for (int i = 0; i < m_Width; ++i)
            {
                for (int j = 0; j < m_Height; ++j)
                {
                    foreach (EnemyData data in deadEnemies)
                    {
                        m_Nodes[i, j].EnemiesOnCell.Remove(data);
                    }
                }
            }
        }
    }
}
