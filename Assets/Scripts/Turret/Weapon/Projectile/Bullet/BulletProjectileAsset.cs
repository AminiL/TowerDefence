using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile.Bullet
{
    [CreateAssetMenu(menuName = "Assets/Bullet Projectile Asset", fileName = "Bullet Projectile Asset")]
    class BulletProjectileAsset : ProjectileAssetBase
    {
        [SerializeField]
        private BulletProjectile m_BulletProjectile;

        [SerializeField]
        private float m_Speed;//10
        [SerializeField]
        private float m_Damage;//5

        public override IProjectile CreateProjectile(Vector3 origin, Vector3 originForward, EnemyData enemyData)
        {
            BulletProjectile created = Instantiate(m_BulletProjectile, origin, Quaternion.LookRotation(originForward, Vector3.up));
            created.Init(m_Speed, m_Damage);
            return created;
        }
    }
}
