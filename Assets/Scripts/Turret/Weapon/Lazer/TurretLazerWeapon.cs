using Enemy;
using JetBrains.Annotations;
using Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Turret.Weapon.Lazer
{
    class TurretLazerWeapon : ITurretWeapon
    {
        private TurretView m_View;
        private LineRenderer m_LineRenderer;
        private float m_Damage;
        private float m_MaxDistance;

        [CanBeNull]
        private EnemyData m_ClosestEnemyData = null;

        public TurretLazerWeapon(TurretView view, TurretLazerWeaponAsset asset)
        {
            m_View = view;
            m_Damage = asset.DamagePerSecond;
            m_MaxDistance = asset.MaxDistance;
            m_LineRenderer = UnityEngine.Object.Instantiate(asset.LineRendererPrefab, view.ProjectileOrigin);
            m_LineRenderer.gameObject.SetActive(false);
        }

        public void TickShoot()
        {
            TickTower();
            if (CheckClosest())
            {
                m_LineRenderer.gameObject.SetActive(true);
                m_ClosestEnemyData.GetDamage(m_Damage * Time.deltaTime);
            }
            else
            {
                m_LineRenderer.gameObject.SetActive(false);
            }

            SetClosest();
            TickTower();
        }

        private void TickTower()
        {
            if (CheckClosest())
            {
                m_LineRenderer.SetPosition(1, m_View.ProjectileOrigin.position);
                m_LineRenderer.SetPosition(0, m_ClosestEnemyData.View.transform.position);
                m_View.TowerLookAt(m_ClosestEnemyData.View.transform.position);
            }
        }

        private bool CheckClosest()
        {
            return m_ClosestEnemyData != null && 
                !m_ClosestEnemyData.IsDead &&
                (m_View.transform.position - m_ClosestEnemyData.View.transform.position).sqrMagnitude < m_MaxDistance * m_MaxDistance;
        }

        private bool SetClosest()
        {
            m_ClosestEnemyData = EnemySearch.GetClosestEnemy(m_View.transform.position, Game.Player.Grid.GetNodeInCircle(m_View.transform.position, m_MaxDistance));
            return CheckClosest();
        }
    }
}
