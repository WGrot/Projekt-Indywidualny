using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class LevelGrid
{
    public int[,] grid;
    private int size;
    private int buffour = 20;
    private int scale = 1;

    public static LevelGrid Instance { get; private set; }
    public LevelGrid(int levelSize, int buffour, int scale)
    {
        this.buffour = buffour;
        this.size = levelSize + 2 * buffour;
        this.scale = scale;
        this.grid = new int[levelSize + 2 * buffour, levelSize + 2 * buffour];
        SetupLevelGrid();

        if (Instance != null)
        {
            Debug.LogError("More than one LevelGrid instance found!");
            return;
        }

        Instance = this;
        this.scale = scale;
    }

    void SetupLevelGrid()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = ConstantValues.EMPTY_FIELD_VALUE;
            }
        }
    }

    public void SetFieldValue(int x, int z, int value)
    {
        grid[x, z] = value; 
    }

    public int GetFieldValue(int x, int z)
    {
        if (x >= size || x < 0 || z >= size || z < 0)
        {
            return -1;
        }
        return grid[x, z];

    }


    public void MarkOccupiedSpace(int locationX, int locationZ, int roomSizeX, int roomSizeZ)
    {

        int[] startingPoints = DetermineStart(locationX, locationZ, roomSizeX, roomSizeZ);
        for (int i = startingPoints[0] -1; i < startingPoints[0] + Mathf.Abs(roomSizeX) + 1; i++)
        {
            for (int j = startingPoints[1] -1; j < startingPoints[1] + Mathf.Abs(roomSizeZ) +1; j++)
            {
                grid[i, j] = ConstantValues.ROOM_FIELD_VALUE;
            }
        }
    }

    //W funkcji IsSpace free sprawdzamy od -1 do +1 ¿eby mieæ pewnoœæ ¿e pokój nie bêdzie kolidowa³ z innym obok
    // Ogólnie mo¿naby to zostawiæ bez tych dodatkowych jedynek, ale wtedy trzeba by zmodyfikowaæ CorridorSegment.cs
    //¿eby w jakiœ sposób wykrywa³ z której strony ma siê po³¹czyæ z pokojem
    //jeœli siê tego nie zrobi i wywali 1, to zostaj¹ usuniête œciany które powinny zostaæ
    public bool IsSpaceFree(int locationX, int locationZ, int roomSizeX, int roomSizeZ)
    {
        int[] startingPoints = DetermineStart(locationX, locationZ, roomSizeX, roomSizeZ);

        for (int i = startingPoints[0] -1 ; i < startingPoints[0] + Mathf.Abs(roomSizeX) +1; i++)
        {
            for (int j = startingPoints[1] -1; j < startingPoints[1] + Mathf.Abs(roomSizeZ) + 1; j++)
            {
                if (grid[i, j] != ConstantValues.EMPTY_FIELD_VALUE)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int[] DetermineStart(int locationX, int locationZ, int roomSizeX, int roomSizeZ)
    {
        int[] possibleZ = { locationZ, locationZ + roomSizeZ };
        int[] possibleX = { locationX, locationX + roomSizeX };
        int startX = possibleX.Min();
        int startZ = possibleZ.Min();

        int[] result = { startX, startZ};

        return result;
    }

    public int GetSize()
    {
        return size;
    }
    public int GetBuffour()
    {
        return buffour;
    }

    public int GetScale()
    {
        return scale;
    }

    public void printGrid()
    {

        StringBuilder sb = new StringBuilder();
        for (int j = size - 1; j >= 0; j--)
        {
            for (int i = 0; i < size; i++)
            {
                sb.Append(grid[i, j]);
                sb.Append("");
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }
}
