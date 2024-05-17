using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorSegment : MonoBehaviour
{
    private int xOnGrid;
    private int zOnGrid;

    private bool checkForRooms = false;

    [SerializeField] private GameObject northWall;
    [SerializeField] private GameObject southWall;
    [SerializeField] private GameObject westWall;
    [SerializeField] private GameObject eastWall;

    [SerializeField] private int chanceForTrapToSpawn;
    [SerializeField] private GameObject trap;
    [SerializeField] private GameObject floor;

    private void Start()
    {
        DeterminePositionOnGrid();

        if (LevelGrid.Instance.GetFieldValue(xOnGrid, zOnGrid) == ConstantValues.ENTRANCE_FIELD_VALUE)
        {
            checkForRooms = true;
        }
        else
        {
            checkForRooms = false;
        }

        DeleteUnnecesaryCorridorWalls();
        if (checkForRooms)
        {
            DeleteUnnecesaryRoomWalls();
        }

        DetermineTrap();
    }

    private void DeterminePositionOnGrid()
    {
        xOnGrid = (int)transform.position.x / LevelGrid.Instance.GetScale();
        zOnGrid = (int)transform.position.z / LevelGrid.Instance.GetScale();
    }

    private void DeleteUnnecesaryCorridorWalls()
    {
        int northFieldValue = LevelGrid.Instance.GetFieldValue(xOnGrid, zOnGrid + 1);
        int southFieldValue = LevelGrid.Instance.GetFieldValue(xOnGrid, zOnGrid - 1);
        int westFieldValue = LevelGrid.Instance.GetFieldValue(xOnGrid + 1, zOnGrid);
        int eastFieldValue = LevelGrid.Instance.GetFieldValue(xOnGrid - 1, zOnGrid);

        if (northFieldValue == ConstantValues.CORRIDOR_FIELD_VALUE || northFieldValue == ConstantValues.ENTRANCE_FIELD_VALUE)
        {
            Destroy(northWall);
        }

        if (southFieldValue == ConstantValues.CORRIDOR_FIELD_VALUE || southFieldValue == ConstantValues.ENTRANCE_FIELD_VALUE)
        {
            Destroy(southWall);
        }

        if (westFieldValue == ConstantValues.CORRIDOR_FIELD_VALUE || westFieldValue == ConstantValues.ENTRANCE_FIELD_VALUE)
        {
            Destroy(westWall);
        }

        if (eastFieldValue == ConstantValues.CORRIDOR_FIELD_VALUE || eastFieldValue == ConstantValues.ENTRANCE_FIELD_VALUE)
        {
            Destroy(eastWall);
        }
    }

    private void DeleteUnnecesaryRoomWalls()
    {
        int northFieldValue = LevelGrid.Instance.GetFieldValue(xOnGrid, zOnGrid + 1);
        int southFieldValue = LevelGrid.Instance.GetFieldValue(xOnGrid, zOnGrid - 1);
        int westFieldValue = LevelGrid.Instance.GetFieldValue(xOnGrid + 1, zOnGrid);
        int eastFieldValue = LevelGrid.Instance.GetFieldValue(xOnGrid - 1, zOnGrid);

        if (northFieldValue == ConstantValues.ROOM_FIELD_VALUE)
        {
            Destroy(northWall);
        }

        if (southFieldValue == ConstantValues.ROOM_FIELD_VALUE)
        {
            Destroy(southWall);
        }

        if (westFieldValue == ConstantValues.ROOM_FIELD_VALUE)
        {
            Destroy(westWall);
        }

        if (eastFieldValue == ConstantValues.ROOM_FIELD_VALUE)
        {
            Destroy(eastWall);
        }
    }

    private void DetermineTrap()
    {
        int randInt = Random.Range(0, 100);

        if (randInt <= chanceForTrapToSpawn)
        {
            Instantiate(trap, floor.transform.position, Quaternion.identity);
            Destroy(floor);
            floor.SetActive(false);
        }
    }
}
