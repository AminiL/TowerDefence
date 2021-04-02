using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using UnityEngine;

namespace Turret.Weapon
{
    public static class EnemySearch
    {
        public static EnemyData GetClosestEnemy(Vector3 center,  List<Node> accesibleNodes)
        {
            float minSqrDistance = float.MaxValue;
            EnemyData closestEnemy = null;
            
            foreach (Node node in accesibleNodes)
            {
                foreach (EnemyData enemyData in node.EnemiesOnCell)
                {
                    //mine code
                    if (enemyData.IsDead)
                    {
                        continue;
                    }

                    float sqrDistance = (enemyData.View.transform.position - center).sqrMagnitude;

                    if (sqrDistance < minSqrDistance)
                    {
                        minSqrDistance = sqrDistance;
                        closestEnemy = enemyData;
                    }
                }

                //mine code
                //node.EnemiesOnCell.RemoveAll(enemyData => enemyData.IsDead);
            }


            return closestEnemy;
        }
    }
}