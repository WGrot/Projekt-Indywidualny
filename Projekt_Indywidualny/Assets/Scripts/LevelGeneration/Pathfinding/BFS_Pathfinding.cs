using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS_Pathfinding
{
    LevelGrid levelGrid;

    public BFS_Pathfinding(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    public GridNode FindCorridorPath(int x, int z, int xDes, int zDes)
    {
        Queue<GridNode> queue = new Queue<GridNode>();
        HashSet<GridNode> visitedNodes = new HashSet<GridNode>();
        GridNode startNode = new GridNode(x, z, null);

        queue.Enqueue(startNode);
        visitedNodes.Add(startNode);

        while (queue.Count > 0)
        {
            GridNode currentNode = queue.Dequeue();
            if (levelGrid.GetFieldValue(currentNode.x, currentNode.z + 1) != ConstantValues.ROOM_FIELD_VALUE && levelGrid.GetFieldValue(currentNode.x , currentNode.z + 1) != -1)
            {
                GridNode nextNode = new GridNode(currentNode.x, currentNode.z + 1, currentNode);
                if (nextNode.x == xDes && nextNode.z == zDes)
                {
                    return nextNode;
                }

                if (!visitedNodes.Contains(nextNode)){
                    queue.Enqueue(nextNode);
                    visitedNodes.Add(nextNode);
                }
            }

            if (levelGrid.GetFieldValue(currentNode.x+1 , currentNode.z ) != ConstantValues.ROOM_FIELD_VALUE && levelGrid.GetFieldValue(currentNode.x +1 , currentNode.z ) != -1)
            {
                GridNode nextNode = new GridNode(currentNode.x+1, currentNode.z, currentNode);
                if (nextNode.x == xDes && nextNode.z == zDes)
                {
                    return nextNode;
                }
                if (!visitedNodes.Contains(nextNode))
                {
                    queue.Enqueue(nextNode);
                    visitedNodes.Add(nextNode);
                }

            }

            if (levelGrid.GetFieldValue(currentNode.x , currentNode.z - 1) != ConstantValues.ROOM_FIELD_VALUE && levelGrid.GetFieldValue(currentNode.x , currentNode.z - 1 ) != -1)
            {
                GridNode nextNode = new GridNode(currentNode.x, currentNode.z - 1, currentNode);
                if (nextNode.x == xDes && nextNode.z == zDes)
                {
                    return nextNode;
                }
                if (!visitedNodes.Contains(nextNode))
                {
                    queue.Enqueue(nextNode);
                    visitedNodes.Add(nextNode);
                }

            }

            if (levelGrid.GetFieldValue(currentNode.x - 1 , currentNode.z ) != ConstantValues.ROOM_FIELD_VALUE && levelGrid.GetFieldValue(currentNode.x -1 , currentNode.z) != -1)
            {
                GridNode nextNode = new GridNode(currentNode.x-1, currentNode.z, currentNode);
                if (nextNode.x == xDes && nextNode.z == zDes)
                {
                    return nextNode;
                }
                if (!visitedNodes.Contains(nextNode))
                {
                    queue.Enqueue(nextNode);
                    visitedNodes.Add(nextNode);
                }

            }

        }
        return null;
    }
}
