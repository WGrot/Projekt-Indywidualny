using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    protected float x;
    protected float y;
    int roomId;

    public Vertex(float x, float y)
    {
        this.x = x;
        this.y = y;
        this.roomId = -1;
    }

    public Vertex(Vertex v)
    {
        this.x = v.x;
        this.y = v.y;
        this.roomId = -1;
    }

    public Vertex(float x, float y, int roomId)
    {
        this.x = x;
        this.y = y;
        this.roomId = roomId;
    }

    public Vertex(Vertex v, int roomId)
    {
        this.x = v.x;
        this.y = v.y;
        this.roomId = roomId;
    }

    public int GetRoomId()
    {
        return roomId;
    }

    public float X { get => x; set => x = value; }
    public float Y { get => y; set => y = value; }

    public override bool Equals(object obj)
    {
        return obj is Vertex vertex &&
               x == vertex.x &&
               y == vertex.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override string ToString()
    {
        return $"({X}, {Y} )" + " " + roomId;
    }

}
