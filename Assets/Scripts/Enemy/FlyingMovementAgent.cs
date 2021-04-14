using Field;
using Runtime;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    class FlyingMovementAgent : IMovementAgent
    {
        private readonly float m_StartY;
        private Transform m_Transform;
        private Vector3 m_TargetPos;
        private float m_Speed;
        private bool m_IsFinished = false;
        private Node m_UnderNode = null;
        private EnemyData m_EnemyData;

        public FlyingMovementAgent(Transform transform, Vector3 targetPos, EnemyData enemyData)
        {
            m_StartY = transform.position.y;
            m_TargetPos = targetPos;
            m_Transform = transform;
            m_Speed = enemyData.Speed;
            m_UnderNode = Game.Player.Grid.GetNodeAtPoint(m_Transform.position);
            m_EnemyData = enemyData;
        }

        private const float TOLERANCE = 0.1f;

        public void TickMovement()
        {
            if (m_IsFinished)
            {
                return;
            }

            Node currentUnderNode = Game.Player.Grid.GetNodeAtPoint(m_Transform.position);
            if (m_UnderNode != currentUnderNode)
            {
                m_UnderNode?.EnemiesOnCell.Remove(m_EnemyData);
                m_UnderNode = currentUnderNode;
                m_UnderNode?.EnemiesOnCell.Add(m_EnemyData);
            }

            float distance = (m_TargetPos - m_Transform.position).magnitude;
            if (distance < TOLERANCE)
            {
                m_IsFinished = true;
                return;
            }

            Vector3 dir = (m_TargetPos - m_Transform.position).normalized;
            Vector3 delta = dir * (m_Speed * Time.deltaTime);
            m_Transform.Translate(delta);
        }
    }
}
