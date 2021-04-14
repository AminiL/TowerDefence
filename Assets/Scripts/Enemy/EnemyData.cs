using Assets;
using Runtime;
using UnityEngine;

namespace Enemy
{
    public class EnemyData
    {
        private EnemyView m_View;
        public EnemyView View => m_View;

        private float m_Health;

        private bool m_IsDead = false;
        public bool IsDead => m_IsDead;

        private bool m_IsFlying;
        public bool IsFlying => m_IsFlying;

        private float m_Speed;
        public float Speed => m_Speed;

        public readonly EnemyAsset Asset;

        public EnemyData(EnemyAsset asset)
        {
            m_Health = asset.StartHealth;
            m_IsFlying = asset.IsFlyingEnemy;
            m_Speed = asset.Speed;
        }

        public void AttachView(EnemyView view)
        {
            m_View = view;
            m_View.AttachData(this);
        }

        public void GetDamage(float damage)
        {
            m_Health -= damage;
            if (m_Health < 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            if (m_IsDead)
            {
                return;
            }
            Debug.Log("Die");
            m_IsDead = true;
            m_View.AnimateDeath();

            Game.Player.EnemyDied(this);
        }
    }
}