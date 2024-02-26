using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    Vertex v1;
    Vertex v2;

    public Edge(Vertex v1, Vertex v2)
    {
        this.V1 = v1;
        this.V2 = v2;
    }

    public Vertex V1 { get => v1; set => v1 = value; }
    public Vertex V2 { get => v2; set => v2 = value; }

    public override bool Equals(object obj)
    {
        return obj is Edge edge && (
               (EqualityComparer<Vertex>.Default.Equals(V1, edge.V1) &&
               EqualityComparer<Vertex>.Default.Equals(V2, edge.V2)) ||
               (EqualityComparer<Vertex>.Default.Equals(V1, edge.V2) &&
               EqualityComparer<Vertex>.Default.Equals(V2, edge.V1)));
    }

    public bool ContainsVertex(Vertex v)
    {
        if (V1.Equals(v))
        {
            return true;
        }
        if(V2.Equals(v)){
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(V1, V2);
    }

    public override string ToString()
    {
        return $"Edge: {V1} - {V2}";
    }
}
