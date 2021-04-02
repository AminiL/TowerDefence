using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Turret.Weapon.Lazer
{
    [CreateAssetMenu(menuName = "Assets/Turret Lazer Weapon Asset", fileName = "Turret Lazer Weapon Asset")]
    class TurretLazerWeaponAsset : TurretWeaponAssetBase
    {
        public float DamagePerSecond;
        public float MaxDistance;

        public LineRenderer LineRendererPrefab;
        
        public override ITurretWeapon GetWeapon(TurretView view)
        {
            return new TurretLazerWeapon(view, this);
        }
    }
}
