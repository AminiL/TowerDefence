using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile.Rocket
{
    [CreateAssetMenu(menuName = "Assets/Rocket Projectile Asset", fileName = "Rocket Projectile Asset")]
    class RocketProjectileAsset : ProjectileAssetBase
    {
        [SerializeField]
        private RocketProjectile m_RocketProjectile;

        public override IProjectile CreateProjectile(Vector3 origin, Vector3 originForward, EnemyData enemyData)
        {
            RocketProjectile created = Instantiate(m_RocketProjectile, origin, Quaternion.LookRotation(originForward, Vector3.up));
            created.SetChasingEnemy(enemyData);
            return created;
        }
    }
}
