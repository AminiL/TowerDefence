using Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Turret.Weapon.Projectile.Bullet
{
    public class BulletProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField]
        private float m_Speed;
        [SerializeField]
        private float m_Damage;

        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;

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

        public bool DidHit()
        {
            return m_DidHit;
        }

        public void TickApproaching()
        {
            transform.Translate(transform.forward * (m_Speed * Time.deltaTime), Space.World);
        }

        public void DestroyProjectile()
        {
            if (m_HitEnemy != null)
            {
                m_HitEnemy.GetDamage(m_Damage);

            }
            Destroy(gameObject);
        }
    }
}
