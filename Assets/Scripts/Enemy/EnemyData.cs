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

        public readonly EnemyAsset Asset;

        public EnemyData(EnemyAsset asset)
        {
            m_Health = asset.StartHealth;
            m_IsFlying = asset.isFlyingEnemy;
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
            Debug.Log("Die");
            m_IsDead = true;

            //mine code
            Game.Player.EnemyDied(this);
            m_View.Die();
        }
    }
}