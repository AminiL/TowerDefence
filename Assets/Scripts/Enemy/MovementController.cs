using Runtime;
using System.Collections.Generic;

namespace Enemy
{
    public class MovementController : IController
    {
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            List<EnemyData> enemyToDelete = new List<EnemyData>();

            foreach (EnemyData data in Game.Player.EnemyDatas)
            {
                data.View.MovementAgent.TickMovement();
            }
        }
    }
}