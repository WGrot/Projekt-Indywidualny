using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{

    [SerializeField] private int roomSizeX;
    [SerializeField] private int roomSizeZ;

    [SerializeField] private GameObject[] roomEntrances;


    public void ApplyRotation(int chosenRotation)
    {
        /* 0 - domyœlna rotacja
        * 1 - 90 stopni w prawo
        * 2 - 180 stopni w prawo
        * 3 - 270stopni w prawo
        */

        for (int i = 0; i < chosenRotation; i++)
        {

            RotateSizes();
            transform.Rotate(0, 90, 0);
        }


    }

    void RotateSizes()
    {
        int tmp = roomSizeX;
        roomSizeX = roomSizeZ;
        roomSizeZ = -tmp;
    }

    public int[] GetRotatedSizes(int rotation)
    {
        int tmpSizeX = roomSizeX;
        int tmpSizeZ = roomSizeZ;

        for (int i =0; i < rotation; i++)
        {
            int tmp = tmpSizeX;
            tmpSizeX = tmpSizeZ;
            tmpSizeZ = -tmp;
        }
        int[] result = {tmpSizeX, tmpSizeZ}; 
        return result;
    }

    public int GetRoomSizeX()
    {
        return roomSizeX;
    }

    public int GetRoomSizeZ()
    {
        return roomSizeZ;
    }

    public float[] GetCorridorDestination(int scale)
    {
        float[] position = new float[2];
        if (roomEntrances != null)
        {
            position[0] = roomEntrances[0].transform.position.x;
            position[1] = roomEntrances[0].transform.position.z;
            return position;
        }
        position[0] = transform.position.x + roomSizeX * scale / 2;
        position[1] = transform.position.z + roomSizeZ * scale / 2;
        return position;
    }

    public Vertex[] GetEntrancesVerticies(int roomId)
    {
        int n = roomEntrances.Length;
        Vertex[] verticies = new Vertex[n];
        for (int i = 0; i < n; i++)
        {
            verticies[i] = new Vertex(roomEntrances[i].transform.position.x, roomEntrances[i].transform.position.z , roomId);
        }
        return verticies;
    }

}