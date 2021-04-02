using Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turret.Weapon
{
    class TurretShootController : IController
    {
        public void OnStart()
        {

        }

        public void OnStop()
        {

        }

        public void Tick()
        {
            foreach (TurretData turretData in Game.Player.TurretDatas)
            {
                turretData.Weapon.TickShoot();
            }
        }
    }
}
