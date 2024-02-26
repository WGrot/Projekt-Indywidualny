using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PassageGraph
{
    private List<Edge> edges;

    List<Vertex> visitedVerticies;
    List<Edge> edgesToStay;

    public PassageGraph()
    {
        edges = new List<Edge>();
        visitedVerticies = new List<Vertex>();
        edgesToStay = new List<Edge>();

    }

    public void AddEdge(Edge edge)
    {
        if (!edges.Contains(edge))
        {
            edges.Add(edge);
        }
    }

    public List<Edge> GetEdges()
    {
        return edges;
    }

    public int GetNumberOfEdges()
    {
        return edges.Count;
    }

    public int GetNumberOfEdgesToStay()
    {
        return edgesToStay.Count;
    }


    //Przekazujemy Vertex pokoju startowego ¿eby loch mia³ jak najwiêcêj przejœæ bli¿ej pokoju startowego
    public void GenerateTree(Vertex startingVertex, Vertex[] nodes, int chanceForEdgeToRandomlyStay)
    {
        visitedVerticies.Add(startingVertex);
        ChoosePassagesBFS(startingVertex, nodes);
        DeleteUnnecesaryEdges(chanceForEdgeToRandomlyStay);
        DeleteEdgesInSameRoom();

        DeleteRepeatingEdges();

    }

    //Inny sposób generowania drzewa (Depth First Search), nie u¿ywany, ale nie wywalam bo mo¿e siê przydaæ do jakiœ innych piêter na przyk³ad
    public void ChoosePassagesDFS(Vertex v, Vertex[] nodes)
    {
        List<Edge> edgesWithVertex = FindEdgesWithVertex(v);

        foreach (Edge edge in edgesWithVertex)
        {
            if (!visitedVerticies.Contains(edge.V1))
            {
                visitedVerticies.Add(edge.V1);
                edgesToStay.Add(edge);
                ChoosePassagesDFS(edge.V1, nodes);
            }
            else if (!visitedVerticies.Contains(edge.V2))
            {
                visitedVerticies.Add(edge.V2);
                edgesToStay.Add(edge);
                ChoosePassagesDFS(edge.V2, nodes);
            }
        }
    }

    //Generujemy drzewo przejœæ u¿ywaj¹c algorytmu Breadth First Search
    public void ChoosePassagesBFS(Vertex startVertex, Vertex[] nodes)
    {
        Queue<Vertex> queue = new Queue<Vertex>();
        HashSet<Vertex> visitedVertices = new HashSet<Vertex>();

        queue.Enqueue(startVertex);
        visitedVertices.Add(startVertex);

        while (queue.Count > 0)
        {
            Vertex currentVertex = queue.Dequeue();

            List<Edge> edgesWithVertex = FindEdgesWithVertex(currentVertex);

            foreach (Edge edge in edgesWithVertex)
            {
                Vertex nextVertex = (edge.V1 == currentVertex) ? edge.V2 : edge.V1;

                if (!visitedVertices.Contains(nextVertex))
                {
                    visitedVertices.Add(nextVertex);
                    edgesToStay.Add(edge);
                    queue.Enqueue(nextVertex);
                }
            }
        }
    }

    //funkcja znajduje wszystkie krawêdzie z konkretnym wierzcho³kiem w liœcie krawêdzi
    private List<Edge> FindEdgesWithVertex(Vertex vertex)
    {
        List<Edge> edgesWithVertex = new List<Edge>();
        foreach (Edge edge in edges)
        {
            if (edge.ContainsVertex(vertex))
            {
                edgesWithVertex.Add(edge);
            }
        }
        return edgesWithVertex;
    }

    private void DeleteUnnecesaryEdges(int chanceToStay)
    {
        foreach (Edge edge in edges.ToList())
        {
            int randomChance = Random.Range(0, 100);

            if (!edgesToStay.Contains(edge))
            {
                if (randomChance > chanceToStay)
                {
                    edges.Remove(edge);
                }
            }
        }
    }

    public void DeleteEdgesInSameRoom()
    {
        foreach (Edge edge in edges.ToList())
        {
            if (edge.V1.GetRoomId() == edge.V2.GetRoomId())
            {
                edges.Remove(edge);

            }
        }
    }

    public void DeleteRepeatingEdges()
    {
        List<Edge> edgesToDelete = new List<Edge>();
        for (int i = 0; i < edges.Count; i++)
        {
            Edge edgeOne = edges[i];
            for (int j = 0; j < edges.Count; j++)
            {
                Edge edgeTwo = edges[j];
                if (i != j && edgeOne.V1.GetRoomId() == edgeTwo.V1.GetRoomId() && edgeOne.V2.GetRoomId() == edgeTwo.V2.GetRoomId())
                {
                    if (!edgesToDelete.Contains(edgeOne))
                    {
                        edgesToDelete.Add(edgeTwo);
                    }

                }
                else if (i != j && edgeOne.V1.GetRoomId() == edgeTwo.V2.GetRoomId() && edgeOne.V2.GetRoomId() == edgeTwo.V1.GetRoomId())
                {
                    if (!edgesToDelete.Contains(edgeOne))
                    {
                        edgesToDelete.Add(edgeTwo);
                    }

                }
            }
        }

        foreach (Edge edge in edgesToDelete)
        {

            if (edges.Contains(edge))
            {
                edges.Remove(edge);
            }
        }

    }
}
