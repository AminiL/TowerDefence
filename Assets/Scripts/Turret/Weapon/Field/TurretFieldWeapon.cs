using Enemy;
using Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Turret.Weapon.Field
{
    class TurretFieldWeapon : ITurretWeapon
    {
        private TurretView m_View;
        private float m_Damage;
        private float m_MaxDistance;

        public TurretFieldWeapon (TurretView view, TurretFieldWeaponAsset asset)
        {
            m_View = view;
            m_Damage = asset.DamagePerSecond;
            m_MaxDistance = asset.MaxDistance;
        }

        public void TickShoot()
        {
            var nodes =  Game.Player.Grid.GetNodeInCircle(m_View.transform.position, m_MaxDistance);
            foreach (var node in nodes)
            {
                foreach (EnemyData data in node.EnemiesOnCell)
                {
                    if (data != null)
                    {
                        data.GetDamage(m_Damage * Time.deltaTime);
                    }
                }
            }
        }
    }
}
