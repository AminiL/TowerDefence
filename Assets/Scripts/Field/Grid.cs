using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] m_Nodes;

        private int m_Width;
        private int m_Height;

        private FlowFieldPathfinding m_Pathfinding;

        public int Width { get => m_Width; }
        public int Height { get => m_Height; }

        public Grid(int width, int height, Vector3 offset, float nodeSize, Vector2Int target, Vector2Int start)
        {
            m_Width = width;
            m_Height = height;

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
    }
}
