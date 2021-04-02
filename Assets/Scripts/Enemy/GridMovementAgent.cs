using Field;
using Runtime;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class GridMovementAgent : IMovementAgent
    {
        private float m_Speed;
        private Transform m_Transform;
        private EnemyData m_EnemyData;

        public GridMovementAgent(float speed, Transform transform, Grid grid, EnemyData enemyData)
        {
            m_Speed = speed;
            m_Transform = transform;
            m_EnemyData = enemyData;
            m_UnderNode = Game.Player.Grid.GetNodeAtPoint(m_Transform.position);

            SetTargetNode(grid.GetStartNode());
        }

        private const float TOLERANCE = 0.1f;

        private Node m_TargetNode;
        private Node m_UnderNode = null;

        public void TickMovement()
        {

            //mine code
            if (m_EnemyData.IsDead)
            {
                return;
            }

            if (m_TargetNode == null)
            {
                return;
            }

            Vector3 target = m_TargetNode.Position;

            Node currentUnderNode = Game.Player.Grid.GetNodeAtPoint(m_Transform.position);
            if (m_UnderNode != currentUnderNode)
            {
                m_UnderNode?.EnemiesOnCell.Remove(m_EnemyData);
                m_UnderNode = currentUnderNode;
                m_UnderNode?.EnemiesOnCell.Add(m_EnemyData);
            }

            float distance = (target - m_Transform.position).magnitude;
            if (distance < TOLERANCE)
            {
                m_TargetNode = m_TargetNode.NextNode;
                return;
            }

            Vector3 dir = (target - m_Transform.position).normalized;
            Vector3 delta = dir * (m_Speed * Time.deltaTime);
            m_Transform.Translate(delta);
        }

        private void SetTargetNode(Node node)
        {
            m_TargetNode = node;
        }
    }
}