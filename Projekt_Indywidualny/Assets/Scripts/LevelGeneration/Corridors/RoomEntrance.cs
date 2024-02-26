using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntrance : MonoBehaviour
{
    private int xOnGrid;
    private int zOnGrid;


    [SerializeField] private GameObject roomWall;
    [SerializeField] private GameObject roomDoors;

    private void OnEnable()
    {
        LevelGenerationV2.OnLevelGenerated += ChangeDoorToWall;
    }

    private void OnDisable()
    {
        LevelGenerationV2.OnLevelGenerated -= ChangeDoorToWall;
    }

    private void DeterminePositionOnGrid()
    {
        xOnGrid = (int)transform.position.x / LevelGrid.Instance.GetScale();
        zOnGrid = (int)transform.position.z / LevelGrid.Instance.GetScale();
    }

    private void ChangeDoorToWall()
    {
        DeterminePositionOnGrid();
        if (LevelGrid.Instance.GetFieldValue(xOnGrid, zOnGrid) != 6)
        {
            Instantiate(roomWall, roomDoors.transform.position, roomDoors.transform.rotation);
            roomDoors.SetActive(false);
        }
    }
}
