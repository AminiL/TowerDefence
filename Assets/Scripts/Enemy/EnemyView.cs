using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;

        public EnemyData Data => m_Data;
        public IMovementAgent MovementAgent => m_MovementAgent;

        public Animator m_DeathAnimator;
        private static readonly int DieAnimationIndex = Animator.StringToHash("Die");

        public void AttachData(EnemyData data)
        {
            m_Data = data;
        }

        public void CreateMovementAgent(Grid grid)
        {
            if (m_Data.IsFlying)
            {
                Vector3 finalPos = grid.GetTargetNode().Position;
                finalPos.y = transform.position.y;
                m_MovementAgent = new FlyingMovementAgent(transform, finalPos, m_Data);
            }
            else
            {
                m_MovementAgent = new GridMovementAgent(transform, grid, m_Data);
            }
        }

        public void AnimateDeath()
        {
            if (m_DeathAnimator)
            {
                m_DeathAnimator.SetTrigger(DieAnimationIndex);
            }
        }

        //mine code
        public void Die()
        {
            //Destroy(gameObject);
        }
    }
}