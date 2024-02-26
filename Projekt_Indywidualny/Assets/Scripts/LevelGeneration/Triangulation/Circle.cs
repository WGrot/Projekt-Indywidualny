using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle
{
    public float radius;
    public Vertex center;

    public Circle(float radius, Vertex center)
    {
        this.radius = radius;
        this.center = center;
    }

    public Circle(Vertex v1, Vertex v2, Vertex v3)
    {
        this.center = FindCircleCenter(v1, v2, v3);
        this.radius = FindDistanceToCentre(v1);
    }

    public static Vertex FindCircleCenter(Vertex v1, Vertex v2, Vertex v3)
    {
        float x1 = v1.X, y1 = v1.Y;
        float x2 = v2.X, y2 = v2.Y;
        float x3 = v3.X, y3 = v3.Y;

        float a = x2 - x1;
        float b = y2 - y1;
        float c = x3 - x1;
        float d = y3 - y1;

        float e = a * (x1 + x2) + b * (y1 + y2);
        float f = c * (x1 + x3) + d * (y1 + y3);

        float g = 2 * (a * (y3 - y2) - b * (x3 - x2));

        if (g == 0)
        {
            throw new Exception("Punkty s¹ wspó³liniowe, nie mo¿na obliczyæ œrodka okrêgu. {" + x1 + " " + y1 + "}{" + x2 + " " + y2 + "}{" + x3 + " " + y3 + "}");
        }

        float centerX = (d * e - b * f) / g;
        float centerY = (a * f - c * e) / g;

        return new Vertex(centerX, centerY);
    }

    public float FindDistanceToCentre(Vertex v)
    {
        float deltaX = center.X - v.X;
        float deltaY = center.Y - v.Y;
        return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }


    public override bool Equals(object obj)
    {
        return obj is Circle circle &&
               radius == circle.radius &&
               center == circle.center;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(radius, center);
    }

    public void PrintCircleInfo()
    {
        Debug.Log(center.X + "  " + center.Y + "  " + radius);
    }
}
