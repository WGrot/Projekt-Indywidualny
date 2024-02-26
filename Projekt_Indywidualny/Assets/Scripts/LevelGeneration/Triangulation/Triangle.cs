using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Triangle
{
    Vertex v1;
    Vertex v2;
    Vertex v3;
    Edge e1;
    Edge e2;
    Edge e3;
    Circle circumCircle;

    public Triangle(Vertex v1, Vertex v2, Vertex v3)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
        this.e1 = new Edge(v1, v2);
        this.e2 = new Edge(v2, v3);
        this.e3 = new Edge(v3, v1);
        this.circumCircle = new Circle(v1, v2, v3);
    }

    public Vertex GetV1()
    {
        return this.v1;
    }

    public Vertex GetV2()
    {
        return this.v2;
    }

    public Vertex GetV3()
    {
        return this.v3;
    }

    public Circle GetCircumCircle()
    {
        return circumCircle;
    }

    public Edge[] GetEdges()
    {
        return new Edge[]
        {
            new Edge(v1, v2),
            new Edge(v2, v3),
            new Edge(v3, v1),
        };
    }

    public bool ContainsEdge(Edge edge)
    {
        if (edge.Equals(e1))
        {
            return true;
        }
        if (edge.Equals(e2))
        {
            return true;
        }
        if (edge.Equals(e3))
        {
            return true;
        }
        return false;
    }
    public override bool Equals(object obj)
    {
        return obj is Triangle triangle &&
               EqualityComparer<Vertex>.Default.Equals(v1, triangle.v1) &&
               EqualityComparer<Vertex>.Default.Equals(v2, triangle.v2) &&
               EqualityComparer<Vertex>.Default.Equals(v3, triangle.v3);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(v1, v2, v3);
    }


}



