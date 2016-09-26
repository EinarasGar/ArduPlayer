
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;

public class Face : IComparable<Face>
{
    public PointF[] Corners2D;
    public Point3D[] Corners3D;
    public Point3D Center;

    public string name { get; set; }

    public Face(float x0, float y0, float z0, float x1, float y1, float z1, float x2, float y2, float z2, float x3,
    float y3, float z3)
    {
        Corners3D = new Point3D[] {
            new Point3D(x0, y0, z0),
            new Point3D(x1, y1, z1),
            new Point3D(x2, y2, z2),
            new Point3D(x3, y3, z3)
        };
    }

    public int CompareTo(Face otherFace)
    {
        return Convert.ToInt32(this.Center.Z - otherFace.Center.Z);
    }

    public override string ToString()
    {
        return name;
    }

}