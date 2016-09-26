using System;
public class Point3D
{
    public float X;
    public float Y;
    public float Z;
    
    public Point3D(double _x, double _y, double _z)
    {
        X = Convert.ToSingle(_x);
        Y = Convert.ToSingle(_y);
        Z = Convert.ToSingle(_z);
    } 
}