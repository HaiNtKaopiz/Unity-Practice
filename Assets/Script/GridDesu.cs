using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class GridDesu : MonoBehaviour
{
    public int xSize, ySize; //Size of the grid
    private Vector3[] vertices; // Array of 3D vectors to store the points
    private Mesh mesh;

    private void Awake()
    {
        StartCoroutine(Generate());        
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        Debug.Log(xSize);
        vertices = new Vector3[ (xSize + 1) * (ySize + 1) ]; 
        //Positioning the vertices
        for(int i =0, y = 0;  y<= ySize; y++)
        {
            for(int x=0; x<= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return wait;
            }
        }
        mesh.vertices = vertices;

        //UV coordinates
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize); //why?
                //uv[i] = new Vector2(x/xSize, y/ySize);
                tangents[i] = tangent;
            }
        }
        mesh.uv = uv;
        mesh.tangents = tangents;

        //Create a triangle for mesh
        int[] triangles = new int[xSize * ySize * 6]; //one row of grid * the number of vertices needed to draw a square
        // ti-Triangle indices      vi-vertx indices

        for(int ti=0, vi=0, y=0; y<ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                yield return wait;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        yield return wait;
        /*
         Which side a triangle is visible from is determined by the orientation of its vertex indices. 
         By default, if they are arranged in a clockwise direction the triangle is considered to be
         forward-facing and visible. Counter-clockwise triangles are discarded so we don't need to spend time
         rendering the insides of objects, which are typically not meant to be seen anyway.
         
         */
    }

    private void OnDrawGizmos()
    {
        //OnDrawGizmos invoked while Unity in edit mode where we have no vertices
        if (vertices == null)
        {
            return;
        }

        //Drawing the Gizmos
        Gizmos.color = Color.white;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
        
    }
}
