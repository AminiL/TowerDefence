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
                //mine code
                if (data.IsDead)
                {
                    //enemyToDelete.Add(data);
                    continue;
                }

                data.View.MovementAgent.TickMovement();
            }

            //mine code
            //foreach (EnemyData enemyData in enemyToDelete)
            //{
            //    Game.Player.EnemyDied(enemyData);
            //}
        }
    }
}