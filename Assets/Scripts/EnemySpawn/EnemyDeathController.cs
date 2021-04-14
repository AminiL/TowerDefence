using Enemy;
using Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemySpawn
{
    class EnemyDeathController : IController
    {
        public void OnStart()
        {

        }

        public void OnStop()
        {

        }

        public void Tick()
        {
            Game.Player.Grid.ClearDeadEnemies(Game.Player.DiedEnemyDatas);
            foreach (EnemyData deadEnemy in Game.Player.DiedEnemyDatas)
            {
                deadEnemy.View.Die();
            }
            Game.Player.ResetDeadEnemy();
        }
    }
}
