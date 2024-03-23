using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationV2 : MonoBehaviour
{
    [Header("Grid Parameters")]
    [SerializeField] private int levelSize = 50;
    [SerializeField] private int buffour = 20;
    [SerializeField] private int gridScale = 1;


    [Header("Level Parameters")]
    [SerializeField] private int numberOfNormalRooms = 10;
    [SerializeField] private int ChanceForEdgeToStay = 15;
    [SerializeField] int trycounter = 300;

    [Header("Assign Rooms and parameters")]
    [SerializeField] private GameObject spawnRoom;
    [SerializeField] private GameObject ExitRoom;
    [SerializeField] private GameObject[] normalRooms;


    [Header("Assign Corridor Segments")]
    [SerializeField] private GameObject[] CorridorSegments;
    [SerializeField] private GameObject CorridorEntrance;

    List<GameObject> RoomsInLevel = new List<GameObject>();

    LevelGrid levelGrid;
    Triangulator triangulator;
    PassageGraph passageGraph;
    BFS_Pathfinding bfs_Pathfinding;


    [SerializeField] private GameObject line;

    public delegate void GenerateAction();
    public static event GenerateAction OnLevelGenerated;

    int roomCounter = 0;



    IEnumerator Start()
    {

        levelGrid = new LevelGrid(levelSize , buffour, gridScale);
        GenerateSpecialRoom(spawnRoom, levelSize / 2 + buffour, levelSize / 2 + buffour);
        if (ExitRoom != null)
        {
            GenerateSpecialRoom(ExitRoom, buffour / 2, buffour / 2);
        }

        GenerateRooms();
        GeneratePassages();
        yield return null; // new WaitForSeconds(0.1f);
        ScanGrid();
        InstantiateCorridors();
        if (OnLevelGenerated != null)
        {
            OnLevelGenerated();
        }

    }

    private void GenerateSpecialRoom(GameObject room, int x, int z)
    {


        GameObject roomInstance = Instantiate(room, new Vector3((x ) * gridScale, 0, (z ) * gridScale), Quaternion.identity);
        Room currentRoomScript = roomInstance.GetComponent<Room>();
        RoomsInLevel.Add(roomInstance);
        levelGrid.MarkOccupiedSpace(x , z , currentRoomScript.GetRoomSizeX(), currentRoomScript.GetRoomSizeZ());
        roomCounter++;
    }

    private void GenerateRooms()
    {
        List<GameObject> roomList = new List<GameObject>(normalRooms);

        while (roomCounter < numberOfNormalRooms && trycounter > 0)
        {
            if (roomList.Count == 0)
            {
                roomList = new List<GameObject>(normalRooms);
            }

            int randomRoomId = Random.Range(0, roomList.Count);
            GameObject currentRoom = roomList[randomRoomId];
            Room currentRoomScript = currentRoom.GetComponent<Room>();


            int x = Random.Range(0, levelSize) + buffour;
            int z = Random.Range(0, levelSize) + buffour;




            // trzeba naprawiæ metodê ChoseRandomRotaation: currentRoomScript.ChooseRandomRotation();


            int roomSizeX = currentRoomScript.GetRoomSizeX();
            int roomSizeZ = currentRoomScript.GetRoomSizeZ();

            if (levelGrid.IsSpaceFree(x, z, roomSizeX, roomSizeZ))
            {
                levelGrid.MarkOccupiedSpace(x, z, roomSizeX, roomSizeZ);
                roomCounter++;
                roomList.RemoveAt(randomRoomId);
                GameObject roomInstance = Instantiate(currentRoom, new Vector3((x) * gridScale, 0, (z ) * gridScale), Quaternion.identity);
                RoomsInLevel.Add(roomInstance);
            }

            trycounter--;

        }

    }

    private void GeneratePassages()
    {
        Vertex[] roomsEntrancesVertices = RoomsToVertices();
        triangulator = new Triangulator(roomsEntrancesVertices);
        triangulator.Triangulate();


        passageGraph = triangulator.TrianglesToGraph();
        passageGraph.GenerateTree(roomsEntrancesVertices[0], RoomsToVertices(), ChanceForEdgeToStay);
        EdgePrinter();
    }

    private Vertex[] RoomsToVertices()
    {
        List<Vertex> vertices = new List<Vertex>();

        for (int i = 0; i < RoomsInLevel.Count; i++)
        {
            GameObject room = RoomsInLevel[i];
            Room roomScript = room.GetComponent<Room>();
            Vertex[] roomEntrances = roomScript.GetEntrancesVerticies(i);

            for (int j = 0; j < roomEntrances.Length; j++)
            {
                if (!vertices.Contains(roomEntrances[j]))
                {
                    Vertex vertex = roomEntrances[j];
                    vertices.Add(vertex);
                }

            }
        }
        return vertices.ToArray();
    }

    private void EdgePrinter()
    {
        List<Edge> edges = passageGraph.GetEdges();

        foreach (Edge edge in edges)
        {
            GameObject lines = Instantiate(line);
            LineRenderer lineRenderer = lines.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, new Vector3(edge.V1.X, 2, edge.V1.Y));
            lineRenderer.SetPosition(1, new Vector3(edge.V2.X, 2, edge.V2.Y));
        }
    }


    private void ScanGrid()
    {

        int size = levelGrid.GetSize();
        RaycastHit scan;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                transform.position = new Vector3((i + 0.5f) * gridScale, 10, (j + 0.5f) * gridScale);
                if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out scan))
                {
                    if (scan.collider.gameObject.CompareTag("Room"))
                    {
                        levelGrid.SetFieldValue(i, j, ConstantValues.ROOM_FIELD_VALUE);

                    }
                }
                else
                {
                    levelGrid.SetFieldValue(i, j, ConstantValues.EMPTY_FIELD_VALUE);
                }
            }
        }

    }

    private void InstantiateCorridors()
    {
        bfs_Pathfinding = new BFS_Pathfinding(levelGrid);
        List<Edge> edges = passageGraph.GetEdges();


        foreach (Edge edge in edges)
        {
            int[] start = { (int)edge.V1.X / gridScale, (int)edge.V1.Y / gridScale };
            int[] goal = { (int)edge.V2.X / gridScale, (int)edge.V2.Y / gridScale };
            levelGrid.SetFieldValue(start[0], start[1], ConstantValues.ENTRANCE_FIELD_VALUE);
            levelGrid.SetFieldValue(goal[0], goal[1], ConstantValues.ENTRANCE_FIELD_VALUE);
            GridNode currentNode = bfs_Pathfinding.FindCorridorPath(start[0], start[1], goal[0], goal[1]);
            while (currentNode != null)
            {
                if (levelGrid.GetFieldValue(currentNode.x, currentNode.z) == ConstantValues.EMPTY_FIELD_VALUE)
                {
                    Instantiate(CorridorEntrance, new Vector3((currentNode.x) * gridScale, 0, (currentNode.z) * gridScale), Quaternion.identity);
                    levelGrid.SetFieldValue(currentNode.x, currentNode.z, ConstantValues.CORRIDOR_FIELD_VALUE);
                }else if (levelGrid.GetFieldValue(currentNode.x, currentNode.z) == ConstantValues.ENTRANCE_FIELD_VALUE)
                {
                    Instantiate(CorridorEntrance, new Vector3((currentNode.x) * gridScale, 0, (currentNode.z) * gridScale), Quaternion.identity);
                }
                currentNode = currentNode.prev;

            }
        }
        levelGrid.printGrid();
    }
}
