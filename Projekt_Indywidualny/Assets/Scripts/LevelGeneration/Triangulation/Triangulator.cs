using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class Triangulator
{
    private Vertex[] pointsToTriangulate;
    private List<Triangle> triangles;
    private Triangle superTriangle;

    public Triangulator(Vertex[] pointsToTriangulate)
    {
        this.pointsToTriangulate = pointsToTriangulate;
        this.triangles = new List<Triangle>();
        this.superTriangle = new Triangle(new Vertex(-100, -100), new Vertex(2000, -100), new Vertex(-100, 2000));  //It could be good to add some reasonable function to determine supertriangle
        this.triangles.Add(superTriangle);
    }

    public void Triangulate()
    {
        foreach (Vertex vertex in pointsToTriangulate)
        {
            List<Triangle> badTriangles = new List<Triangle>();

            //Identify 'bad triangles'
            for (int triIndex = triangles.Count - 1; triIndex >= 0; triIndex--)
            {
                Triangle triangle = triangles[triIndex];

                //A 'bad triangle' is defined as a triangle who's CircumCentre contains the current point
                float dist = triangle.GetCircumCircle().FindDistanceToCentre(vertex);
                if (dist < triangle.GetCircumCircle().radius)
                {
                    badTriangles.Add(triangle);
                }
            }

            //Contruct a polygon from unique edges, i.e. ignoring duplicate edges inclusively
            List<Edge> polygon = new List<Edge>();
            for (int i = 0; i < badTriangles.Count; i++)
            {
                Triangle triangle = badTriangles[i];
                Edge[] edges = triangle.GetEdges();

                for (int j = 0; j < edges.Length; j++)
                {
                    bool rejectEdge = false;
                    for (int t = 0; t < badTriangles.Count; t++)
                    {
                        if (t != i && badTriangles[t].ContainsEdge(edges[j]))
                        {
                            rejectEdge = true;
                        }
                    }

                    if (!rejectEdge)
                    {
                        polygon.Add(edges[j]);
                    }
                }
            }

            //Remove bad triangles from the triangulation
            for (int i = badTriangles.Count - 1; i >= 0; i--)
            {
                triangles.Remove(badTriangles[i]);
            }


            //Create new triangles
            for (int i = 0; i < polygon.Count; i++)
            {
                Edge edge = polygon[i];
                Vertex pointA = new Vertex(vertex.X, vertex.Y, vertex.GetRoomId());
                Vertex pointB = new Vertex(edge.V1, edge.V1.GetRoomId());
                Vertex pointC = new Vertex(edge.V2, edge.V2.GetRoomId());

                triangles.Add(new Triangle(pointA, pointB, pointC));


            }

        }

        //Delete all triangles that shere a vertex with supertriangle
        foreach (Triangle triangle in triangles.ToList())
        {
            Vertex[] vertices = { triangle.GetV1(), triangle.GetV2(), triangle.GetV3() };
            foreach (Vertex vertex in vertices)
            {
                if (vertices.Contains(superTriangle.GetV1()) || vertices.Contains(superTriangle.GetV2()) || vertices.Contains(superTriangle.GetV3()))
                {
                    triangles.Remove(triangle);
                }
            }

        }
    }

    public void PrintPointToTriangulate()
{
        for (int i = 0; i < pointsToTriangulate.Length; i++)
        {
            Debug.Log(pointsToTriangulate[i].X + "  " + pointsToTriangulate[i].Y + "\n");
        }
    }

    public List<Triangle> GetTriangles()
    {
        return triangles;
    }

    public PassageGraph TrianglesToGraph()
    {
        PassageGraph graph = new PassageGraph();

        foreach (Triangle triangle in triangles)
        {
            graph.AddEdge(triangle.GetEdges()[0]);
            graph.AddEdge(triangle.GetEdges()[1]);
            graph.AddEdge(triangle.GetEdges()[2]);
        }
        return graph;
    }
}
