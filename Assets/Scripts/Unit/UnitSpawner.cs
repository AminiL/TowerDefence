using Field;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unit
{
    class UnitSpawner : MonoBehaviour
    {
        //    [SerializeField]
        //    private GridMovementAgent m_MovementAgent;

        //    [SerializeField]
        //    private GridHolder m_GridHolder;

        //    private void Awake()
        //    {
        //        StartCoroutine(SpawnUnitsCoroutine());
        //    }

        //    private IEnumerator SpawnUnitsCoroutine()
        //    {
        //        while (true)
        //        {
        //            yield return new WaitForSeconds(1f);
        //            SpawnUnit();
        //        }
        //    }

        //    private void SpawnUnit()
        //    {
        //        Node startNode = m_GridHolder.Grid.GetNode(m_GridHolder.StartCoordinate);
        //        Vector3 position = startNode.Position;
        //        GridMovementAgent movementAgent = Instantiate(m_MovementAgent, position, Quaternion.identity);
        //        movementAgent.SetStartNode(startNode);
        //    }
        //}
    }
}
