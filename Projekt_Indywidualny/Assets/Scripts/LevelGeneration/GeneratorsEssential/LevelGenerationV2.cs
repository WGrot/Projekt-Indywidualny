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
    [SerializeField] private GameObject[] specialRooms;


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
        yield return null; // czekamy jedn¹ klatkê a¿stary levelgrid siê zniszczy. Trzeba to jakoœ inaczej rozwi¹zaæ póŸniej
        levelGrid = new LevelGrid(levelSize , buffour, gridScale);

        GenerateSpecialRoom(spawnRoom, levelSize / 2 + buffour, levelSize / 2 + buffour);
        
        if (ExitRoom != null)
        {
            int[] location = DetermineExitLocationAndRotation();
            GenerateSpecialRoom(ExitRoom, location[0], location[1], location[2]);
        }
        GenerateRooms(specialRooms, false);
        GenerateRooms(normalRooms, true);
        GeneratePassages();
        yield return null; // czekamy jedn¹ klatkê a¿ instancje pokoi siê pojawi¹ ¿ebu je zeskanowaæ
        ScanGrid();
        InstantiateCorridors();
        MiniMapGenerator.Instance.GenerateMiniMap();
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

    private void GenerateSpecialRoom(GameObject room, int x, int z, int chosenRotation)
    {
        GameObject roomInstance = Instantiate(room, new Vector3((x) * gridScale, 0, (z) * gridScale), Quaternion.identity);
        Room currentRoomScript = roomInstance.GetComponent<Room>();
        currentRoomScript.ApplyRotation(chosenRotation);
        RoomsInLevel.Add(roomInstance);
        levelGrid.MarkOccupiedSpace(x, z, currentRoomScript.GetRoomSizeX(), currentRoomScript.GetRoomSizeZ());
        roomCounter++;
    }

    private void GenerateRooms(GameObject[] RoomsToGenerate, bool ReloadListAtEmpty)
    {
        List<GameObject> roomList = new List<GameObject>(RoomsToGenerate);

        while (roomCounter < numberOfNormalRooms && trycounter > 0)
        {
            if (roomList.Count == 0)
            {
                if(ReloadListAtEmpty == false)
                {
                    return;
                }
                roomList = new List<GameObject>(RoomsToGenerate);
            }

            int randomRoomId = Random.Range(0, roomList.Count);
            GameObject currentRoom = roomList[randomRoomId];
            Room currentRoomScript = currentRoom.GetComponent<Room>();





            int chosenRotation = Random.Range(0, 4);
            int[] rotatedSizes = currentRoomScript.GetRotatedSizes(chosenRotation);

            int x = Random.Range(0, levelSize) + buffour;
            int z = Random.Range(0, levelSize) + buffour;

            int roomSizeX = rotatedSizes[0];
            int roomSizeZ = rotatedSizes[1];

            if (levelGrid.IsSpaceFree(x, z, roomSizeX, roomSizeZ))
            {
                levelGrid.MarkOccupiedSpace(x, z, roomSizeX, roomSizeZ);
                roomCounter++;
                roomList.RemoveAt(randomRoomId);
                GameObject roomInstance = Instantiate(currentRoom, new Vector3((x) * gridScale, 0, (z ) * gridScale), Quaternion.identity);
                
                roomInstance.GetComponent<Room>().ApplyRotation(chosenRotation);
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

    private int[] DetermineExitLocationAndRotation()
    {
        int[] location = new int[3];
        
        int rand = Random.Range(0, 4);
        location[2] = rand;
        if(rand == 0)
        {
            location[0] = levelSize / 2 + buffour;
            location[1] = levelSize + buffour;// + buffour/2;
        }
        else if (rand == 1)
        {
            location[0] = levelSize + buffour;// + buffour / 2;
            location[1] = levelSize/2 +buffour;
        }
        else if (rand == 2)
        {
            location[0] = levelSize/2 + buffour;
            location[1] = buffour; //4;
        }
        else
        {
            location[0] = buffour; //4;
            location[1] = levelSize/2 + buffour;
        }
        Debug.Log(location[0] + " " + location[1] +" " + location[2] +" ");
        return location;

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
