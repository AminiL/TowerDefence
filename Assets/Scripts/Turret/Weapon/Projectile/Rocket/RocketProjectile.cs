using Enemy;
using Field;
using Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Turret.Weapon.Projectile.Rocket
{
    class RocketProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField]
        private float m_Speed;
        [SerializeField]
        private float m_Damage;
        [SerializeField]
        private float m_DestructionRadius;

        private bool m_DidHit = false;
        private EnemyData m_ChasingEnemy = null;
        private EnemyData m_HitEnemy = null;
        private Vector3 m_Direction;

        private void OnTriggerEnter(Collider other)
        {
            m_DidHit = true;
            if (other.CompareTag("Enemy"))
            {
                EnemyView enemyView = other.GetComponent<EnemyView>();
                if (enemyView != null)
                {
                    m_HitEnemy = enemyView.Data;
                }
            }
        }

        private void SetDirection()
        {
            m_Direction = (m_ChasingEnemy.View.transform.position - transform.position).normalized;
        }

        public void SetChasingEnemy(EnemyData enemyData)
        {
            m_ChasingEnemy = enemyData;
            SetDirection();
        }

        public void TickApproaching()
        {
            if (m_ChasingEnemy != null && !m_ChasingEnemy.IsDead)
            {
                SetDirection();
            }
            transform.Translate(m_Direction * (m_Speed * Time.deltaTime), Space.World);
        }

        public bool DidHit()
        {
            return m_DidHit;
        }

        public void DestroyProjectile()
        {
            if (!DidHit())
            {
                return;
            }

            List<Node> nodesInCircle = Game.Player.Grid.GetNodeInCircle(transform.position, m_DestructionRadius);
            foreach (Node node in nodesInCircle)
            {
                foreach (EnemyData enemyData in node.EnemiesOnCell)
                {
                    //mine code
                    if (enemyData.IsDead)
                    {
                        continue;
                    }
                    if ((enemyData.View.transform.position - transform.position).sqrMagnitude < m_DestructionRadius * m_DestructionRadius)
                    {
                        enemyData.GetDamage(m_Damage);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
