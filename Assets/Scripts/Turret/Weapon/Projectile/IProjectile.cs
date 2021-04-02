using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turret.Weapon.Projectile
{
    public interface IProjectile
    {
        void TickApproaching();
        bool DidHit();
        void DestroyProjectile();
    }
}
