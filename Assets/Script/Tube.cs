using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Tube : MonoBehaviour
{
    public int NumberOfPoints;

    private Vector3[] vertices;
    private List<Vector3> circle01;
    private List<Vector3> circle02;

    private Mesh mesh;

    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        Debug.Log(NumberOfPoints);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Drawing Tube";

        circle01 = MakeCircle(new Vector3(0, 0));
        circle02 = MakeCircle(new Vector3(0, 0,-5));
        circle01.AddRange(circle02);
        vertices = circle01.ToArray();

        for (int i = 0; i < vertices.Length; i++)
            Debug.Log(vertices[i]);

        //Fil in triangles
        //Fill in rear
        List<int> triangles = new List<int>();
        int num = vertices.Length/2 - 1;
        for (int ti = 0, vi = 0, x = 0; x < num; x++, ti += 6, vi++)
        {
            triangles.Add(vi);
            triangles.Add(vi + num + 1);
            triangles.Add(vi + 1);
            triangles.Add(vi + 1);
            triangles.Add(vi + num + 1);
            triangles.Add(vi + num + 2);
            yield return wait;
        }

        for (int ti = 0, vi = 0, x = 0; x < num; x++, ti += 6, vi++)
        {
            triangles.Add(vi);
            triangles.Add(vi + 1);
            triangles.Add(vi + num + 1);
            triangles.Add(vi + num + 1);
            triangles.Add(vi + 1);
            triangles.Add(vi + num + 2);
            yield return wait;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();

        int[] array = triangles.ToArray();
        for(int i=0; i< array.Length; i+=3)
        {
            Debug.Log("("+array[i]+", "+array[i+1]+", "+array[i+2]+")");
        }

        //OldMakeCircle(NumberOfPoints);
        yield return wait;
    }

    public List<Vector3> MakeCircle(Vector3 center)
    {
        float angleStep = 360.0f / (float)NumberOfPoints;
        List<Vector3> circleList = new List<Vector3>();
        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, angleStep);

        //circleList.Add(center);
        circleList.Add(center + new Vector3(0.0f, 0.5f, 0.0f)); //Circle Radius 0.5f
        circleList.Add(quaternion * circleList[0]);
        for (int i = 0; i < NumberOfPoints ; i++)
        {
            circleList.Add(quaternion * circleList[circleList.Count - 1]);
        }
        return circleList;
    }
    
    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color=Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.05f);
        }
    }

    /*
    public void OldMakeCircle(int numOfPoints)
    {
        Debug.Log("Make circle");
        float angleStep = 360.0f / (float)numOfPoints;
        List<Vector3> vertexList = new List<Vector3>();
        List<int> triangleList = new List<int>();
        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, angleStep);
        // Make first triangle.
        vertexList.Add(new Vector3(0.0f, 0.0f, 0.0f));  // 1. Circle center.
        vertexList.Add(new Vector3(0.0f, 0.5f, 0.0f));  // 2. First vertex on circle outline (radius = 0.5f)
        vertexList.Add(quaternion * vertexList[1]);     // 3. First vertex on circle outline rotated by angle)
                                                        // Add triangle indices.
        triangleList.Add(0);
        triangleList.Add(1);
        triangleList.Add(2);

        //TriangelList (0,1,2) (0,2,3) (0,3,4) (0,4,5) ....
        for (int i = 0; i < numOfPoints - 1; i++)
        {
            triangleList.Add(0);                      // Index of circle center. 
            triangleList.Add(vertexList.Count - 1);
            triangleList.Add(vertexList.Count);
            vertexList.Add(quaternion * vertexList[vertexList.Count - 1]);
        }
        int[] array = triangleList.ToArray();
        Debug.Log(array.Length);
        mesh.vertices = vertexList.ToArray();
        mesh.triangles = triangleList.ToArray();
    }
    */
}
