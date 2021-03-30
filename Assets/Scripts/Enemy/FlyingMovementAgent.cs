using Field;
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

        public FlyingMovementAgent(float speed, Transform transform, Vector3 targetPos)
        {
            m_StartY = transform.position.y;
            m_TargetPos = targetPos;
            m_Transform = transform;
            m_Speed = speed;
        }

        private const float TOLERANCE = 0.1f;

        public void TickMovement()
        {
            if (m_IsFinished)
            {
                return;
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
