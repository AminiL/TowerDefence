using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Turret.Weapon.Field
{
    [CreateAssetMenu(menuName = "Assets/Turret Field Weapon Asset", fileName = "Turret Field Weapon Asset")]
    class TurretFieldWeaponAsset : TurretWeaponAssetBase
    {
        public float DamagePerSecond;
        public float MaxDistance;

        public override ITurretWeapon GetWeapon(TurretView view)
        {
            return new TurretFieldWeapon(view, this);
        }
    }
}
