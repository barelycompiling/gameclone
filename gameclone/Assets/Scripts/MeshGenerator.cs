using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]      //check that we have MeshFilter attached
public class MeshGenerator : MonoBehaviour
{
    public Material material;

    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    public void SetRoundedMesh(double outerRadius, double innerRadius, double startPhi, double endPhi)
    {
        Clear();
        AddRoundedPart(outerRadius, innerRadius, startPhi, endPhi);
        UpdateMesh();
    }

    public void SetRectangularMesh(double x0, double y0, double x1, double y1)
    {
        Clear();
        AddSquare(x0, y0, x1, y1);
        UpdateMesh();
    }

    void Clear()
    {
        mesh = new Mesh();
        vertices = new List<Vector3>();
        triangles = new List<int>();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        GetComponent<MeshRenderer>().material = material;
        print("created a playground element");
    }

    //########### Code related to mesh generation #############

    void AddTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        int baseN = vertices.Count;
        vertices.Add(p1);
        vertices.Add(p2);
        vertices.Add(p3);

        triangles.Add(baseN);
        triangles.Add(baseN + 1);
        triangles.Add(baseN + 2);
    }

    void AddQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        AddTriangle(p1, p2, p3);
        AddTriangle(p3, p4, p1);
    }

    void AddRoundedPart(double radius1, double radius2, double phi0, double phi1)
    {
        print("called method AddRoundedPart with arguments: " + phi0.ToString() + ", " + phi1.ToString());
        // Validate arguments
        if (radius1 < radius2)
            throw new System.ArgumentException("radius1 cannot be smaller than radius2", "radius1");

        if (phi1 < phi0)
            throw new System.ArgumentException("phi1 cannot be smaller than phi0", "phi1");

        // Choose angle step so that the side of circle chord is smaller than 1/180 meters
        double pi = Math.PI;
        double angleStep = pi / 180 / radius1;
        int quadN = (int) Math.Ceiling((phi1 - phi0) / angleStep);
        angleStep = (phi1 - phi0) / quadN;

        // Create quads which approximate the circled shape
        for (int i = 0; i < quadN; i++)
        {
            double startPhi = phi0 + i * angleStep;
            double endPhi = phi0 + (i + 1) * angleStep;
            var p0 = genCirclePoint(radius2, startPhi);
            var p1 = genCirclePoint(radius1, startPhi);
            var p2 = genCirclePoint(radius1, endPhi);
            var p3 = genCirclePoint(radius2, endPhi);

            AddQuad(p0, p1, p2, p3);
        }
    }

    Vector3 genCirclePoint(double radius, double phi)
    {
        return new Vector3(
                (float)(radius * Math.Cos(phi)),
                (float)(radius * Math.Sin(phi)),
                0);
    }

    void AddSquare(double x0, double y0, double x1, double y1)
    {
        var p0 = new Vector3((float)x0, (float)y0, 0);
        var p1 = new Vector3((float)x1, (float)y0, 0);
        var p2 = new Vector3((float)x1, (float)y1, 0);
        var p3 = new Vector3((float)x0, (float)y1, 0);

        AddQuad(p0, p1, p2, p3);
    }
}
